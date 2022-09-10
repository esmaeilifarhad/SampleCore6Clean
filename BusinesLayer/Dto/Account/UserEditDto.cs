using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Dto.Account
{
    public class UserEditDto
    {
        [Required(ErrorMessage = "این قسمت باید تکمیل شود.")]
        [EmailAddress(ErrorMessage = "ایمیل صحیح نیست.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "این قسمت باید تکمیل شود.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "شماره تلفن درست نیست.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "این قسمت باید تکمیل شود.")]
        [StringLength(maximumLength: 20, MinimumLength = 6, ErrorMessage = "می بایست بین 6 تا 20 کاراکتر باشد.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "این قسمت باید تکمیل شود.")]
        [Compare("Password", ErrorMessage = "می بایست با رمز اصلی یکسان باشد.")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "این قسمت باید تکمیل شود.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "این قسمت باید تکمیل شود.")]
        public string LastName { get; set; }
    }
}
