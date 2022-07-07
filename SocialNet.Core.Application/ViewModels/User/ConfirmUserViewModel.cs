using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.User
{
    public class ConfirmUserViewModel
    {
        [Required(ErrorMessage = "Coloque un nombre de usuario")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        public bool status { get; set; }
        [Required(ErrorMessage ="Coloque el codigo de confirmacion")]
        public string ConfirmationCode { get; set; }
    }
}
