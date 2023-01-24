using Task4.Data.Entities;

namespace Task4.Models
{
    public class UserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public static implicit operator UserModel(ApplicationUser appUser)
        {
            return new UserModel
            {
                Id = appUser.Id,
                UserName = appUser.UserName
            };
        }
    }
}
