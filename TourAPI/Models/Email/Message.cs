using MimeKit;

namespace User.Manage.API.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string Content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(e => new MailboxAddress("email", e)));
            this.Subject = subject;
            this.Content = Content;
        }
    }
}
