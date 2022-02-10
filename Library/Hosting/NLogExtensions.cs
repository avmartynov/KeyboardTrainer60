using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;

namespace Twidlle.Library.Hosting
{
    public static class NLogExtensions
    {
        public static void Configure(string? loggingConfigFile = null, 
                                     bool defaultConfig = false, 
                                     IEnumerable<KeyValuePair<string, string>>? configData = null)
        {
            var configBuilder = new ConfigurationBuilder();

            configData = defaultConfig ? _defaultNLogConfig : configData;
            
            if (configData != null)
            {
                configBuilder.AddInMemoryCollection(configData);
            }

            if (File.Exists(loggingConfigFile))
            {
                configBuilder.AddJsonFile(loggingConfigFile);
            }

            var config = configBuilder.Build();

            var configSection = config.GetSection("NLog");

            LogManager.Configuration = new NLogLoggingConfiguration(configSection);
        }

        private static readonly Dictionary<string, string> _defaultNLogConfig = new ()
        {
            ["Logging:LogLevel:Default"] = "Debug",
            ["Logging:LogLevel:Microsoft"] = "Warning",
            ["Logging:LogLevel:Microsoft.Hosting.Lifetime"] = "Information",

            ["NLog:autoReload"] = "True",
            ["NLog:throwConfigExceptions"] = "True",
            ["NLog:internalLogLevel"] = "warn",
            ["NLog:internalLogFile"] = "${basedir}\\log\\internalNLog.log",

            ["NLog:variables:_date"] = "${date:format=yyyy-MM-dd HH\\:mm\\:ss.fff}",
            ["NLog:variables:_time"] = "${date:format=HH\\:mm\\:ss.fff}",
            ["NLog:variables:_thread"] = "${pad:fixedLength=true:padding=-5  :inner=${threadId}}",
            ["NLog:variables:_level"] = "${pad:fixedLength=true:padding=-5  :inner=${level}}",
            ["NLog:variables:_logger"] = "${pad:fixedLength=true:padding=-24 :inner=${logger:shortName=true}}",
            ["NLog:variables:_layout"] = "${var:_logger} | ${message} ${exception:format=toString}",

            ["NLog:variables:layoutDebugger"] = "${var:_time} | ${var:_level} | ${var:_layout}",
            ["NLog:variables:layoutFile"]     = "${var:_date} | ${var:_thread} | ${var:_level} | ${var:_layout}",

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
            ["NLog:rules:1:minLevel"] = "Debug",
            ["NLog:rules:1:writeTo"] = "file"
        };
    }
}
