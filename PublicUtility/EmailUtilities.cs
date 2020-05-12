using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fhs.Bulletin.E_Utility.PublicUtility
{
    public static class EmailUtilities
    {
        #region Variables

        private static bool _invalid = false;

        #endregion

        #region Methods

        public static void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            // Instantiate a new instance of MailMessage
            MailMessage mMailMessage = new MailMessage();

            // Set the sender address of the mail message
            mMailMessage.From = new MailAddress(from);
            // Set the recepient address of the mail message
            mMailMessage.To.Add(new MailAddress(to));

            // Check if the bcc value is null or an empty string
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            } // Check if the cc value is null or an empty value
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                mMailMessage.CC.Add(new MailAddress(cc));
            } // Set the subject of the mail message
            mMailMessage.Subject = subject;
            // Set the body of the mail message
            mMailMessage.Body = body;

            // Set the format of the mail message body as HTML
            mMailMessage.IsBodyHtml = true;
            // Set the priority of the mail message to normal
            mMailMessage.Priority = MailPriority.Normal;

            // Instantiate a new instance of SmtpClient
            SmtpClient mSmtpClient = new SmtpClient();
            // Send the mail message
            mSmtpClient.Send(mMailMessage);
        }
        public static bool SendEmailToAddress(string from, string to, string subject, string message, string password, string hostName, bool isHtmlBody = false)
        {

            var email = new MailMessage();
            email.From = new MailAddress(from);
            email.Sender = new MailAddress(from);
            email.To.Add(new MailAddress(to));
            email.Subject = subject;
            email.Body = message;
            email.IsBodyHtml = isHtmlBody;

            var smtpC = new SmtpClient
            {
                Host = hostName,
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(@from, password)
            };

            try
            {
                smtpC.Send(email);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public static bool SendEmail(string subject, string body, string from, string to, string username, string password, string host, int port)
        {
            using (MailMessage mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = body;

                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = host;
                smtp.EnableSsl = true;
                NetworkCredential networkCred = new NetworkCredential(username, password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = networkCred;
                smtp.Port = port;

                try
                {
                    smtp.Send(mm);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        public static bool IsValidEmail(string emailAddress)
        {
            _invalid = false;
            if (String.IsNullOrEmpty(emailAddress))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (_invalid)
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(emailAddress,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        #endregion

        #region Helper Methods
        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                _invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
        #endregion
    }
}
