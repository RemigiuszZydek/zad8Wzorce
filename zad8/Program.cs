using System;


abstract class Logger
{
    protected Logger nextLogger;

    public void SetNext(Logger nextLogger)
    {
        this.nextLogger = nextLogger;
    }

    public void LogMessage(int level, string message)
    {
        if (CanHandle(level))
        {
            Write(message);
        }
        else if (nextLogger != null)
        {
            nextLogger.LogMessage(level, message);
        }
    }

    protected abstract bool CanHandle(int level);
    protected abstract void Write(string message);
}


class InfoLogger : Logger
{
    protected override bool CanHandle(int level)
    {
        return level == LoggerLevel.INFO;
    }

    protected override void Write(string message)
    {
        Console.WriteLine("INFO: " + message);
    }
}


class WarningLogger : Logger
{
    protected override bool CanHandle(int level)
    {
        return level == LoggerLevel.WARNING;
    }

    protected override void Write(string message)
    {
        Console.WriteLine("WARNING: " + message);
    }
}


class ErrorLogger : Logger
{
    protected override bool CanHandle(int level)
    {
        return level == LoggerLevel.ERROR;
    }

    protected override void Write(string message)
    {
        Console.WriteLine("ERROR: " + message);
    }
}

static class LoggerLevel
{
    public const int INFO = 1;
    public const int WARNING = 2;
    public const int ERROR = 3;
}


class Program
{
    static void Main(string[] args)
    {
  
        Logger infoLogger = new InfoLogger();
        Logger warningLogger = new WarningLogger();
        Logger errorLogger = new ErrorLogger();

      
        infoLogger.SetNext(warningLogger);
        warningLogger.SetNext(errorLogger);

       
        Logger loggerChain = infoLogger;

        loggerChain.LogMessage(LoggerLevel.INFO, "This is an information.");
        loggerChain.LogMessage(LoggerLevel.WARNING, "This is a warning.");
        loggerChain.LogMessage(LoggerLevel.ERROR, "This is an error.");
    }
}
