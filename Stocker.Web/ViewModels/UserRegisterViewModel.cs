using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stocker.Web.ViewModels
{
    public class UserRegisterViewModel
    {
        [EmailAddress]
		public string Email { get; set; }
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [PasswordPropertyText]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

}
