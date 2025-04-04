namespace Rum.Logging;

public partial interface ILogger<T> : ILogger, ICloneable;
public partial interface ILogger
{
    public void Error(params object?[] args);
    public void Warn(params object?[] args);
    public void Info(params object?[] args);
    public void Debug(params object?[] args);
    public void Log(LogLevel level, params object?[] args);
    public ILogger Child(string name);
    public ILogger Copy();
    public bool IsEnabled(LogLevel level);
    public ILogger SetLevel(LogLevel level);
}