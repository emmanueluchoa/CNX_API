using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CNX_Domain.Models
{
    public class ResetPasswordUserVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords not match!")]
        public string RepeatNewPassword { get; set; }
    }
}
