using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocker.Web.ViewModels
{
    public class UserLoginViewModel
    {
        [EmailAddress]
        [UIHint("Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [PasswordPropertyText]
        [UIHint("Password")]
        public string Password { get; set; }
    }
}
