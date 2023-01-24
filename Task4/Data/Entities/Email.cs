namespace Task4.Data.Entities
{
    public class Email
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Body { get; set; }

        public string Sender { get; set; }

        public ApplicationUser Recipient { get; set; }

        public Email(string title, string body, string sender, ApplicationUser recipient)
        {
            Id = "a" + Guid.NewGuid().ToString();
            Title = title;
            CreatedDate = DateTime.Now;
            Body = body;
            Sender = sender;
            Recipient = recipient;
        }

        public Email()
        {

        }
    }
}
