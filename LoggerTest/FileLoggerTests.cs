namespace Logger.Tests
{
    [TestClass]
    public class FileLoggerTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            string s = Util.GetMoreThan1000LongInput();
            string[] msgs = { "alpha", "beta", "gamma", s};
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
            string[] msgs = { string.Empty, "     ", null };
            foreach (var msg in msgs)
            {
                Assert.IsFalse(FileLogger.Info(msg));
                Assert.IsFalse(FileLogger.Debug(msg));
                Assert.IsFalse(FileLogger.Error(msg));
            }
        }
    }
}