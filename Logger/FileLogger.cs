namespace Logger
{
    public static class FileLogger
    {
        private const int SIZE = 5000;

        private static string dir = @"C:\temp\";
        private static string fileName = "skippy-log";
        private static string fileDotExt = ".txt";
        private static string filePath = string.Format(@"{0}{1}{2}", dir, fileName, fileDotExt);
        private static int fileSize = 0;

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
            if (!File.Exists(filePath))
            {                
                WriteAsync(msg);
            }
            else
            {
                fileSize = fileSize == 0 ? File.ReadAllText(filePath).Length:fileSize;
                if (fileSize + msg.Length < SIZE)
                {
                    WriteAsync(msg, isAppend:true);
                }
                else 
                {
                    //Console.WriteLine("archiveing...");
                    Archive();
                    WriteAsync(msg);
                }
            }

            return true;
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
            fileSize += message.Length + 2; //+2 cause cr+lf on win
        }

        /// <summary>
        /// The archive method which renames the "full" file to an archive name.
        /// </summary>
        public static void Archive()
        {            
            string destination = Helper.GetDestinationFileName(dir, fileName, fileDotExt);
            //Console.WriteLine("path:"+filePath+":destination:"+destination);
            File.Move(filePath, destination);
            fileSize = 0;
        }

        /// <summary>
        /// Logs only the debug type message to file.
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
        /// Logs only the info type message to file.
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
        /// Logs only the error type message to file.
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