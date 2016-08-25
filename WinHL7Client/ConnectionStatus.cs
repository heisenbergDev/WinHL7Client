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

        public static void AddConnectionStatus(Exception exception)
        {
            AddStatus(exception.ToString());
        }

        public static void AddConnectionStatus(String status)
        {
            AddStatus(status);
        }

        private static void AddStatus(String newStatus)
        {
            Status = newStatus + "\n" + Status;
        }

        public static void CleanConnectionStatus()
        {
            Status = "";
        }
    }
}
