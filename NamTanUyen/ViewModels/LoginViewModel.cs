using System.ComponentModel.DataAnnotations;

namespace NamTanUyen.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRemember { get; set; }
    }
}