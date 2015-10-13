namespace Domain.SubtitleService.OpenSubtitles.Proxy.XmlRpc
{
    using CookComputing.XmlRpc;

    public struct SearchRequest
    {
        public string UserToken { get; set; }

        [XmlRpcMember("sublanguageid")]
        public string LanguageId { get; set; }

        [XmlRpcMember("moviehash")]
        public string MovieHash { get; set; }
    }
}