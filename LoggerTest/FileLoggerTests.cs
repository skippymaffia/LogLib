using Logger;

namespace LoggerTest
{
    [TestClass]
    public class FileLoggerTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            string[] msgs = { "alpha", "beta", "gamma"};
            foreach(var msg in msgs)
            {
                Assert.IsTrue(FileLogger.Info(msg));
                Assert.IsTrue(FileLogger.Debug(msg));
                Assert.IsTrue(FileLogger.Error(msg));
            }
        }

        [TestMethod]
        public void TestAllUnSuccess()
        {
            string s = Util.GetMoreThan1000LongInput();
            Console.WriteLine(s);
            string[] msgs = { string.Empty, "     ", null, s };
            foreach (var msg in msgs)
            {
                Assert.IsFalse(FileLogger.Info(msg));
                Assert.IsFalse(FileLogger.Debug(msg));
                Assert.IsFalse(FileLogger.Error(msg));
            }
        }
    }
}