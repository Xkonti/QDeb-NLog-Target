# QDeb-NLog-Target

The NLog target for the QDeb application.

## Installation

You can get the NuGet package from [nuget.org](https://www.nuget.org/packages/QDeb_NLog_Target/).

You can install the NuGet package via the package manager:

```
Install-Package QDeb_NLog_Target -Version 0.1.0
```

You can install the NuGet package via .NET CLI:

```
dotnet add package QDeb_NLog_Target --version 0.1.0
```

You can add a reference to the NuGet package:

```
<PackageReference Include="QDeb_NLog_Target" Version="0.1.0" />
```

You can add a reference to the NuGet package via Paket CLI:

```
paket add QDeb_NLog_Target --version 0.1.0
```

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