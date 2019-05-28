using System.ComponentModel.DataAnnotations;

namespace NamTanUyen.ViewModels
{
    public class UpdatePassViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPass { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string NewsPass { get; set; }
        [Compare("NewsPass", ErrorMessage = "Password not match")]
        [DataType(DataType.Password)]
        [Required]
        public string NewPassConfirm { get; set; }
    }
}