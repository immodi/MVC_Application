namespace WebApplicaiton.DAL.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Receiver { get; set; }
    }
}
