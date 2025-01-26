using MinhaCdn.Integration.Convert.Abstractions;
using MinhaCdn.Integration.Convert.Models.MinhaCdnToAgoraLog;

namespace MinhaCdn.Integration.Convert.Applications.MinhaCdnToAgoraLog
{
    public class ConvertMinhaCdnToAgoraLogService : IConvertAgoraToMinhaCdnService
    {
        public ConvertMinhaCdnToAgoraLogService() { }


        public async Task<string> ExecuteConvertLogFileAsync(ConvertMinhaCdnToAgoraLog convertToAgoraLog)
        {
            var logContent = await GetLogContent(convertToAgoraLog);

            var linesFormateds = FormateToAgoraLines(logContent);

            var result = BuildNewFileOutput(convertToAgoraLog, linesFormateds);
            return result;
        }

        private string BuildNewFileOutput(ConvertMinhaCdnToAgoraLog convertToAgoraLog, string[] linesFormateds)
        {
            var pathWithFileOutput = $@"{convertToAgoraLog.PathOutput}\\{convertToAgoraLog.FileNameOutput}";

            var result = WriteInNewFile(convertToAgoraLog, pathWithFileOutput, linesFormateds);
            return result;
        }

        private string WriteInNewFile(ConvertMinhaCdnToAgoraLog convertToAgoraLog, string pathWithFileOutput, string[] linesFormateds)
        {
            try
            {
                string formattedFilePath = Path.ChangeExtension(pathWithFileOutput, convertToAgoraLog.ExtensionOutPut);
                File.WriteAllLines(formattedFilePath, linesFormateds);

                return "Arquivo convertido e salvo com sucesso!";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string[] FormateToAgoraLines(string logContent)
        {
            var lines = SeparateByLines(logContent);
            var linesFormateds = new List<string>();

            var headerFormatted = BuilHeader();
            linesFormateds.Add(headerFormatted);

            foreach (var line in lines)
            {
                var logAgoraOutput = new LogAgoraOutput();
                logAgoraOutput.ParseToAgora(line);

                var lineFormated = logAgoraOutput.ToAgoraFormatStyle();
                linesFormateds.Add(lineFormated);
            }

            return linesFormateds.ToArray();
        }

        private string BuilHeader()
        {
            var header = new LogAgoraHeader();
            var headerFormatted = header.GetFormatted();

            return headerFormatted;
        }

        private string[] SeparateByLines(string logContent)
        {
            var lines = logContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            return lines;
        }

        private async Task<string> GetLogContent(ConvertMinhaCdnToAgoraLog convertToAgoraLog)
        {
            string logContent = "";

            if (CheckIfPathIsUrl(convertToAgoraLog))
            {
                var downloadFileWebService = new DownloadFileWebService();

                var pathFile = $"{convertToAgoraLog.Path}/{convertToAgoraLog.FileNameInput}";

                logContent = await downloadFileWebService.DownloadFileWeb(pathFile);
                return logContent;
            }
            else
            {
                var pathWithFileInput = $@"{convertToAgoraLog.Path}\\{convertToAgoraLog.FileNameInput}";
                logContent = File.ReadAllText(pathWithFileInput);
            }

            return logContent;
        }

        private bool CheckIfPathIsUrl(ConvertMinhaCdnToAgoraLog convertToAgoraLog)
        {
            return Uri.TryCreate(convertToAgoraLog.Path, UriKind.Absolute, out var uriResult) &&
                        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }

}
