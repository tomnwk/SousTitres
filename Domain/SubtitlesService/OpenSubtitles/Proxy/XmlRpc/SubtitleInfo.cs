namespace Domain.SubtitleService.OpenSubtitles.Proxy.XmlRpc
{
    using CookComputing.XmlRpc;

    public struct SubtitleInfo
    {
        [XmlRpcMember("IDSubtitle")]
        public string Id { get; set; }

        [XmlRpcMember("SubLanguageID")]
        public string LanguageId { get; set; }

        [XmlRpcMember("ISO639")]
        public string Iso639 { get; set; }

        [XmlRpcMember("SubFormat")]
        public string Format { get; set; }

        [XmlRpcMember("SubDownloadLink")]
        public string DownloadUrl { get; set; }
    }
}