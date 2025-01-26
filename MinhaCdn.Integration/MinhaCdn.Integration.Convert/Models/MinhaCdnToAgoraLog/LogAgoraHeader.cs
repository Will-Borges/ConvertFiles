namespace MinhaCdn.Integration.Convert.Models.MinhaCdnToAgoraLog
{
    public class LogAgoraHeader
    {
        public string Version { get; set; } = "1.0";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Fields { get; set; } = "provider http-method status-code uri-path time-taken response-size cache-status";

        public string GetFormatted()
        {
            return $"#Version: {Version}\n#Date: {Date:dd/MM/yyyy HH:mm:ss}\n#Fields: {Fields}";
        }
    }
}
