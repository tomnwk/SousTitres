namespace Domain.Tests
{
    using System.IO;
    using SubtitleService.OpenSubtitles.Hash;
    using Infrastructure.IO;
    using Moq;
    using NUnit.Framework;

    public class OpenSubtitlesHashTests
    {
        [Test]
        public void When_sampleData_is_extracted_from_file_Then_returns_expected_sampleData()
        {
            var inputData = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            var expectedData = new SampleData(inputData.Length, new byte[] { 1, 2, 7, 8 });

            var fakeFileHelper = new Mock<IFileHelper>();
            fakeFileHelper.Setup(x => x.Open(It.IsAny<string>(), It.IsAny<FileMode>())).Returns(new MemoryStream(inputData));

            var fakeFileInfoHelper = new Mock<IFileInfoHelper>();
            fakeFileInfoHelper.SetupProperty(x => x.Exists, true);

            var hashHelper = new OpenSubtitlesHash(fakeFileHelper.Object, fakeFileInfoHelper.Object)
            {
                DataChunckLength = 2
            };
            var result = hashHelper.ExtractSampleData("c:\\dummy.avi");

            Assert.AreEqual(expectedData, result);
        }

        [Test]
        public void When_sampleData_is_extracted_from_file_but_file_does_not_exist_Then_throws_FileNotFoundException()
        {
            var fakeFileHelper = new Mock<IFileHelper>();

            var fakedFileInfoHelper = new Mock<IFileInfoHelper>();
            fakedFileInfoHelper.SetupProperty(x => x.Exists, false);

            var hashHelper = new OpenSubtitlesHash(fakeFileHelper.Object, fakedFileInfoHelper.Object);

            Assert.Throws<FileNotFoundException>(() => hashHelper.ExtractSampleData("c:\\dummy.avi"));
        }

        [Test]
        public void When_bytes_of_data_are_hashed_using_opensubtitles_algorithm_Then_returns_expected_hash()
        {
            var inputData = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedHash = "18161412100e0c1a";

            var fakeFileHelper = new Mock<IFileHelper>();
            var fakeFileInfoHelper = new Mock<IFileInfoHelper>();
            fakeFileInfoHelper.SetupProperty(x => x.Exists, true);

            var hashHelper = new OpenSubtitlesHash(fakeFileHelper.Object, fakeFileInfoHelper.Object);
            var sampleData = new SampleData(inputData.Length, inputData);

            var hash = hashHelper.ComputeHash(sampleData);

            StringAssert.AreEqualIgnoringCase(expectedHash, hash);
        }
    }
}
