using CookComputing.XmlRpc;
using Domain.SubtitleService.OpenSubtitles.Proxy.XmlRpc;

namespace Domain.SubtitleService.OpenSubtitles.Proxy.Factory
{
    public class OpenSubtitlesProxyFactory: IOpenSubtitlesProxyFactory
    {
        public virtual IOpenSubtitlesProxy CreateProxy()
        {
            var proxy = XmlRpcProxyGen.Create<IOpenSubtitlesXmlRpcProxy>();
            return new OpenSubtitlesProxy(proxy);
        }
    }
}
