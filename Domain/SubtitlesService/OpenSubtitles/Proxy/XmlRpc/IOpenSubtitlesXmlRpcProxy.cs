namespace Domain.SubtitleService.OpenSubtitles.Proxy.XmlRpc
{
    using CookComputing.XmlRpc;

    [XmlRpcUrl("http://api.opensubtitles.org/xml-rpc")]
    public interface IOpenSubtitlesXmlRpcProxy : IXmlRpcProxy
    {
        [XmlRpcMethod("LogIn")]
        LoginResponse LogIn(string usename, string password, string language, string useragent);

        [XmlRpcMethod("SearchSubtitles")]
        SearchResponse SearchSubtitles(string token, SearchRequest[] requests);
    }
}
