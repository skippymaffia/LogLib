using System.Text;

namespace Logger
{
    public static class StreamLogger
    {
        /// <summary>
        /// The whole logging logic for file logger.
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

            string msg = $"{DateTime.Now} [{type}] {message}";
            var writer = new StreamWriter(stream, Encoding.UTF8);
            Console.WriteLine("pos:"+stream.Position);
            writer.WriteLine(msg);
            writer.Flush();

            return true;
        }

        /// <summary>
        /// Logs only the debug type message to file.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public static bool Debug(Stream stream, string? message)
        {
            try
            {
                return Log(LogType.debug, stream, message);
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Logs only the info type message to file.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public static bool Info(Stream stream, string? message)
        {
            try
            {
                return Log(LogType.info, stream, message);
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Logs only the error type message to file.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public static bool Error(Stream stream, string? message)
        {
            try
            {
                return Log(LogType.error, stream, message);
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}