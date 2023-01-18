namespace Logger
{
    public static class ConsoleLogger
    {
        /// <summary>
        /// Logs the different types of messages to the console.
        /// </summary>
        /// <param name="type">The type of the log.</param>
        /// <param name="message">The message of the log.</param>
        /// <returns>True if the log success, false otherwise.</returns>
        internal static bool Log(LogType type, string? message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return false;
            }

            if (message.Length > 1000)
            {
                throw new Exception("The length of the message is too long!");
            }
            
            var fgc = Console.ForegroundColor;
            Console.ForegroundColor = Helper.GetForegroundColor(type);
            Console.WriteLine($"{DateTime.Now} [{type}] {message}");
            Console.ForegroundColor = fgc;
            
            return true;
        }


        /// <summary>
        /// Logs only the debug type message to console.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public static bool Debug(string? message)
        {
            try
            {
                return Log(LogType.debug, message);
            }
            catch (Exception)
            {
            }

            return false;            
        }

        /// <summary>
        /// Logs only the info type message to console.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public static bool Info(string? message)
        {
            try
            {
                return Log(LogType.info, message);
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Logs only the error type message to console.
        /// </summary>
        /// <param name="message">The message of the log</param>
        /// <returns>True if success, false otherwise.</returns>
        public static bool Error(string? message)
        {
            try
            {
                return Log(LogType.error, message);
            }
            catch (Exception)
            {
            }

            return false;
        }
    }
}