using BookStore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";                                                                              /*this property is created to get the path of file. this basically a template where we can simply pass only name of template file & we can get actual body from that template */

        private readonly SMTPConfigModel _smtpConfig;

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)                                                                            /*To send the email we need a public method. we get details from UserEmailOptions cls in this method*/
        {
            userEmailOptions.Subject = UpdatePlaceHolder("Hello {{UserName}}, This is test email subject from book store web app", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolder(GetEmailBody("TestEmail"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);                                                                                                           /*here we are calling SendEmail private method in this public method */
        }

        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions)                                                                            /*To send the email we need a public method. we get details from UserEmailOptions cls in this method*/
        {
            userEmailOptions.Subject = UpdatePlaceHolder("Hello {{UserName}}, Confirm your Email Id", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolder(GetEmailBody("EmailConfirm"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);                                                                                                           /*here we are calling SendEmail private method in this public method */
        }

        public async Task SendEmailForForgotPassword(UserEmailOptions userEmailOptions)                                                                            /*To send the email we need a public method. we get details from UserEmailOptions cls in this method*/
        {
            userEmailOptions.Subject = UpdatePlaceHolder("Hello {{UserName}}, reset your password", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolder(GetEmailBody("ForgotPassword"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);                                                                                                           /*here we are calling SendEmail private method in this public method */
        }

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);                                       /*to enter the username and password for the SMTP. we use network credential class. in the costructor of class we need to pass username and passwrd all done in this line*/

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);                                                                                                          /*here we need to use this sendmailasync method and pass mail object in this method*/
        }
        private string GetEmailBody(string templateName)                                                                                                   /*this method is to read email body from the Email template that we have created in emailTemplate html file. but to reach the file we need path of perticuler file thats done above*/
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));                                                                                               /*we can read the body(above comment) by this File(from System.IO)*/
            return body;
        }

        private string UpdatePlaceHolder(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }
    }
}
