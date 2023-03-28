using Logger;

namespace Presentation;

internal class Program
{
    internal static string GetSimpleContent(int i, int j)
    {
        switch (j)
        {
            case 1:
                return string.Format("This is the simple {0}. console log content", i);
            case 2:
                return string.Format("This is the simple {0}. file log content", i);
        }

        return string.Format("This is the simple {0}. stream log content", i);
    }

    static async Task Main(string[] args)
    {
        string log;
        var asy = "async";
        //----
        for (int i = 0; i < 3; i++)
        {
            log = GetSimpleContent(i,1);
            
            ConsoleLogger.Info(log);
            ConsoleLogger.Debug(log);
            ConsoleLogger.Error(log);
            ConsoleLogger.Warning(log);

            await ConsoleLogger.InfoAsync(log + asy);
            await ConsoleLogger.DebugAsync(log + asy);
            await ConsoleLogger.ErrorAsync(log + asy);
            await ConsoleLogger.WarningAsync(log+ asy);
        }

        //----
        for (int i = 0; i < 150; i++)
        {
            log = GetSimpleContent(i,2);
            FileLogger.Info(log);
            FileLogger.Debug(log);
            FileLogger.Error(log);
            FileLogger.Warning(log);

            await FileLogger.InfoAsync(log + asy);
            await FileLogger.DebugAsync(log + asy);
            await FileLogger.ErrorAsync(log + asy);
            await FileLogger.WarningAsync(log + asy);
        }

        string[] files = Directory.GetFiles(@"c:\temp\", "skippy-*");
        foreach (string file in files)
        {
            Console.WriteLine(file);
        }

        //----
        log = GetSimpleContent(1,3);
        using (MemoryStream stream = new MemoryStream())
        {
            StreamLogger.Info(stream, log);
            StreamLogger.Debug(stream, log);
            StreamLogger.Error(stream, log);
            StreamLogger.Warning(stream, log);

            await StreamLogger.InfoAsync(stream, log + asy);
            await StreamLogger.DebugAsync(stream, log+asy);
            await StreamLogger.ErrorAsync(stream, log + asy);
            await StreamLogger.WarningAsync(stream, log + asy);

            stream.Position = 0;
            var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var str = reader.ReadLine();
                Console.WriteLine(str);
            }
        }
                
        log = GetSimpleContent(2, 3);
        var fileName = @"C:\temp\tmp-skippy.txt";
        using (FileStream stream = File.Create(fileName)) 
        {
            StreamLogger.Info(stream, log);
            StreamLogger.Debug(stream, log);
            StreamLogger.Error(stream, log);
            StreamLogger.Warning(stream, log);

            await StreamLogger.InfoAsync(stream, log + asy);
            await StreamLogger.DebugAsync(stream, log + asy);
            await StreamLogger.ErrorAsync(stream, log + asy);
            await StreamLogger.WarningAsync(stream, log + asy);

            stream.Position = 0;
            var reader = new StreamReader(stream);
            while (!reader.EndOfStream)
            {
                var str = reader.ReadLine();
                Console.WriteLine(str);
            }
        }
        
        Console.WriteLine("The End!");
    }
}