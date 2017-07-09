using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Assignment6.Models.Utilities
{
    public static class EmailHandler
    {
        //If you get authentication error, You may check last portion of this link @ following link 
        //http://bstechnical.blogspot.com/2011/09/sending-email-in-aspnet.html
        //https://security.google.com/settings/security/activity


        public static int SendEmail(String toEmailAddress, String subject, String body , String senderLogin , string senderPassword)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(senderLogin, senderPassword);

                MailMessage mm = new MailMessage( senderLogin, toEmailAddress, subject, body);
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                client.Send(mm);
                 return 0;
              
             }
            catch (Exception e)
            {
                return 1;
            }

        }
   }
}

