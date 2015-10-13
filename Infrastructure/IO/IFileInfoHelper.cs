namespace Infrastructure.IO
{
    public interface IFileInfoHelper
    {
        void SetFile(string filePath);
        string FullName { get; set; }
        bool Exists { get; set; }
    }
}