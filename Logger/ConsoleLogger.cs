namespace Logger;

public static class ConsoleLogger
{
    private const int LOGLENGTH = 1000;
    private static readonly object lockerasync = new();
    private static readonly object locker = new();

    /// <summary>
    /// Logs the different types of messages to the console.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message of the log.</param>
    /// <returns>True if the log success, false otherwise.</returns>
    private static bool Log(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        if (message.Length > LOGLENGTH)
        {
            throw new Exception("The length of the message is too long!");
        }

        lock (locker)
        {
            DoLog(type, message);
        }

        return true;
    }

    private static void DoLog(LogType type, string? message)
    {
        var fgc = Console.ForegroundColor;
        Console.ForegroundColor = Helper.GetForegroundColor(type);
        Console.WriteLine($"{DateTime.Now} [{type}] {message}");
        Console.ForegroundColor = fgc;
    }

    /// <summary>
    /// Logs only the debug type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool TryLog(LogType type, string? message)
    {
        try
        {
            return Log(type, message);
        }
        catch (Exception)
        {
        }

        return false;
    }

    /// <summary>
    /// Logs only the debug type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Debug(string? message)
    {
        return TryLog(LogType.debug, message);
    }

    /// <summary>
    /// Logs only the info type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Info(string? message)
    {
        return TryLog(LogType.info, message);
    }

    /// <summary>
    /// Logs only the error type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Error(string? message)
    {
        return TryLog(LogType.error, message);
    }

    /// <summary>
    /// Logs only the warning type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Warning(string? message)
    {
        return TryLog(LogType.warning, message);
    }

    /// <summary>
    /// Logs the different types of messages to the console async!
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message of the log.</param>
    /// <returns>True if the log success, false otherwise.</returns>
    private static async Task<bool> LogAsync(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return await Task.FromResult(false);
        }

        if (message.Length > LOGLENGTH)
        {
            throw new Exception("The length of the message is too long!");
        }

        return await DoLogAsync(type, message);
    }

    private static async Task<bool> DoLogAsync(LogType type, string? message)
    {
        lock (lockerasync)
        {
            DoLog(type, message);
        }

        return await Task.FromResult(true);
    }

    /// <summary>
    /// Logs only the debug type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    private static async Task<bool> TryLogAsync(LogType type, string? message)
    {
        try
        {
            return await LogAsync(type, message);
        }
        catch (Exception)
        {
        }

        return await Task.FromResult(false);
    }

    /// <summary>
    /// Logs only the debug type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> DebugAsync(string? message)
    {
        return await TryLogAsync(LogType.debug, message);
    }

    /// <summary>
    /// Logs only the info type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> InfoAsync(string? message)
    {
        return await TryLogAsync(LogType.info, message);
    }

    /// <summary>
    /// Logs only the error type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> ErrorAsync(string? message)
    {
        return await TryLogAsync(LogType.error, message);
    }

    /// <summary>
    /// Logs only the warning type message to console.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> WarningAsync(string? message)
    {
        return await TryLogAsync(LogType.warning, message);
    }
}