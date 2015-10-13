namespace Infrastructure.Net
{
    using System;
    using System.Net;

    public sealed class WebClientHelper : IWebClientHelper, IDisposable
    {
        private readonly WebClient _webClient;

        public WebClientHelper()
        {
            _webClient = new WebClient();
        }

        public byte[] DownloadData(string address)
        {
            return _webClient.DownloadData(address);
        }

        public void Dispose()
        {
            _webClient.Dispose();
        }
    }
}
