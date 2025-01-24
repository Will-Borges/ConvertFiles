using MinhaCdn.Integration.Convert.Models;

namespace MinhaCdn.Integration.Convert.Abstractions
{
    public interface IConvertService
    {
        Task<string> ConvertLogFileAsync(ConvertToMinhaCdnLog convertToMinhaCdnLog);
    }
}
