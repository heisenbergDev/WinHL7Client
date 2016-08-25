using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinHL7Client
{
    public sealed class ConnectionStatus
    {
        private static readonly ConnectionStatus instance = new ConnectionStatus();

        private ConnectionStatus() { }

        public static ConnectionStatus Instance
        {
            get
            {
                return instance;
            }
        }

        public static String Status { get; set; }

        public static void SetConnectionStatus(Exception exception)
        {
            AddStatus(exception.ToString());
        }

        public static void SetConnectionStatus(String status)
        {
            AddStatus(status);
        }

        private static void AddStatus(String newStatus)
        {
            Status = Status + newStatus + "\n";
        }
    }
}
