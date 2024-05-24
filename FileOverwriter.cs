using System;
using System.IO;

namespace SecureDelete
{
    public class FileOverwriter
    {
        private static readonly Random random = new Random();

        public void OverwriteFile(string filePath, string method, Action<int> reportProgress, Action<string> reportError)
        {
            if (method == "DoD 5220.22-M")
            {
                OverwriteWithDoD(filePath, reportProgress, reportError);
            }
            else if (method == "Gutmann")
            {
                OverwriteWithGutmann(filePath, reportProgress);
            }
        }

        private void OverwriteWithDoD(string filePath, Action<int> reportProgress, Action<string> reportError)
        {
            byte[] zeros = new byte[1024];
            byte[] ones = new byte[1024];
            byte[] randomData = new byte[1024];
            byte[] buffer = new byte[1024];
            int bufferLength = buffer.Length;

            Array.Fill(zeros, (byte)0x00);
            Array.Fill(ones, (byte)0xFF);

            byte[] originalData = File.ReadAllBytes(filePath); // Backup original data
            byte[] randomDataWritten = new byte[bufferLength];

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    long fileSize = fs.Length;

                    // First pass: write zeros
                    WriteDataAndReportProgress(fs, zeros, reportProgress, fileSize, 0);

                    // Second pass: write ones
                    WriteDataAndReportProgress(fs, ones, reportProgress, fileSize, 25);

                    // Third pass: write random data
                    random.NextBytes(randomData);
                    Buffer.BlockCopy(randomData, 0, randomDataWritten, 0, bufferLength);
                    WriteDataAndReportProgress(fs, randomData, reportProgress, fileSize, 50);

                    // Fourth pass: verification
                    VerifyDataAndReportProgress(fs, randomDataWritten, reportProgress, fileSize, 75);
                }

                // Proceed to delete the file securely if verification is successful
                File.Delete(filePath);
                reportProgress(100); // Report completion
            }
            catch (Exception ex)
            {
                reportError($"An error occurred: {ex.Message}. Process aborted.");
                RestoreOriginalData(filePath, originalData); // Restore original data if an error occurs
            }
        }

        private void WriteDataAndReportProgress(FileStream fs, byte[] data, Action<int> reportProgress, long fileSize, int basePercentage)
        {
            fs.Seek(0, SeekOrigin.Begin);
            for (long i = 0; i < fileSize; i += data.Length)
            {
                fs.Write(data, 0, data.Length);
                reportProgress((int)((double)i / fileSize * 100 / 4 + basePercentage));
            }
        }

        private void VerifyDataAndReportProgress(FileStream fs, byte[] expectedData, Action<int> reportProgress, long fileSize, int basePercentage)
        {
            byte[] buffer = new byte[1024];
            int bufferLength = buffer.Length;

            fs.Seek(0, SeekOrigin.Begin);
            for (long i = 0; i < fileSize; i += bufferLength)
            {
                fs.Read(buffer, 0, bufferLength);
                for (int j = 0; j < bufferLength; j++)
                {
                    if (buffer[j] != expectedData[j])
                    {
                        reportError($"Verification failed at byte {i + j}. Process aborted.");
                        return;
                    }
                }
                reportProgress((int)((double)i / fileSize * 100 / 4 + basePercentage));
            }
        }

        private void reportError(string v)
        {
            throw new NotImplementedException();
        }

        private void OverwriteWithGutmann(string filePath, Action<int> reportProgress)
        {
            byte[] patterns = new byte[35] {
                0x55, 0xAA, 0x92, 0x49, 0x24, 0x6D, 0xB6, 0xDB, 0x49, 0x92,
                0x24, 0x6D, 0xB6, 0xDB, 0x49, 0x92, 0x24, 0x6D, 0xB6, 0xDB,
                0x49, 0x92, 0x24, 0x6D, 0xB6, 0xDB, 0x49, 0x92, 0x24, 0x6D,
                0xB6, 0xDB, 0x49, 0x92, 0x24
            };

            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Write))
            {
                long fileSize = fs.Length;
                byte[] buffer = new byte[1024];
                int bufferLength = buffer.Length;

                for (int pass = 0; pass < patterns.Length; pass++)
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    Array.Fill(buffer, patterns[pass]);
                    for (long i = 0; i < fileSize; i += bufferLength)
                    {
                        fs.Write(buffer, 0, bufferLength);
                        reportProgress((int)((double)(pass * fileSize + i) / (patterns.Length * fileSize) * 100));
                    }
                }
            }
        }

        private void RestoreOriginalData(string filePath, byte[] originalData)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(originalData, 0, originalData.Length);
            }
        }
    }
}

