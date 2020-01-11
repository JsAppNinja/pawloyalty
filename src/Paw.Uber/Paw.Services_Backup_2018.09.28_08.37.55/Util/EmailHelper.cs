using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using SendGrid;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Configuration;

namespace Paw.Services.Util
{
    public static class EmailHelper
    {
        public static void Send(string fromAddress, string fromDisplayName, string toAddress, string subject, string text, string html = null)
        {
            SendGridMessage message = new SendGridMessage();
            message.From = new MailAddress(fromAddress, fromDisplayName);

            List<string> toList = toAddress.GetList();
            foreach (var item in toList)
            {
                message.AddTo(item);
            }

            message.Subject = subject;

            if (html != null)
            {
                message.Html = html;
            }
            else
            {
                message.Text = text;
            }

            Web web = new Web(GetNetworkCredential());
            web.Deliver(message);
        }

        public static Task SendIdentityMessageAsync(IdentityMessage message)
        {
            var sendGridMessage = new SendGridMessage();

            List<string> toList = message.Destination.GetList();
            foreach (var item in toList)
            {
                sendGridMessage.AddTo(item);
            }
            sendGridMessage.From = GetFromAddress();
            sendGridMessage.Subject = message.Subject;
            sendGridMessage.Text = message.Body;
            sendGridMessage.Html = message.Body;

            // Create a Web transport for sending email.
            var transportWeb = new Web(GetNetworkCredential());
            
            // Send the email.
            if (transportWeb != null)
            {
                return transportWeb.DeliverAsync(sendGridMessage);
            }
            else
            {
                return Task.FromResult(0);
            }
        }

        private static NetworkCredential GetNetworkCredential()
        {
            return new NetworkCredential("azure_bac160b499248e5e6d3ab319f4e3fe7d@azure.com", "kpctsbay");
        }

        public static MailAddress GetFromAddress()
        { 
            string fromAddress = ConfigurationManager.AppSettings["FromEmailAddress"] as string;
            string fromDisplay = ConfigurationManager.AppSettings["FromEmailDisplay"] as string;

            if (fromAddress == null || fromDisplay == null)
            {
                throw new Exception("Unable to find application setting for 'FromEmailAddress' or 'FromEmailDisplay'.");
            }
            return new MailAddress(fromAddress, fromDisplay);
        }
    }
}
