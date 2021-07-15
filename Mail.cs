using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace SimpleMailCLI
{
    public static class Mail
    {
        public static void Send(List<string> recipients, string subject, string displayName, string body, bool enableSsl = true)
        {
            try
            {
                // Get configuration values
                var appConfig = new AppConfiguration();
                int _port = appConfig.MailPort;
                string _host = appConfig.MailHost;
                string _sender = appConfig.MailUser;
                string _password = appConfig.MailPassword;

                // Initialize the SMTP host.
                SmtpClient client = new SmtpClient(_host);
                client.Port = _port;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(_sender, _password);
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
