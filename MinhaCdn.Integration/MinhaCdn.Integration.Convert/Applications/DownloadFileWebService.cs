using MinhaCdn.Integration.Convert.Abstractions;

namespace MinhaCdn.Integration.Convert.Applications
{
    public class DownloadFileWebService : IDownloadFileWebService
    {
        private readonly HttpClient _httpClient;

        public DownloadFileWebService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> DownloadFileWeb(string path)
        {
            try
            {
                var logContent = await _httpClient.GetStringAsync(path);
                return logContent;
            }
            catch (Exception)
            {
                throw new Exception("Sem permissão para acessar o arquivo ou arquivo não encontrado");
            }
        }
    }
}
