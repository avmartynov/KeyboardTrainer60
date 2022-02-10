using System.Diagnostics;
using System.Reflection;
using NLog;
using Twidlle.Library.Hosting;
using Twidlle.Library.Utility;

namespace Twidlle.Library.Testing.Diagnostics;

public sealed class TestRun : IDisposable
{
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
    private readonly Stopwatch _stopwatch = Stopwatch.StartNew();

    public TestRun()
    {
        var runnerName = Assembly.GetEntryAssembly()?.GetName().Name;
        var testDir = PathExtensions.GetAppDirectory();
        _logger.Info($"Test run starting..., TestDirectory: '{testDir}', Runner: {runnerName}{Environment.NewLine}");
    }

    public void Dispose()
    {
        _logger.Info($"Test run finished. ElapsedMilliseconds: {_stopwatch.ElapsedMilliseconds:F}{Environment.NewLine}");
    }

    static TestRun()
    {
        NLogExtensions.Configure(configData: _configData);
    }

    private static readonly Dictionary<string, string> _configData = new()
    {
        ["Logging:LogLevel:Default"] = "Debug",
        ["Logging:LogLevel:Microsoft"] = "Warning",
        ["Logging:LogLevel:Microsoft.Hosting.Lifetime"] = "Information",

        ["NLog:autoReload"] = "True",
        ["NLog:throwConfigExceptions"] = "True",
        ["NLog:internalLogLevel"] = "warn",
        ["NLog:internalLogFile"] = "${basedir}\\log\\internalNLog.log",

        ["NLog:variables:_date"]    = "${date:format=yyyy-MM-dd HH\\:mm\\:ss.fff}",
        ["NLog:variables:_time"]    = "${date:format=HH\\:mm\\:ss.fff}",
        ["NLog:variables:_thread"]  = "${pad:fixedLength=true:padding=-5  :inner=${threadId}}",
        ["NLog:variables:_level"]   = "${pad:fixedLength=true:padding=-5  :inner=${level}}",
        ["NLog:variables:_logger"]  = "${pad:fixedLength=true:padding=-16 :inner=${logger:shortName=true}}",
        ["NLog:variables:_fixture"] = "${pad:fixedLength=true:padding=-16 :inner=${mdc:FIXTURE}}",
        ["NLog:variables:_fact"]    = "${pad:fixedLength=true:padding=-32 :inner=${mdc:TEST}}",

        ["NLog:variables:layoutBase"]     = "${var:_fixture} | ${var:_fact} | ${var:_logger} | ${message} ${exception:format=toString}",
        ["NLog:variables:layoutDebugger"] = "${var:_time} | ${var:layoutBase}",
        ["NLog:variables:layoutFile"]     = "${var:_date} | ${var:_level} | ${var:layoutBase}",

        ["NLog:targets:debug:type"] = "Debugger",
        ["NLog:targets:debug:layout"] = "${var: layoutDebugger}",

        ["NLog:targets:file:type"] = "File",
        ["NLog:targets:file:layout"] = "${var:layoutFile}",
        ["NLog:targets:file:fileName"] = "${basedir}\\log\\${processName}_${date:format=yyyy-MM-dd}.log",
        ["NLog:targets:file:keepFileOpen"] = "True",
        ["NLog:targets:file:deleteOldFileOnStartup"] = "False",

        ["NLog:rules:0:logger"] = "*",
        ["NLog:rules:0:minLevel"] = "Debug",
        ["NLog:rules:0:writeTo"] = "debug",

        ["NLog:rules:1:logger"] = "*",
        ["NLog:rules:1:minLevel"] = "Trace",
        ["NLog:rules:1:writeTo"] = "file"
    };
}