namespace MinhaCdn.Integration.Convert.Models.MinhaCdnToAgoraLog
{
    public class LogAgoraOutput
    {
        public int ResponseSize { get; set; }
        public int StatusCode { get; set; }
        public string CacheStatus { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public string UriPath { get; set; } = string.Empty;
        public double ProcessingTime { get; set; }

        public LogAgoraOutput() { }

        public LogAgoraOutput(
            int responseSize,
            int statusCode, 
            string cacheStatus,
            string httpMethod,
            string uriPath, 
            double processingTime)
        {
            ResponseSize = responseSize;
            StatusCode = statusCode;
            CacheStatus = cacheStatus;  // Status do cache (HIT, MISS, etc.)
            HttpMethod = httpMethod;
            UriPath = uriPath;
            ProcessingTime = processingTime;
        }


        public string ToAgoraFormatStyle()
        {
            var formatAgora = $"\"MINHA CDN\" {HttpMethod} {StatusCode} {UriPath} {ProcessingTime} {ResponseSize} {CacheStatus}";
            return formatAgora;
        }

        public void ParseToAgora(string line)
        {
            var parts = line.Split('|');

            ResponseSize = int.Parse(parts[0]);
            StatusCode = int.Parse(parts[1]);
            CacheStatus = parts[2];  // Status do cache (HIT, MISS, etc.)
            HttpMethod = parts[3].Split(' ')[0];
            UriPath = parts[3].Split(' ')[1];
            ProcessingTime = double.Parse(parts[4]);
        }
    }
}
