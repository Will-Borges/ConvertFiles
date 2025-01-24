using MinhaCdn.Integration.Convert.Models;

namespace MinhaCdn.Integration.Convert.Abstractions
{
    public interface IConvertService
    {
        string ConvertLogFile(ConvertToMinhaCdnLog convertToMinhaCdnLog);
    }
}
