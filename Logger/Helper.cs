namespace Logger
{
    public class Helper
    {
        public static string GetDestinationFileName(string dir, string fileName, string fileDotExt)
        {
            string ret = dir + fileName;
            string[] files = Directory.GetFiles(dir, fileName + "*" + fileDotExt);
            if(files.Length==1)
            {
                return ret + ".0" + fileDotExt;
            }

            int[] nums = new int[files.Length];
            int i = 0;
            int num = 0;
            foreach (var file in files)
            {
                var x = file.Split('.');
                try
                {
                    nums[i] = int.Parse(x.Length > 2 ? x[1] : "0");
                }
                catch (Exception e)  
                {
                    Console.WriteLine(e.Message);                    
                }
                i++;
            }
            
            num = nums.Max()+1;            

            return ret + "." + num + fileDotExt;
        }

        /// <summary>
        /// Gets the foreground color by the type of the log message.
        /// </summary>
        /// <param name="type">The type of the log.</param>
        /// <returns>The foreground constant of the console.</returns>
        public static ConsoleColor GetForegroundColor(LogType type)
        {
            switch (type)
            {
                case LogType.info:
                    {
                        return ConsoleColor.Green;
                    }
                case LogType.debug:
                    {
                        return ConsoleColor.Gray; ;
                    }
                case LogType.error:
                    {
                        return ConsoleColor.Red; ;
                    }
            }

            return ConsoleColor.White;
        }
    }
}
