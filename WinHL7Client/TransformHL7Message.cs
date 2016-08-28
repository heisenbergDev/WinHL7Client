using System;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapiTools.Base.Net;

namespace WinHL7Client
{
    public class TransformHL7Message
    {

        public static String EncodeHL7Message(IMessage iMessage)
        {
            PipeParser parser = new PipeParser();
            String encodedMessage = null;

            try
            {
                encodedMessage = parser.Encode(iMessage);
            }
            catch (Exception exception)
            {
                ConnectionStatus.AddConnectionStatus(exception);
            }

            return encodedMessage;
        }

        public static IMessage DecodeHL7Message(String message)
        {
            PipeParser parser = new PipeParser();
            IMessage decodedMessage = null;

            try
            {
                decodedMessage = parser.Parse(message);
            }
            catch (Exception exception)
            {
                ConnectionStatus.AddConnectionStatus(exception);
            }

            return decodedMessage;
        }

    }
}
