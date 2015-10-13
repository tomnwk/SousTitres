namespace Domain.SubtitleService.OpenSubtitles.Hash
{
    public interface IOpenSubtitlesHash
    {
        string ComputeHash(SampleData inputData);

        SampleData ExtractSampleData(string filePath);
    }
}
