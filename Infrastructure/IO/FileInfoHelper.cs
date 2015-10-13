namespace Infrastructure.IO
{
    using System.IO;

    public class FileInfoHelper: IFileInfoHelper
    {
        private FileInfo _fileInfo;
        private string _fullName;

        private bool _exists;

        public void SetFile(string path)
        {
            _fileInfo = new FileInfo(path);
        }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_fullName))
                {
                    _fullName = _fileInfo.FullName;
                }

                return _fullName;
            }

            set
            {
                _fullName = value;
            }
        }

        public bool Exists
        {
            get
            {
                _exists = _fileInfo.Exists;
                return _exists;
            }

            set
            {
                _exists = value;
            }
        }
    }
}
