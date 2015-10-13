namespace Domain.SubtitleService.OpenSubtitles.Proxy.XmlRpc
{
    public enum StatusCode
    {
        EmptyCode = 0,
        Ok = 200,
        Unauthorized = 401,
        InvalidSubtitlesFormat = 402,
        SubHashesMismatch = 403,
        InvalidSubtitlesLanguage = 404,
        MandatoryParametersMissing = 405,
        NoSession = 406,
        DownloadLimitReached = 407,
        InvalidParameters = 408,
        MethodNotFound = 409,
        UnknownError = 410,
        InvalidUserAgent = 411,
        InvalidFormat = 412,
        InvalidImdbId = 413,
        UnknownUserAgent = 414,
        DisabledUserAgent = 415,
        InternalSubtitleValidationFailed = 416,
        ServiceUnavailable = 503
    }
}
