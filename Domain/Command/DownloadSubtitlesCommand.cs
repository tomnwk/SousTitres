namespace Domain.Command
{
    using System;
    using System.IO;
    using SubtitleService.OpenSubtitles;
    using SubtitleService.OpenSubtitles.Hash;
    using Infrastructure.IO;

    public class DownloadSubtitlesCommand : ICommand<DownloadSubtitlesCommandArgs>
    {
        private readonly OpenSubtitlesService _openSubtitlesServiceService;
        private readonly IOpenSubtitlesHash _openSubtitlesHash;
        private readonly IFileHelper _fileHelper;

        public DownloadSubtitlesCommand(OpenSubtitlesService openSubtitlesServiceService, IOpenSubtitlesHash openSubtitlesHash, IFileHelper fileHelper)
        {
            _openSubtitlesServiceService = openSubtitlesServiceService;
            _openSubtitlesHash = openSubtitlesHash;
            _fileHelper = fileHelper;
        }

        public void Execute(DownloadSubtitlesCommandArgs args)
        {
            var sampleData = _openSubtitlesHash.ExtractSampleData(args.FilePath);
            var videoHash = ComputeHash(sampleData);
            var subtitleData = _openSubtitlesServiceService.DownloadSubtitle(new OpenSubtitlesServiceParameters {
                VideoFileHash = videoHash,
                Language = args.Language,
                UserAgent = args.UserAgent
            });

            if (subtitleData.DataBytes != null)
            {
                CopySubtitleDataToFile(subtitleData, args.FilePath);
            }
        }

        private string ComputeHash(SampleData inputData)
        {
            var result = _openSubtitlesHash.ComputeHash(inputData);
            if (string.IsNullOrEmpty(result))
            {
                throw new Exception("Wrong hash");
            }

            return result;
        }

        private void CopySubtitleDataToFile(DownloadedSubtitles downloadedSubtitle, string filePath)
        {
            var destinationFile = Path.Combine(
                Path.GetDirectoryName(filePath),
                $"{Path.GetFileNameWithoutExtension(filePath)}.{downloadedSubtitle.Format}");

            _fileHelper.WriteAllBytes(destinationFile, downloadedSubtitle.DataBytes);
        }
    }
}
