using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
