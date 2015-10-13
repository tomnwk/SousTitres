namespace Infrastructure.Tests
{
    using global::Tests.Common;

    using NUnit.Framework;

    public class ByteArrayExtensionsTests
    {
        [Test]
        public void When_two_arrays_of_bytes_are_combined_Then_returns_the_expected_combined_array()
        {
            var firstArray = new byte[] { 0, 1, 2, 3, 4 };
            var secondArray = new byte[] { 5, 6, 7, 8, 9 };
            var expectedResult = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Assert.AreEqual(expectedResult, firstArray.Combine(secondArray));
        }

        [Test]
        public void When_a_compressed_array_of_bytes_is_decompressed_Then_returns_expected_decompressed_array()
        {
            var expectedResult = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var compressedInputData = Compression.CompressBuffer(expectedResult);

            Assert.AreEqual(expectedResult, compressedInputData.Decompress());
        }

    }
}
