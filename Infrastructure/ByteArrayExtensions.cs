namespace Infrastructure
{
    using System;
    using System.IO;
    using System.IO.Compression;

    public static class ByteArrayExtensions
    {
        public static byte[] Combine(this byte[] first, byte[] second)
        {
            byte[] result = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, result, 0, first.Length);
            Buffer.BlockCopy(second, 0, result, first.Length, second.Length);
            return result;
        }

        public static byte[] Decompress(this byte[] gzippedBuffer)
        {
            using (var memoryStream = new MemoryStream())
            {
                memoryStream.Write(gzippedBuffer, 0, gzippedBuffer.Length);
                memoryStream.Position = 0;
                using (var decompressionStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    using (var decompressedFileStream = new MemoryStream())
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        return decompressedFileStream.ToArray();
                    }
                }
            }
        }
    }
}
