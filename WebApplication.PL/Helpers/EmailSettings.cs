using System.Net;
using System.Net.Mail;
using WebApplicaiton.DAL.Models;

namespace WebApplication.PL.Helpers
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("immodi.business@gmail.com", "slpg nlrw ijdk qoxj");
            client.Send("immodi.business@gmail.com", email.Receiver, email.Subject, email.Body);
        }
    }
}
