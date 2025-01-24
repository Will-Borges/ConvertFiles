using MinhaCdn.Integration.Convert.Abstractions;
using MinhaCdn.Integration.Convert.Models;

namespace MinhaCdn.Integration.Convert.Applications
{
    public class ConvertService : IConvertService
    {
        public string ConvertLogFile(ConvertToMinhaCdnLog convertToMinhaCdnLog)
        {
            // Baixar o arquivo de log (simulando com uma leitura do arquivo local)
            string logContent = File.ReadAllText(convertToMinhaCdnLog.FilePathInput);

            // Quebra o conteúdo do arquivo por linha
            var lines = logContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            // Criar o cabeçalho
            var header = new LogHeader();
            var logEntries = new List<string> { header.ToString() };

            // Adiciona a primeira linha "1"
            //logEntries.Add("1");

            // Processa cada linha do arquivo
            var formattedLines = new List<string>();
            foreach (var line in lines)
            {
                var logEntry = ParseLogLine(line);
                var lineFormated = logEntry.ToAgoraFormat();

                logEntries.Add(lineFormated);
                formattedLines.Add(lineFormated);
            }

            // Escrever o arquivo no formato "Agora"
            File.WriteAllLines(convertToMinhaCdnLog.OutputPath, logEntries);

            // Criar e escrever o arquivo com extensão ".formatted"
            string formattedFilePath = Path.ChangeExtension(convertToMinhaCdnLog.OutputPath, ".formatted");
            File.WriteAllLines(formattedFilePath, formattedLines);

            return "Arquivo convertido e arquivo formatado gerados com sucesso!";
        }

        private LogEntry ParseLogLine(string line)
        {
            // Divide a linha com base no delimitador '|'
            var parts = line.Split('|');

            // Cria o LogEntry de acordo com os dados
            return new LogEntry
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

    // Representa uma entrada de log
    public class LogEntry
    {
        public int ResponseSize { get; set; }
        public int StatusCode { get; set; }
        public string CacheStatus { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public string UriPath { get; set; } = string.Empty;
        public double TimeTaken { get; set; }

        // Método para formatar no estilo 'Agora'
        public string ToAgoraFormat()
        {
            return $"\"MINHA CDN\" {HttpMethod} {StatusCode} {UriPath} {TimeTaken} {ResponseSize} {CacheStatus}";
        }
    }

    // Representa o cabeçalho do arquivo 'Agora'
    public class LogHeader
    {
        public string Version { get; set; } = "1.0";
        public DateTime Date { get; set; } = DateTime.Now;
        public string Fields { get; set; } = "provider http-method status-code uri-path time-taken response-size cache-status";

        public override string ToString()
        {
            return $"#Version: {Version}\n#Date: {Date:dd/MM/yyyy HH:mm:ss}\n#Fields: {Fields}";
        }
    }
}
