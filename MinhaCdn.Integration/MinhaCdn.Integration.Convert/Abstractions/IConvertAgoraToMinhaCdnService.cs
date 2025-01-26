using MinhaCdn.Integration.Convert.Models.MinhaCdnToAgoraLog;

namespace MinhaCdn.Integration.Convert.Abstractions
{
    public interface IConvertAgoraToMinhaCdnService
    {
        Task<string> ExecuteConvertLogFileAsync(ConvertMinhaCdnToAgoraLog convertToAgoraLog);
    }
}
