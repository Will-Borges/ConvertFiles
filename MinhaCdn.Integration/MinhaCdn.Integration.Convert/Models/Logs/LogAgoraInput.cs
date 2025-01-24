namespace MinhaCdn.Integration.Convert.Models.Logs
{
    public class LogAgoraInput
    {
        public int ResponseSize { get; set; }
        public int StatusCode { get; set; }
        public string CacheStatus { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public string UriPath { get; set; } = string.Empty;
        public double TimeTaken { get; set; }


        // Método para formatar no estilo 'Agora'
        public string ToAgoraFormat()
        {
            var formatAgora = $"\"MINHA CDN\" {HttpMethod} {StatusCode} {UriPath} {TimeTaken} {ResponseSize} {CacheStatus}";
            return formatAgora;
        }
    }
}
