namespace Domain.SubtitleService.OpenSubtitles.Hash
{
    using System.Linq;

    public struct SampleData
    {
        public readonly long FileLength;
        public readonly byte[] DataBytes;

        public SampleData(long fileLength, byte[] dataBytes)
        {
            FileLength = fileLength;
            DataBytes = dataBytes;
        }

        public bool Equals(SampleData other)
        {
            return other.FileLength == FileLength && other.DataBytes.SequenceEqual(DataBytes);
        }

        public override bool Equals(object o)
        {
            if (!(o is SampleData))
            {
                return false;
            }

            var otherSampleData = (SampleData)o;
            
            return otherSampleData.FileLength == FileLength && otherSampleData.DataBytes.SequenceEqual(DataBytes);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (FileLength.GetHashCode()*397) ^ (DataBytes != null ? DataBytes.GetHashCode() : 0);
            }
        }
    }
}

