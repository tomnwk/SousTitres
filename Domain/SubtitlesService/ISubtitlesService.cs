using Domain.SubtitleService.OpenSubtitles;

namespace Domain.SubtitleService
{
    public interface ISubtitlesService<T>
    {
        DownloadedSubtitles DownloadSubtitle(OpenSubtitlesServiceParameters parameters);
    }
}
