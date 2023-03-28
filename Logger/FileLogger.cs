using Logger.Common;

namespace Logger;

public static class FileLogger
{
    private const int FILESIZE = 5000;
    
    private static readonly object locker = new();
    private static SemaphoreSlim semaphoreSlim = new(1, 1);

    private static string dir = @"C:\temp\";
    private static string fileName = "skippy-log";
    private static string fileDotExt = ".txt";
    private static string filePath = string.Format(@"{0}{1}{2}", dir, fileName, fileDotExt);
    private static int currFileSize = 0;

    /// <summary>
    /// The whole logging logic for file logger.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message to be logged.</param>
    /// <returns>True if success, false otherwise.</returns>
    public static bool Log(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        string msg = $"{DateTime.Now} [{type}] {message}";
        lock (locker)
        {
            if (File.Exists(filePath))
            {
                Append(msg); 
            }
            else
            {
                Write(msg);
            }
        }

        return true;
    }

    /// <summary>
    /// The whole logging logic for file logger.
    /// </summary>
    /// <param name="type">The type of the log.</param>
    /// <param name="message">The message to be logged.</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> LogAsync(LogType type, string? message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return false;
        }

        string msg = $"{DateTime.Now} [{type}] {message}";
        
        await semaphoreSlim.WaitAsync();
        try
        {
            if (File.Exists(filePath))
            {
                await AppendAsync(msg);
            }
            else
            {
                await WriteAsync(msg);
            }
        }
        finally
        {
            semaphoreSlim.Release();
        }

        return true;
    }

    private static void Append(string msg)
    {
        currFileSize = currFileSize == 0 ? File.ReadAllText(filePath).Length : currFileSize;
        var isAppend = true;
        if (currFileSize + msg.Length > FILESIZE)
        {
            Archive();
            isAppend = false;            
        }        
            
        Write(msg, isAppend);
    }

    private static async Task AppendAsync(string msg)
    {
        currFileSize = currFileSize == 0 ? File.ReadAllText(filePath).Length : currFileSize;
        var isAppend = true;
        if (currFileSize + msg.Length > FILESIZE)
        {
            Archive();
            isAppend = false;
        }

        await WriteAsync(msg, isAppend);
    }

    /// <summary>
    /// Writes/Appends a log into a file.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="isAppend">True if the message need to be appended, false otherwise.</param>
    /// <returns></returns>
    public static void Write(string message, bool isAppend = false)
    {
        using StreamWriter file = new(filePath, append: isAppend);
        file.WriteLine(message);
        currFileSize += message.Length + 2; //+2 cause cr+lf on win
    }

    /// <summary>
    /// Writes/Appends a log into a file.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="isAppend">True if the message need to be appended, false otherwise.</param>
    /// <returns></returns>
    public static async Task WriteAsync(string message, bool isAppend=false)
    {
        using StreamWriter file = new(filePath, append: isAppend);
        await file.WriteLineAsync(message);
        currFileSize += message.Length + 2; //+2 cause cr+lf on win
    }

    /// <summary>
    /// The archive method which renames the "full" file to an archive name.
    /// </summary>
    public static void Archive()
    {            
        string destination = Helper.GetDestinationFileName(dir, fileName, fileDotExt);
        //Console.WriteLine("path:"+filePath+":destination:"+destination);
        File.Move(filePath, destination);
        currFileSize = 0;
    }

    #region normal-logging
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
    #endregion normal-logging

    #region async-logging
    /// Try to Log the * type message to the file.
    /// </summary>
    /// <param name="message">The message of the log</param>
    /// <returns>True if success, false otherwise.</returns>
    public static async Task<bool> TryLogAsync(LogType type, string? message)
    {
        try
        {
            return await LogAsync(type, message);
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
    #endregion async-logging
}