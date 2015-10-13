namespace Domain.Command
{
    public class DownloadSubtitlesCommandArgs
    {
        public string FilePath { get; set; }

        public string Language { get; set; }

        public string UserAgent { get; set; }
    }
}
