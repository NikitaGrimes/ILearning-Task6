using Task4.Data.Entities;

namespace Task4.Data
{
    public interface IApplicationRepository
    {
        IEnumerable<ApplicationUser> GetAllUsers();

        ApplicationUser GetUserByUserName(string name);

        bool AddUser(ApplicationUser user);

        bool IsUserExist(string userName);

        IEnumerable<Email> GetEmailsByUserName(string name);

        bool AddEmail(Email email);
    }
}
