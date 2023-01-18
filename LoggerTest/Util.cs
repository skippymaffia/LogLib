using System.Text;

namespace Logger.Tests
{
    internal static class Util
    {
        internal static string GetMoreThan1000LongInput()
        {
            Random r = new Random();
            StringBuilder sb = new StringBuilder("a", 1001);
            for (int i = 0; i < 1001; i++)
            {
                int j = r.Next(0, 26);
                char c = (char)('a' + j);
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
}
