using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapiTools.Base.Net;
using System;
using System.Net;
using System.Windows;

namespace WinHL7Client
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sendMessage(object sender, RoutedEventArgs e)
        {
            IPEndPoint destinationConn = CreateIPEndPoint(destinationIPTextbox.Text, destinationPortTextbox.Text);
            IMessage message = DecodeHL7Message(messageContent.Text);

            String ackResponse = sendHL7MessageToIpPort(destinationConn, message);

            if (String.IsNullOrEmpty(ackResponse))
            {
                messageReceivedTextbox.Text = @"No message received!!";
            }
            else
            {
                messageReceivedTextbox.Text = ackResponse;

            }

        }

        private String sendHL7MessageToIpPort(IPEndPoint destinationConn, IMessage message)
        {
            String ackReceived = null;

            if ((destinationConn != null) && (message != null))
            {
                try
                {
                    SimpleMLLPClient mllpClient = new SimpleMLLPClient(destinationConn.Address.ToString(), destinationConn.Port);
                    IMessage response = mllpClient.SendHL7Message(message);
                    ackReceived = EncodeHL7Message(response);
                    mllpClient.Disconnect();
                }
                catch (Exception e)
                {
                    ConnectionStatus.AddConnectionStatus(e);
                    ShowConnectionStatus(ConnectionStatus.Status);
                }

            }
            return ackReceived;
        }

        private String EncodeHL7Message(IMessage iMessage)
        {
            PipeParser parser = new PipeParser();
            String encodedMessage = null;

            try
            {
                encodedMessage = parser.Encode(iMessage);
            }
            catch (Exception e)
            {
                ConnectionStatus.AddConnectionStatus(e);
                ShowConnectionStatus(ConnectionStatus.Status);
            }

            return encodedMessage;
        }

        private IMessage DecodeHL7Message(String message)
        {
            PipeParser parser = new PipeParser();
            IMessage decodedMessage = null;

            try
            {
                decodedMessage = parser.Parse(message);
            }
            catch (Exception e)
            {
                ConnectionStatus.AddConnectionStatus(e);
                ShowConnectionStatus(ConnectionStatus.Status);
            }

            return decodedMessage;
        }

        private IPEndPoint CreateIPEndPoint(String textIp, String textPort)
        {
            IPEndPoint destinationConn = null;
            try
            {
                IPAddress destinationIp = IPAddress.Parse(textIp);
                int destinationPort = int.Parse(textPort);
                destinationConn = new IPEndPoint(destinationIp, destinationPort);
            }
            catch (Exception e)
            {
                ConnectionStatus.AddConnectionStatus(e);
                ShowConnectionStatus(ConnectionStatus.Status);
            }
            return destinationConn;
        }

        private void ShowConnectionStatus(String textToShow)
        {
            if (textToShow != null)
            {
                connectionLogTextBox.Text = textToShow;
            }
        }
    }
}
