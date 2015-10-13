namespace Infrastructure.Net
{
    public interface IWebClientHelper
    {
        byte[] DownloadData(string address);
    }
}
