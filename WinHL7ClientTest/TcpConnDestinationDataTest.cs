using WinHL7Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WinHL7ClientTest
{
    [TestClass]
    public class TcpConnDestinationDataTest
    {
        [TestMethod]
        public void CreateValidConnectionTest()
        {
            string goodIP = "127.0.0.1";
            string goodPortNumber = "65535";

            TcpConnDestinationData conn1 = new TcpConnDestinationData(goodIP, goodPortNumber);

            Assert.IsTrue(conn1.isValid());
        }
    }
}
