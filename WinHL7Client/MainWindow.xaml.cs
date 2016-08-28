using NHapi.Base.Model;
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
            MLLPConectorHL7Message mllpConectorHL7Message = new MLLPConectorHL7Message(destinationIPTextbox.Text, destinationPortTextbox.Text, TransformHL7Message.DecodeHL7Message(messageContent.Text));

            String ackResponse = mllpConectorHL7Message.sendHL7Message();

            if (String.IsNullOrEmpty(ackResponse))
            {
                messageReceivedTextbox.Text = @"No message received!!";
                ShowConnectionStatus(false);
            }
            else
            {
                messageReceivedTextbox.Text = ackResponse;
                ShowConnectionStatus(true);
            }

        }

        private void ShowConnectionStatus(Boolean success)
        {
            if (success)
            {
                ConnectionStatus.CleanConnectionStatus();
            }
            connectionLogTextBox.Text = ConnectionStatus.Status;
        }
    }
}
