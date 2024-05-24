using System;
using System.IO;

namespace SecureDelete
{
    public class SecureDeleter
    {
        public void DeleteFile(string filePath)
        {
            File.Delete(filePath);
        }
    }
}

