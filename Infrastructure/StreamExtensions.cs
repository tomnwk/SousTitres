namespace Infrastructure
{
    using System.IO;

    public static class StreamExtensions
    {
        public static byte[] Extract(this Stream stream, long position, int length)
        {
            var result = new byte[length];
            stream.Position = position;
            stream.Read(result, 0, length);

            return result;
        }
    }
}
