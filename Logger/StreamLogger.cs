using System.Text;

namespace Logger;

public static class StreamLogger
{
    private static readonly object locker = new();

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
}