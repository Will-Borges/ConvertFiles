namespace MinhaCdn.Integration.Convert.Abstractions
{
    public interface IDownloadFileWebService
    {
        Task<string> DownloadFileWeb(string path);
    }
}
