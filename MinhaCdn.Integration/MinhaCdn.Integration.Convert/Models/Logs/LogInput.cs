namespace MinhaCdn.Integration.Convert.Models.Logs
{
    public class LogInput
    {
        public int ResponseSize { get; set; }                     // Tamanho da resposta
        public int StatusCode { get; set; }                       // Código de status HTTP
        public string CacheStatus { get; set; } = string.Empty;   // Status do cache (HIT, MISS, etc.)
        public string HttpMethod { get; set; } = string.Empty;    // Método HTTP (GET, POST, etc.)
        public string UriPath { get; set; } = string.Empty;       // Caminho da URI
        public double TimeTaken { get; set; }                     // Tempo de processamento (arredondado)

    }
}
