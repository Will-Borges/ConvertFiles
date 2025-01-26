namespace MinhaCdn.Integration.Convert.Models.MinhaCdnToAgoraLog
{
    public class ConvertMinhaCdnToAgoraLog
    {
        public string FileNameInput { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        public string PathOutput { get; set; } = string.Empty;
        public string FileNameOutput { get; set; } = string.Empty;
        public string ExtensionOutPut { get; set; } = ".txt";
    }
}
