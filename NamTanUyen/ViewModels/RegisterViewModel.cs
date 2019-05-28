using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NamTanUyen.ViewModels
{
    public class RegisterViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }

        public string Phone { get; set; }
    }
}