using Task4.Data.Entities;

namespace Task4.Data
{
    public class ApplicaionRepository : IApplicationRepository
    {
        private readonly ApplicationContext _context;

        public ApplicaionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser GetUserByUserName(string name)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == name);
        }

        public bool AddUser(ApplicationUser user)
        {
            _context.Users.Add(user);
            return SaveAll();
        }

        public bool IsUserExist(string userName)
        {
            ApplicationUser user = GetUserByUserName(userName);
            if (user == null)
                return false;

            return true;
        }

        private bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Email> GetEmailsByUserName(string name)
        {
            return _context.Emails.Where(x => x.Recipient.UserName == name)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
        }

        public bool AddEmail(Email email)
        {
            _context.Emails.Add(email);
            return SaveAll();
        }
    }
}
