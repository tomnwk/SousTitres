namespace Domain.SubtitleService.OpenSubtitles.Proxy.Factory
{
    using XmlRpc;

    public interface IOpenSubtitlesProxy
    {
        LoginResponse LoginResponse { get; set; }
        void Login(OpenSubtitlesServiceParameters parameters);
        SearchResponse Search(OpenSubtitlesServiceParameters parameters);
    }
}