namespace Tests.Common
{
    using System.IO;
    using System.IO.Compression;

    public static class Compression
    {
        public static byte[] CompressBuffer(byte[] buffer)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzip.Write(buffer, 0, buffer.Length);
                }
                return memoryStream.ToArray();
            }
        }
    }
}
