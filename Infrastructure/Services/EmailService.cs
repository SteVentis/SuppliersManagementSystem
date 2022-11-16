using Infrastructure.Interfaces;
using MimeKit;
using MailKit.Net.Smtp;
using Models.Email;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Observability.Contracts;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        private ILoggerManager _logger;
        public EmailService(EmailConfiguration emailConfig, ILoggerManager logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;  
        }
        public void SendEmailToNewSupplier(Supplier supplier)
        {
            string subject = "Welcome to the Suppliers Management System";
            var message = new Message(new string[] { supplier.Email }, subject, EmailBody(supplier));
            SendEmail(message);
        }
        public void SendEmail(Message message)
        {
            MimeMessage mailMessage = this.CreateEmailMessage(message);
            this.Send(mailMessage);
        }

        public MimeMessage CreateEmailMessage(Message message)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("email",_emailConfig.From));
            mailMessage.To.AddRange(message.To);
            mailMessage.Subject = message.Subject;
            mailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html){ Text = message.Body };
            
            return mailMessage;
        }
        public void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Something went wrong in MailSend method : {ex}");
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
        public string EmailBody(Supplier supplier)
        {
            string body = $@"<html>
                                <head>    
                                    <title></title>
                                </head>
                                <body>
                                    <h3>Dear {supplier.Name} </h3>
                                    <p>You have been succcessfully registered to our system.<p/>
                                   
                                </body>
                            </html>";
            return body;
        }
    }
}
