using Microsoft.Extensions.Logging;

namespace StarSystemWithEFCore.Logging;

public class ConsoleLoggerProvider : ILoggerProvider
{
    // if your logger uses unmanaged resources,
    // then you can release them her
    public void Dispose()
    {
    }

    public ILogger CreateLogger(string categoryName)
    {
        // we could have different logger implementations for
        // different categoryName values but we only have one
        return new ConsoleLogger();
    }
}