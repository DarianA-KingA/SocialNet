using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.User
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Coloque el nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Coloque la password")]
        [DataType(DataType.Text)]
        public string Password { get; set; }

    }
}
