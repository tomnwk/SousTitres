namespace Domain.SubtitleService.OpenSubtitles.Proxy.XmlRpc
{
    using System;
    using System.Linq;
    using CookComputing.XmlRpc;

    public struct LoginResponse
    {
        private StatusCode _status;

        [XmlRpcMember("token")]
        public string Token { get; set; }

        [XmlRpcMember("status")]
        public string RawStatus { get; set; }

        [XmlRpcMissingMapping(MappingAction.Ignore)]
        public StatusCode Status
        {
            get
            {
                if (_status == StatusCode.EmptyCode && !string.IsNullOrEmpty(RawStatus))
                {
                    _status = (StatusCode)Enum.Parse(typeof(StatusCode), RawStatus.Split(' ').First());
                }

                return _status;
            }
            set
            {
                _status = value;
            }
        }
    }
}
