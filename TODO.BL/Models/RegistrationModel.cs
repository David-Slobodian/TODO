using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.BL.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Please, enter a username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
