using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.API.Dtos
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "This Field Is Required")]
        public string Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "This Field Is Required")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
