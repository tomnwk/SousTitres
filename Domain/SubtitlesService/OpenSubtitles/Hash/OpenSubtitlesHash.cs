namespace Domain.SubtitleService.OpenSubtitles.Hash
{
    using System;
    using System.IO;
    using Infrastructure;
    using Infrastructure.IO;

    public class OpenSubtitlesHash : IOpenSubtitlesHash
    {
        private readonly IFileHelper _fileHelper;
        private readonly IFileInfoHelper _fileInfoHelper;
        private const int DefaultDataChunckLength = 65536;

        public OpenSubtitlesHash(IFileHelper fileHelper, IFileInfoHelper fileInfoHelper)
        {
            _fileHelper = fileHelper;
            _fileInfoHelper = fileInfoHelper;
            DataChunckLength = DefaultDataChunckLength;
        }

        public string ComputeHash(SampleData inputData)
        {
            var lhash = (ulong) inputData.FileLength;
            for (var i = 0; i < inputData.DataBytes.Length; i += 8)
                unchecked
                {
                    lhash += BitConverter.ToUInt64(inputData.DataBytes, i);
                }

            return lhash.ToString("x16");
        }

        public int DataChunckLength { get; set; }

        public SampleData ExtractSampleData(string filePath)
        {
            _fileInfoHelper.SetFile(filePath);

            if (!_fileInfoHelper.Exists)
            {
                throw new FileNotFoundException("The file doesn't exist");
            }

            using (var fileStream = _fileHelper.Open(_fileInfoHelper.FullName, FileMode.Open))
            {
                var firstPosition = 0;
                var lastPosition = fileStream.Length - DataChunckLength;

                var firstChunck = fileStream.Extract(firstPosition, DataChunckLength);
                var lastChunck = fileStream.Extract(lastPosition, DataChunckLength);

                return new SampleData(fileStream.Length, firstChunck.Combine(lastChunck));
            }
        }
    }
}
