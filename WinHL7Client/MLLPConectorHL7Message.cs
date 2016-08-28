using NHapi.Base.Model;
using NHapiTools.Base.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinHL7Client
{
    class MLLPConectorHL7Message
    {
        private IPEndPoint destinationAddress;
        private IMessage message;

        public MLLPConectorHL7Message(IPEndPoint destination, IMessage message)
        {
            this.destinationAddress = destination;
            this.message = message;
        }

        public MLLPConectorHL7Message(String textIp, String textPort, IMessage message)
        {
            this.destinationAddress = CreateIPEndPoint(textIp, textPort);
            this.message = message;
        }

        public String sendHL7Message()
        {
            String ackReceived = null;

            if ((this.destinationAddress != null) && (this.message != null))
            {
                try
                {
                    SimpleMLLPClient mllpClient = new SimpleMLLPClient(destinationAddress.Address.ToString(), destinationAddress.Port);
                    IMessage response = mllpClient.SendHL7Message(message);
                    ackReceived = TransformHL7Message.EncodeHL7Message(response);
                    mllpClient.Disconnect();
                }
                catch (Exception exception)
                {
                    ConnectionStatus.AddConnectionStatus(exception);
                }

            }
            return ackReceived;
        }

        private IPEndPoint CreateIPEndPoint(String textIp, String textPort)
        {
            IPEndPoint newDestination = null;
            try
            {
                IPAddress destinationIp = IPAddress.Parse(textIp);
                int destinationPort = int.Parse(textPort);
                newDestination = new IPEndPoint(destinationIp, destinationPort);
            }
            catch (Exception exception)
            {
                ConnectionStatus.AddConnectionStatus(exception);
            }
            return newDestination;
        }

    }
}
