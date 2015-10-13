namespace Domain.Tests
{
    using System;
    using global::Tests.Common;
    using SubtitleService.OpenSubtitles;
    using SubtitleService.OpenSubtitles.Proxy.Factory;
    using SubtitleService.OpenSubtitles.Proxy.XmlRpc;
    using Infrastructure.Net;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class OpenSubtitlesServiceTests
    {
        private OpenSubtitlesServiceParameters _serviceParameters;

        [SetUp]
        public void Setup()
        {
            _serviceParameters = new OpenSubtitlesServiceParameters
            {
                VideoFileHash = "dummyHash",
                Language = "eng",
                UserAgent = "OSTestUserAgent"
            };
        }

        [Test]
        public void When_Login_Response_is_not_OK_Then_throws_exception()
        {
            var fakeProxy = new Mock<IOpenSubtitlesProxy>();
            fakeProxy.SetupProperty(x => x.LoginResponse, new LoginResponse { Status = StatusCode.ServiceUnavailable, Token = "" });
            var fakeOpenSubtitleProxyFactory = new Mock<OpenSubtitlesProxyFactory>();
            fakeOpenSubtitleProxyFactory.Setup(x => x.CreateProxy()).Returns(fakeProxy.Object);
            var fakeWebClient = new Mock<IWebClientHelper>();

            var service = new OpenSubtitlesService(fakeOpenSubtitleProxyFactory.Object, fakeWebClient.Object);
            Assert.Throws<Exception>(() => service.DownloadSubtitle(_serviceParameters));
        }

        [Test]
        public void When_search_request_returns_no_result_Then_throws_exception()
        {
            var fakeProxy = new Mock<IOpenSubtitlesProxy>();
            fakeProxy.SetupProperty(x => x.LoginResponse, new LoginResponse { Status = StatusCode.Ok, Token = "validToken" });
            fakeProxy.Setup(x => x.Search(It.IsAny<OpenSubtitlesServiceParameters>())).Returns(() => new SearchResponse
            {
                Status = StatusCode.Ok,
                FoundSubtitles = new SubtitleInfo[] { }
            });
            var fakeOpenSubtitleProxyFactory = new Mock<OpenSubtitlesProxyFactory>();
            fakeOpenSubtitleProxyFactory.Setup(x => x.CreateProxy()).Returns(fakeProxy.Object);
            var fakeWebClient = new Mock<IWebClientHelper>();

            var service = new OpenSubtitlesService(fakeOpenSubtitleProxyFactory.Object, fakeWebClient.Object);
            Assert.Throws<Exception>(() => service.DownloadSubtitle(_serviceParameters));
        }

        [Test]
        public void When_search_request_returns_searchReponse_with_found_subtitles_Then_service_returns_expected_subtitles()
        {
            var expectedUncompressedResult = new byte[] { 0, 1, 2, 3, 4 };
            var expectedFormat = "srt";
            var expectedDownloadedSubtitles = new DownloadedSubtitles(expectedUncompressedResult, expectedFormat);
            var compressedExpectedResult = Compression.CompressBuffer(expectedUncompressedResult);

            var fakeProxy = new Mock<IOpenSubtitlesProxy>();
            fakeProxy.SetupProperty(x => x.LoginResponse, new LoginResponse { Status = StatusCode.Ok, Token = "validToken" });
            fakeProxy.Setup(x => x.Search(It.IsAny<OpenSubtitlesServiceParameters>())).Returns(() => new SearchResponse
            {
                Status = StatusCode.Ok,
                FoundSubtitles = new[] { new SubtitleInfo { Format = expectedFormat } }
            });

            var fakeOpenSubtitleProxyFactory = new Mock<OpenSubtitlesProxyFactory>();
            fakeOpenSubtitleProxyFactory.Setup(x => x.CreateProxy()).Returns(fakeProxy.Object);
            var fakeWebClient = new Mock<IWebClientHelper>();
            fakeWebClient.Setup(x => x.DownloadData(It.IsAny<string>())).Returns(compressedExpectedResult);

            var service = new OpenSubtitlesService(fakeOpenSubtitleProxyFactory.Object, fakeWebClient.Object);
            var downloadedSubtitles = service.DownloadSubtitle(_serviceParameters);

            Assert.AreEqual(expectedDownloadedSubtitles, downloadedSubtitles);
        }
    }
}
