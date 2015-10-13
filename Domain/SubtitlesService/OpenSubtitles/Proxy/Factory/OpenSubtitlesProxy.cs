namespace Domain.SubtitleService.OpenSubtitles.Proxy.Factory
{
    using XmlRpc;

    public class OpenSubtitlesProxy: IOpenSubtitlesProxy
    {
        private readonly IOpenSubtitlesXmlRpcProxy _proxy;

        public OpenSubtitlesProxy(IOpenSubtitlesXmlRpcProxy proxy)
        {
            _proxy = proxy;
        }

        public LoginResponse LoginResponse { get; set; }

        public void Login(OpenSubtitlesServiceParameters parameters)
        {
            LoginResponse = _proxy.LogIn(string.Empty, string.Empty, parameters.Language, parameters.UserAgent);
        }

        public SearchResponse Search(OpenSubtitlesServiceParameters parameters)
        {
            var searchRequest = new[] { new SearchRequest {
                                                UserToken = LoginResponse.Token,
                                                LanguageId = parameters.Language,
                                                MovieHash = parameters.VideoFileHash
                                            }
                                     };
            return _proxy.SearchSubtitles(searchRequest[0].UserToken, searchRequest);
        }

    }
}

