using MinhaCdn.Integration.Convert.Abstractions;
using MinhaCdn.Integration.Convert.Models;
using MinhaCdn.Integration.Convert.Models.Logs;

namespace MinhaCdn.Integration.Convert.Applications
{
    public class ConvertService : IConvertService
    {
        private readonly HttpClient _httpClient;

        public ConvertService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> ConvertLogFileAsync(ConvertToMinhaCdnLog convertToMinhaCdnLog)
        {
            string logContent;

            // Verificar se o caminho é uma URL
            if (Uri.TryCreate(convertToMinhaCdnLog.Path, UriKind.Absolute, out var uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                // Baixar o conteúdo do arquivo pela URL
                try
                {
                    logContent = await _httpClient.GetStringAsync($"{convertToMinhaCdnLog.Path}/{convertToMinhaCdnLog.FileNameInput}");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                // Caminho local: Ler o arquivo diretamente
                var pathWithFileInput = $@"{convertToMinhaCdnLog.Path}\\{convertToMinhaCdnLog.FileNameInput}";
                logContent = File.ReadAllText(pathWithFileInput);
            }

            // Divide o conteúdo do arquivo por linhas
            var lines = logContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // Criar o cabeçalho
            var header = new LogHeader();
            var logEntries = new List<string> { header.ToString() }; // Inclui o cabeçalho

            // Processa cada linha e adiciona ao log
            foreach (var line in lines)
            {
                var logEntry = ParseLogLine(line);
                var lineFormated = logEntry.ToAgoraFormat();
                logEntries.Add(lineFormated);
            }

            // Caminho completo do arquivo de saída
            var pathWithFileOutput = $@"{convertToMinhaCdnLog.PathOutput}\\{convertToMinhaCdnLog.FileNameOutput}";

            // Criar e escrever o arquivo com extensão ".formatted"
            string formattedFilePath = Path.ChangeExtension(pathWithFileOutput, ".formatted");
            File.WriteAllLines(formattedFilePath, logEntries);

            return "Arquivo convertido e salvo com sucesso!";
        }

        private LogAgoraInput ParseLogLine(string line)
        {
            // Divide a linha com base no delimitador '|'
            var parts = line.Split('|');

            // Cria o LogEntry de acordo com os dados
            return new LogAgoraInput
            {
                ResponseSize = int.Parse(parts[0]),     // Resposta
                StatusCode = int.Parse(parts[1]),       // Código de status
                CacheStatus = parts[2],                 // Status do cache (HIT, MISS, etc.)
                HttpMethod = parts[3].Split(' ')[0],    // Método HTTP (GET, POST)
                UriPath = parts[3].Split(' ')[1],       // Caminho da URI
                TimeTaken = double.Parse(parts[4])      // Tempo de processamento
            };
        }
    }

}
