namespace Logger.Tests
{
    [TestClass()]
    public class StreamLoggerTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            using (var stream = new MemoryStream())
            {
                string s = Util.GetMoreThan1000LongInput();
                string[] msgs = { "alpha", "beta", "gamma", s };
                foreach (var msg in msgs)
                {
                    Assert.IsTrue(StreamLogger.Info(stream, msg));
                    Assert.IsTrue(StreamLogger.Debug(stream, msg));
                    Assert.IsTrue(StreamLogger.Error(stream, msg));
                }
            }
        }

        [TestMethod]
        public void TestAllUnSuccess()
        {
            using (var stream = new MemoryStream())
            {
                string[] msgs = { string.Empty, "     ", null };
                foreach (var msg in msgs)
                {
                    Assert.IsFalse(StreamLogger.Info(stream, msg));
                    Assert.IsFalse(StreamLogger.Debug(stream, msg));
                    Assert.IsFalse(StreamLogger.Error(stream, msg));
                }
            }
        }
    }
}