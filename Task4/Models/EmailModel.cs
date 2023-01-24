using Task4.Data.Entities;

namespace Task4.Models
{
    public class EmailModel
    {
        public string Id { get; set; }

        public string Sender { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }

        public static implicit operator EmailModel(Email email)
        {
            return new EmailModel
            {
                Id = email.Id,
                Sender = email.Sender,
                Title = email.Title,
                Body = email.Body,
                CreatedDate = email.CreatedDate
            };
        }
    }
}
