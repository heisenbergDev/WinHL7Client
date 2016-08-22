using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinHL7Client
{
    public class TcpConnDestinationData
    {
        public String Ip { get; set; }

        public int Port { get; set; }

        public TcpConnDestinationData(String Ip, String Port)
        {
            int portNumber;
            if (Int32.TryParse(Port, out portNumber) && validateIpAndPort(Ip, portNumber))
            {
                init(Ip, portNumber);
            }

        }

        private void init(String Ip, int Port)
        {

            this.Ip = Ip;
            this.Port = Port;
        }

        public Boolean isValid()
        {
            if (validateIpAndPort(this.Ip, this.Port))
            {
                return true;
            }
            return false;

        }

        private Boolean validateIpAndPort(String Ip, int Port)
        {
            if (checkIpAddress(Ip) && (Port > 0) && (Port < 65536))
            {
                return true;
            }
            return false;

        }

        private Boolean checkIpAddress(String ip)
        {
            IPAddress ipParsed;
            if (!(String.IsNullOrEmpty(ip)) && (IPAddress.TryParse(ip, out ipParsed)))
            {
                return true;
            }
            return false;
        }

        public override String ToString()
        {
            return this.Ip + " " + this.Port;
        }
    }
}
