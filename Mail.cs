using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace smail
{
    public class Mail
    {
        private readonly string _sender;
        private readonly string _username;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;

        public Mail(string sender, string username, string password, string host, int port)
        {
            _sender = sender;
            _username = username;
            _password = password;
            _host = host;
            _port = port;
        }

        public void Send(List<string> recipients, string subject, string displayName, string body, bool enableSsl)
        {
            try
            {
                // Initialize the SMTP host.
                SmtpClient client = new SmtpClient(_host);
                client.Port = _port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_username, _password);
                client.EnableSsl = enableSsl;
                client.Credentials = credentials;

                // Specify the message content.
                MailMessage message = new MailMessage();
                foreach (string recipient in recipients)
                {
                    message.To.Add(new MailAddress(recipient));
                }

                // Specify the email sender.
                // Create a mailing address that includes a UTF8 character
                // in the display name.
                message.From = new MailAddress(_sender, displayName, System.Text.Encoding.UTF8);

                // Include some non-ASCII characters in body and subject.
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                // Make the email body
                message.Body = body;
                message.BodyEncoding = System.Text.Encoding.UTF8;

                // Send email
                client.Send(message);

                // Clean up.
                message.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
