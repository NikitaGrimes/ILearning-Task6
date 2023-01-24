using System.ComponentModel.DataAnnotations;

namespace Task4.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
