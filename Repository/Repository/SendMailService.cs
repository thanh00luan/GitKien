using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.DTO.MailDTO;
using BusinessObject.Model;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Repository.IRepository;
using Serilog;

namespace Repository.Repository
{
        public class SendMailService : ISendMailService
    {
        private readonly SmtpSettings _smtpSettings;

        public SendMailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        // Gửi email, theo nội dung trong mailContent
        public async Task SendMail(MailContent mailContent)
        {
            /*var email = new MimeMessage();
            email.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            email.From.Add(new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail));
            email.To.Add(MailboxAddress.Parse(mailContent.To));
            email.Subject = mailContent.Subject;


            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new MailKit.Net.Smtp.SmtpClient();

            try
            {
                smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                System.IO.Directory.CreateDirectory("mailssave");
                var emailsavefile = string.Format(@"mailssave/{0}.eml", Guid.NewGuid());
                await email.WriteToAsync(emailsavefile);

                logger.LogInformation("Lỗi gửi mail, lưu tại - " + emailsavefile);
                logger.LogError(ex.Message);
            }

            smtp.Disconnect(true);

            logger.LogInformation("send mail to " + mailContent.To);*/

        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.UseSsl);
                client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }

        public async Task SendEmailWithAttachmentAsync(string email, string subject, string htmlMessage, byte[] attachment, string attachmentName, string mimeType)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };

                // Thêm tệp đính kèm
                using (var stream = new MemoryStream(attachment))
                {
                    bodyBuilder.Attachments.Add(attachmentName, stream, ContentType.Parse(mimeType));
                }

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.UseSsl);
                    client.Authenticate(_smtpSettings.Username, _smtpSettings.Password);
                    await client.SendAsync(message);
                    client.Disconnect(true);

                    // Ghi log khi gửi email thành công
                    Log.Information($"Sent email successfully to {email} with subject: {subject}");
                }
            }
            catch (Exception ex)
            {
                // Ghi log khi gửi email gặp lỗi
                Log.Error($"Failed to send email to {email} with subject: {subject}. Error: {ex.Message}");
                throw; // Ném lại ngoại lệ để xử lý ở lớp gọi
            }
        }


        public async Task SendForgotPasswordEmailAsync(string email, string resetLink)
        {
            string subject = "Forgot Password";
            string htmlMessage = $"Please click <a href=\"{resetLink}\">here</a> to reset your password.";

            await SendEmailAsync(email, subject, htmlMessage);
        }

    }

}