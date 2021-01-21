// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mail;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NetExtender.Mail
{
    public class Mail : IDisposable
    {
        public SmtpClient SmtpClient { get; }

        public MailAddress Address { get; set; }

        public delegate void EmailHandler(Object sender, Boolean cancelled, Exception exception);

        public event EmailHandler EmailSendCompleted;

        public Mail([NotNull] SmtpClient client, [NotNull] MailAddress address)
        {
            SmtpClient = client;
            SmtpClient.SendCompleted += OnEmailSend;

            Address = address;
        }

        private void OnEmailSend(Object sender, AsyncCompletedEventArgs args)
        {
            EmailSendCompleted?.Invoke(sender, args.Cancelled, args.Error);
        }

        public Boolean Send([NotNull] MailMessage message, out Exception exception)
        {
            try
            {
                SmtpClient.Send(message);
                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public Task SendAsync([NotNull] MailMessage message)
        {
            return SmtpClient.SendMailAsync(message);
        }
        
        public MailMessage GetMessage([NotNull] IEnumerable<MailAddress> to, [NotNull] String subject, [NotNull] String body,
            Boolean replyToAdmin = false)
        {
            return GetMessage(Address, to, subject, body, replyToAdmin);
        }

        public static MailMessage GetMessage([NotNull] MailAddress from, [NotNull] IEnumerable<MailAddress> to, [NotNull] String subject,
            [NotNull] String body, Boolean replyToAdmin = false)
        {
            MailMessage message = new MailMessage
            {
                From = from,
                Subject = subject,
                Body = body
            };

            if (replyToAdmin)
            {
                message.ReplyToList.Add(from);
            }

            foreach (MailAddress address in to)
            {
                message.To.Add(address);
            }

            return message;
        }

        public void Dispose()
        {
            SmtpClient.SendCompleted -= OnEmailSend;
        }
    }
}