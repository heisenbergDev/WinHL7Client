using WinHL7Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WinHL7ClientTest
{
    [TestClass]
    public class TcpConnDestinationDataTest
    {
        string goodIP = "127.0.0.1";
        string goodPortNumber = "65535";
        string badIPString = "127.0.0.one";
        string badPortNumberString = "6s535";
        string badIPOutOfRange = "127.256.0.1";
        string badPortNumberOutOfRange = "65537";

        [TestMethod]
        public void CreateValidConnectionTest()
        {
            TcpConnDestinationData conn1 = new TcpConnDestinationData(goodIP, goodPortNumber);

            Assert.IsTrue(conn1.isValid());
        }

        [TestMethod]
        public void CreateInValidConnectionStringTest()
        {
            TcpConnDestinationData conn1 = new TcpConnDestinationData(badIPString, badPortNumberString);

            Assert.IsFalse(conn1.isValid());
        }

        [TestMethod]
        public void CreateInValidConnectionOutOfRangeTest()
        {
            TcpConnDestinationData conn1 = new TcpConnDestinationData(badIPOutOfRange, badPortNumberOutOfRange);

            Assert.IsFalse(conn1.isValid());
        }
    }
}
