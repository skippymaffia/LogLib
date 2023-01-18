{
    [TestClass]
    public class ConsoleLoggerTests
    {
        [TestMethod]
        public void TestAllSuccess()
        {
            string[] msgs = { "alpha", "beta", "gamma"};
            foreach(var msg in msgs)
            {
                Assert.IsTrue(ConsoleLogger.Info(msg));
                Assert.IsTrue(ConsoleLogger.Debug(msg));
                Assert.IsTrue(ConsoleLogger.Error(msg));
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
                Assert.IsFalse(ConsoleLogger.Info(msg));
                Assert.IsFalse(ConsoleLogger.Debug(msg));
                Assert.IsFalse(ConsoleLogger.Error(msg));
            }
        }
    }
}