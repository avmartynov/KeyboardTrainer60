using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using NLog;
using Twidlle.Library.Utility;

namespace Twidlle.Library.Testing.Diagnostics;

public class TestFixtureFacade
{
    private readonly string _fixtureName;

    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public TestFixtureFacade() 
        : this(new StackTrace().GetFrame(2)?.GetMethod())
    {}

    public TestFixtureFacade(string fixtureName) =>
        _fixtureName = fixtureName;


    public TestFixtureFacade(MethodBase? callerMethod)
    {
        var ns = callerMethod?.GetDeclaringNamespace() ?? "";
        var cn = callerMethod?.GetDeclaringClassName() ?? "Unknown";
        _fixtureName = cn[(ns.Length + 1)..];
    }
        


    public void Execute(Action testMethodBody, [CallerMemberName] string? testMethodName = null) 
    {
        if (testMethodBody is null) throw new ArgumentNullException(nameof(testMethodBody));

        Execute(_fixtureName, testMethodName, testMethodBody);
    }


    public void Execute(object testMethodParameters, Action testMethodBody, [CallerMemberName] string? testMethodName = null)
    {
        if (testMethodBody is null) throw new ArgumentNullException(nameof(testMethodBody));

        Execute(_fixtureName, testMethodName, testMethodParameters, testMethodBody);
    }


    private static void Execute(string testFixtureName, string? testMethodName, object parameters, Action testMethodBody) => 
        Invoke(testFixtureName, testMethodName, parameters, testMethodBody);


    private static void Execute(string testFixtureName, string? testMethodName, Action testMethodBody) => 
        Invoke(testFixtureName, testMethodName, testMethodBody);


    private static void Invoke(string testFixtureName, string? testMethodName, object parameters, Action testMethodBody) 
    {
        var stopwatch = Stopwatch.StartNew();
        var mdcContext = false;
        try
        {
            mdcContext = EnterContext(testFixtureName, testMethodName);
            LogStarting(testFixtureName, testMethodName, parameters);
            (testMethodBody ?? throw new ArgumentNullException(nameof(testMethodBody)))();
        }
        catch (Exception x)
        {
            LogFail(testFixtureName, testMethodName, x, stopwatch.ElapsedMilliseconds);
            throw;
        }
        finally
        {
            LeaveContext(mdcContext);
        }
    }


    private static void Invoke(string testFixtureName, string? testMethodName, Action testMethodBody)
    {
        var stopwatch = Stopwatch.StartNew();
        var mdcContext = false;
        try
        {
            mdcContext = EnterContext(testFixtureName, testMethodName);
            LogStarting(testFixtureName, testMethodName);
            (testMethodBody ?? throw new ArgumentNullException(nameof(testMethodBody)))();
        }
        catch (Exception x)
        {
            LogFail(testFixtureName, testMethodName, x, stopwatch.ElapsedMilliseconds);
            throw;
        }
        finally
        {
            LogReturn(testFixtureName, testMethodName, stopwatch.ElapsedMilliseconds);
            LeaveContext(mdcContext);
        }
    }


    private static bool EnterContext(string testFixtureName, string? testMethodName)
    {
        if (!string.IsNullOrEmpty(MappedDiagnosticsContext.Get("FIXTURE")))
        {
            _logger.Warn("Second enter to MDC context ignored.");
            return false;
        }

        MappedDiagnosticsContext.Set("FIXTURE", testFixtureName);
        MappedDiagnosticsContext.Set("TEST", testMethodName);
        return true;
    }


    private static void LeaveContext(bool mdcContext)
    {
        if (mdcContext)
        {
            MappedDiagnosticsContext.Remove("TEST");
            MappedDiagnosticsContext.Remove("FIXTURE");
        }
        LogManager.Flush();
    }


    private static void LogStarting(string testFixtureName, string? testMethodName) => 
        _logger.Info($"Test starting... {testFixtureName}.{testMethodName}");


    private static void LogStarting(string testFixtureName, string? testMethodName, object parameters) => 
        _logger.Info($"Test starting... {testFixtureName}.{testMethodName}. Parameters: {parameters.ToJson()}");


    private static void LogFail(string testFixtureName, string? testMethodName, Exception x, long elapsedMilliseconds) => 
        _logger.Error($"Test failed. ElapsedMilliseconds: {elapsedMilliseconds:F}, Exception: {Environment.NewLine}{x}");


    private static void LogReturn(string testFixtureName, string? testMethodName, long elapsedMilliseconds) => 
        _logger.Info($"Test finished. ElapsedMilliseconds: {elapsedMilliseconds:F} {Environment.NewLine}");
}