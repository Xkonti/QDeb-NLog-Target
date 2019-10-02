# QDeb-NLog-Target

The NLog target for the QDeb application.

## Usage:

Register just like any other NLog target and provide the QDeb url:

```C#
var qDebTarget = new QDebTarget {HostUrl = "http://localhost:33990"};
var config = LogManager.Configuration ?? new LoggingConfiguration();
try
{
    config.AddRule(LogLevel.Trace, LogLevel.Fatal, qDebTarget);
    LogManager.Configuration = config;
}
catch (Exception)
{
    qDebTarget.Dispose();
}
```

When adding logs, arguments will be added as tags:

```C#
private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

// (...)

var linkId = 123;
Logger.Warn($"Tried to access non-existend link with id: {linkId}", "link", "data corruption");

// Resulted message: 'Tried to access non-existend link with id: 123'
// Tags: ['link', 'data corruption']
```