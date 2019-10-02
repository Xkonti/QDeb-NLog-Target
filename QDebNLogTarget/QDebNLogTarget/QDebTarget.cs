using System.Linq;
using NLog;
using NLog.Config;
using NLog.Targets;
using RestSharp;

namespace QDebNLogTarget
{
    [Target("QDeb")]
    public class QDebTarget : TargetWithLayout
    {
        public QDebTarget()
        {
            HostUrl = "http://localhost:33990";
        }
        
        [RequiredParameter]
        public string HostUrl { get; set; }

        private RestClient? _client;
        
        private RestClient Client => _client ??= new RestClient(HostUrl);

        protected override void Write(LogEventInfo logEvent)
        {
            var logEntry = new QDebLogEntryDto
            {
                exception = logEvent.Exception?.ToString(),
                level = logEvent.Level?.Name?.ToLower() ?? LogLevel.Info.ToString(),
                message = logEvent.FormattedMessage,
                source = logEvent.LoggerName,
                tags = logEvent.Parameters?.Select(parameter => parameter.ToString()),
                timestamp = logEvent.TimeStamp
            };
            SendLogEntry(logEntry);
        }

        private void SendLogEntry(QDebLogEntryDto logEntry)
        {
            var request = new RestRequest("", Method.POST);
            request.AddJsonBody(logEntry);
            Client.ExecuteAsync(request, response => { });
        }
    }
}