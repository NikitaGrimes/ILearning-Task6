namespace Task4.Models
{
    public class EmailCreatingModel
    {
        public string Title { get; set; }

        public string Body { get; set; }

        public string RecipientName { get; set; }

        public List<string> UserNames { get; set;}
    }
}
