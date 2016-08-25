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

        public TcpConnDestinationData(String ip, String port)
        {
            int portNumber;
            if (Int32.TryParse(port, out portNumber) && ValidateIpAndPort(ip, portNumber))
            {
                Init(ip, portNumber);
            }

        }

        private void Init(String Ip, int Port)
        {

            this.Ip = Ip;
            this.Port = Port;
        }

        public Boolean IsValid()
        {
            if (ValidateIpAndPort(this.Ip, this.Port))
            {
                return true;
            }
            return false;

        }

        private Boolean ValidateIpAndPort(String Ip, int Port)
        {
            if (CheckIpAddress(Ip) && (Port > 0) && (Port < 65536))
            {
                return true;
            }
            return false;

        }

        private Boolean CheckIpAddress(String ip)
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
