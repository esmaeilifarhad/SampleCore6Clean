using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Dto.Account
{
    public class UserLoginDto
    {
        //[Display(Name = "پسورد")]
        //[Required(ErrorMessage = " باید تکمیل شود.")]
        //public string test { get; set; }

        [Display(Name = "پسورد")]
        [Required(ErrorMessage = " باید تکمیل شود.")]
        public string Password { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "  باید تکمیل شود.")]
        public string Username { get; set; }
        public string ReturnUrl { get; set; }
    }
}
