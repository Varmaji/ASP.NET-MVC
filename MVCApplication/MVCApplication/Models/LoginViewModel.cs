using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCApplication.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="Username is Required")]
        [MinLength(5,ErrorMessage ="Username should be atleasst 5 letters")]
        [Display(Name ="User Name: ")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string  Password { get; set; }
        [Display(Name = "Keep me signed In: ")]
        public bool Rememberme { get; set; }
    }
}