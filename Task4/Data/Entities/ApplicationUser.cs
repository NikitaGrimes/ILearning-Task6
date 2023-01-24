using Microsoft.AspNetCore.Identity;

namespace Task4.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
           
        }

        public ApplicationUser(string userName)
        {
            UserName = userName;
            Id = Guid.NewGuid().ToString();
        }
    }
}
