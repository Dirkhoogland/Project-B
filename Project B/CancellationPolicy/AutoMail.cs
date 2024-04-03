using System.Net;
using System.Net.Mail;

class AutoMail{
    public static void SendMail(string recipient, string subject, string body){
        using SmtpClient client = new SmtpClient("smtp.gmail.com"){
            Port = 587,
            Credentials = new NetworkCredential("newsouthconfirmation@gmail.com", "newsouthprojectb"),
            EnableSsl = true
        };

        MailMessage message = new MailMessage{
            From = new MailAddress("newsouthconfirmation@gmail.com"),
            Subject = subject,
            Body = body,
            IsBodyHtml = false
        };

        message.To.Add(recipient);

        client.Send(message);
    }
}