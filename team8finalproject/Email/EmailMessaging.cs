using System;

//TODO: You need to add these using statements to get mail to work
using System.Net.Mail;
using System.Net;
namespace team8finalproject
{
    public static class EmailMessaging
    {
        public static void SendEmail(String toEmailAddress, String emailSubject, String emailBody)
        {
            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                //This is the SENDING email address and password
                Credentials = new NetworkCredential("team8finalproject@gmail.com", "Fa19team8"),
                EnableSsl = true
            };
            //Add anything that you need to the body of the message
            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "Sent by Team 8! Bevo's Bank and Trust.";


            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("team8finalproject@gmail.com", "Bevo's Bookstore");
            MailMessage mm = new MailMessage();
            mm.Subject = "Team 8 - " + emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }
    }
}