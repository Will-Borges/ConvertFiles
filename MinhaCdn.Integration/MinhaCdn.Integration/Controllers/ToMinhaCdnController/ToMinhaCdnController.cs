using Microsoft.AspNetCore.Mvc;
using MinhaCdn.Integration.Convert.Applications;
using MinhaCdn.Integration.Convert.Applications.MinhaCdnToAgoraLog;
using MinhaCdn.Integration.Convert.Models.MinhaCdnToAgoraLog;

namespace MinhaCdn.Integration.Controllers.ConvertAgoraToMinhaCdnController
{
    [Route("v1/LogConversionSimulation")]
    [ApiController]
    public class ToMinhaCdnController : ControllerBase
    {

        public ToMinhaCdnController()
        {

        }

        [HttpPost]
        [Route("post-logs")]
        public async Task<IActionResult> ConvertLogs([FromBody] ConvertMinhaCdnToAgoraLog convertToMinhaCdnLog)
        {
            try
            {
                var convertService = new ConvertMinhaCdnToAgoraLogService();

                var response = await convertService.ExecuteConvertLogFileAsync(convertToMinhaCdnLog);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
