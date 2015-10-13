namespace Domain.SubtitleService.OpenSubtitles
{
    using System.Linq;

    public struct DownloadedSubtitles
    {
        public readonly byte[] DataBytes;
        public readonly string Format;

        public DownloadedSubtitles(byte[] dataBytes, string format)
        {
            DataBytes = dataBytes;
            Format = format;
        }

        public bool Equals(DownloadedSubtitles other)
        {
            return other.Format == Format && other.DataBytes.SequenceEqual(DataBytes);
        }

        public override bool Equals(object o)
        {
            if (!(o is DownloadedSubtitles))
            {
                return false;
            }

            var otherSampleData = (DownloadedSubtitles)o;

            return otherSampleData.Format == Format && otherSampleData.DataBytes.SequenceEqual(DataBytes);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((DataBytes != null ? DataBytes.GetHashCode() : 0)*397) ^ (Format != null ? Format.GetHashCode() : 0);
            }
        }
    }
}
