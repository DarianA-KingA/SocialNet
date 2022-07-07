using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.Publication
{
    public class SavePublicationViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Debe colocar alguna descripcion")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public int PublicationId { get; set; }

        public string[] UserComentary { get; set; }
    }
}
