using Logger.Common;

namespace Logger.Tests
{
    [TestClass()]
    public class HelperTests
    {
        [TestMethod]
        public void Test1()
        {
            string destination = Helper.GetDestinationFileName(@"c:\temp\", "skippy-log", ".txt");

            Assert.IsTrue(destination.Contains("log"));
        }

        [TestMethod()]
        public void GetForegroundColorAllTest()
        {
            Assert.AreEqual(ConsoleColor.Gray, Helper.GetForegroundColor(LogType.debug));
            Assert.AreEqual(ConsoleColor.Green, Helper.GetForegroundColor(LogType.info));
            Assert.AreEqual(ConsoleColor.Red, Helper.GetForegroundColor(LogType.error));
        }
    }
}
