using Microsoft.Extensions.Logging;
using static System.Console;

namespace StarSystemWithEFCore.Logging;

public class ConsoleLogger : ILogger
{
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (eventId.Id == 20100)
        {
            // log the level and event identifier
            Write("Level: {0}, Event Id: {1}, Event: {2}",
                logLevel, eventId.Id, eventId.Name);
            // only output the state or exception if it exists
            if (state != null)
            {
                Write($", State: {state}");
            }

            if (exception != null)
            {
                Write($", Exception: {exception.Message}");
            }

            WriteLine();
        }
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
            case LogLevel.Information:
            case LogLevel.None:
                return false;
            case LogLevel.Debug:
            case LogLevel.Warning:
            case LogLevel.Error:
            case LogLevel.Critical:
            default:
                return true;
        }
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }
}