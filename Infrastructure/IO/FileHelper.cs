namespace Infrastructure.IO
{
    using System.IO;

    public class FileHelper : IFileHelper
    {
        public Stream Open(string filePath, FileMode mode)
        {
            return File.Open(filePath, mode);
        }

        public void WriteAllBytes(string destinationFile, byte[] data)
        {
            File.WriteAllBytes(destinationFile, data);
        }
    }
}
