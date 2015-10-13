namespace Domain.SubtitleService.OpenSubtitles
{
    using System;
    using System.Linq;
    using Proxy.Factory;
    using Proxy.XmlRpc;
    using Infrastructure;
    using Infrastructure.Net;

    public class OpenSubtitlesService : ISubtitlesService<OpenSubtitlesServiceParameters>
    {
        private readonly IOpenSubtitlesProxyFactory _proxyFactory;
        private readonly IWebClientHelper _webClientHelper;

        private IOpenSubtitlesProxy _proxy;

        private OpenSubtitlesServiceParameters _serviceParameters;

        public OpenSubtitlesService(IOpenSubtitlesProxyFactory proxyFactory, IWebClientHelper webClientHelper)
        {
            _proxyFactory = proxyFactory;
            _webClientHelper = webClientHelper;
        }

        public DownloadedSubtitles DownloadSubtitle(OpenSubtitlesServiceParameters parameters)
        {
            _proxy = _proxyFactory.CreateProxy();
            _serviceParameters = parameters;

            LoginToService();
            var foundSubtitles = SearchSubtitles().FoundSubtitles.First();
            var compressedSubtitles = DownloadCompressedSubtitles(foundSubtitles.DownloadUrl);

            return new DownloadedSubtitles(compressedSubtitles.Decompress(), foundSubtitles.Format);
        }

        private void LoginToService()
        {
            _proxy.Login(_serviceParameters);
            if (_proxy.LoginResponse.Status != StatusCode.Ok || string.IsNullOrEmpty(_proxy.LoginResponse.Token))
            {
                throw new Exception("Login failed");
            }
        }

        private SearchResponse SearchSubtitles()
        {
            var result = _proxy.Search(_serviceParameters);
            if (!result.FoundSubtitles.Any())
            {
                throw new Exception("No subtitles found");
            }

            return result;
        }

        private byte[] DownloadCompressedSubtitles(string gzippedSubtitleUrl)
        {
            return _webClientHelper.DownloadData(gzippedSubtitleUrl);
        }
    }
}
