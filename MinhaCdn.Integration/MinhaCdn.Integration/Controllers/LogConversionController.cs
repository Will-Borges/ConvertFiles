using Microsoft.AspNetCore.Mvc;
using MinhaCdn.Integration.Convert.Applications;
using MinhaCdn.Integration.Convert.Models;

namespace MinhaCdn.Integration.Controllers
{
    [Route("v1/LogConversionSimulation")]
    [ApiController]
    public class LogConversionController : ControllerBase
    {

        public LogConversionController()
        {

        }

        [HttpPost]
        [Route("post-logs")]
        public async Task<IActionResult> ConvertLogs([FromBody] ConvertToMinhaCdnLog convertToMinhaCdnLog)
        {
            try
            {
                var convertService = new ConvertService();

                var response = convertService.ConvertLogFile(convertToMinhaCdnLog);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
