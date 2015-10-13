namespace Infrastructure.Tests
{
    using System.IO;
    using NUnit.Framework;

    public class StreamExtensionsTests
    {
        [Test]
        public void When_extract_piece_of_array_Then_returns_expected_piece_of_data()
        {
            var inputData = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedResult = new byte[] { 2, 3, 4, 5 };
            using (var memoryStream = new MemoryStream(inputData))
            {
                Assert.AreEqual(memoryStream.Extract(2, 4), expectedResult);
            }
        }
    }
}
