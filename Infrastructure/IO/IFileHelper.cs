namespace Infrastructure.IO
{
    using System.IO;

    public interface IFileHelper
    {
        Stream Open(string filePath, FileMode mode);
        void WriteAllBytes(string destinationFile, byte[] data);
    }
}