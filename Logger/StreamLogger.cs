using System.Text;
using Logger.Common;

namespace Logger;

public static class StreamLogger
{
    private static readonly object locker = new();
    private static SemaphoreSlim semaphoreStreamAsync = new(1, 1);

    #region simple-logging

    /// <summary>
    /// The whole logging logic for stream logger.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message to be logged.</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Log(LogType type, Stream stream, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        lock (locker)
        {
            DoLog(type, stream, message);
        }

        return true;
    }

    private static void DoLog(LogType type, Stream stream, string? message)
    {
        string msg = $"{DateTime.Now} [{type}] {message}";
        var writer = new StreamWriter(stream, Encoding.UTF8);
        Console.WriteLine("pos:" + stream.Position);
        writer.WriteLine(msg);
        writer.Flush();
    }
        
    /// Logs only the debug type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool TryLog(LogType type, Stream stream, string? message)
    {
        try
        {
            return Log(type, stream, message);
        }
        catch (Exception)
        {
        }

        return false;
    }

    /// <summary>
    /// Logs only the debug type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Debug(Stream stream, string? message)
    {
        return TryLog(LogType.debug, stream, message);
    }

    /// <summary>
    /// Logs only the info type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Info(Stream stream, string? message)
    {
        return TryLog(LogType.info, stream, message);
    }

    /// <summary>
    /// Logs only the error type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Error(Stream stream, string? message)
    {
        return TryLog(LogType.error, stream, message);
    }

    /// <summary>
    /// Logs only the warning type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Warning(Stream stream, string? message)
    {
        return TryLog(LogType.warning, stream, message);
    }
    #endregion simple-logging

    #region async-logging
    /// <summary>
    /// The whole logging logic for stream logger.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message to be logged.</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> LogAsync(LogType type, Stream stream, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        await semaphoreStreamAsync.WaitAsync();
        try
        {
            await DoLogAsync(type, stream, message);
        }
        finally
        {
            semaphoreStreamAsync.Release();
        }

        return true;
    }

    private static async Task DoLogAsync(LogType type, Stream stream, string? message)
    {
        string msg = $"{DateTime.Now} [{type}] {message}";
        var writer = new StreamWriter(stream, Encoding.UTF8);
        Console.WriteLine("async pos:" + stream.Position);
        await writer.WriteLineAsync(msg);
        writer.Flush();
    }

    /// Logs only the debug type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> TryLogAsync(LogType type, Stream stream, string? message)
    {
        try
        {
            return await LogAsync(type, stream, message);
        }
        catch (Exception)
        {
        }

        return false;
    }

    /// <summary>
    /// Logs only the debug type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> DebugAsync(Stream stream, string? message)
    {
        return await TryLogAsync(LogType.debug, stream, message);
    }

    /// <summary>
    /// Logs only the info type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> InfoAsync(Stream stream, string? message)
    {
        return await TryLogAsync(LogType.info, stream, message);
    }

    /// <summary>
    /// Logs only the error type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> ErrorAsync(Stream stream, string? message)
    {
        return await TryLogAsync(LogType.error, stream, message);
    }

    /// <summary>
    /// Logs only the warning type message to stream.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> WarningAsync(Stream stream, string? message)
    {
        return await TryLogAsync(LogType.warning, stream, message);
    }
    #endregion async-logging
}