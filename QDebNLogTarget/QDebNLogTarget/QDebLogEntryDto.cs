using System;
using System.Collections.Generic;
using NLog;

namespace QDebNLogTarget
{
    public class QDebLogEntryDto
    {
        public string? exception { get; set; }
        public string level { get; set; } = LogLevel.Info.ToString();
        public string? message { get; set; }
        public string source { get; set; } = "QDeb NLog Target";
        public IEnumerable<string>? tags { get; set; }
        public DateTime timestamp { get; set; } = DateTime.Now;
    }
}