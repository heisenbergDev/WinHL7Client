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
            IMessage message = DecodeHL7Message(messageContent.Text);
            TcpConnDestinationData destinationConn = new TcpConnDestinationData(destinationIPTextbox.Text, destinationPortTextbox.Text);
            messageReceivedTextbox.Text = destinationConn.ToString();

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

        private String sendHL7MessageToIpPort(TcpConnDestinationData destinationConn, IMessage message)
        {
            String ackReceived = null;

            if ((destinationConn.IsValid()) && (message != null))
            {
                try
                {
                    SimpleMLLPClient mllpClient = new SimpleMLLPClient(destinationConn.Ip, destinationConn.Port);
                    IMessage response = mllpClient.SendHL7Message(message);
                    ackReceived = EncodeHL7Message(response);
                    mllpClient.Disconnect();
                }
                catch (Exception e)
                {
                    ConnectionStatus.SetConnectionStatus(e);
                    ShowConnectionStatus(ConnectionStatus.Status);
                }

            }
            return ackReceived;
        }

        private String EncodeHL7Message(IMessage iMessage)
        {
            PipeParser parser = new PipeParser();
            String encodedMessage = null;

            if (iMessage != null) encodedMessage = parser.Encode(iMessage);

            return encodedMessage;
        }

        private IMessage DecodeHL7Message(String message)
        {
            PipeParser parser = new PipeParser();
            IMessage decodedMessage = null;

            if (!String.IsNullOrEmpty(message)) decodedMessage = parser.Parse(message);

            return decodedMessage;
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
