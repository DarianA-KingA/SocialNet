using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.Commentary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.Publication
{
    public class PublicationViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }
        public List<ComentaryViewModel> ComentaryPublication { get; set; }


        //navigation property
        public Users User { get; set; }
        public ICollection<Comentary> Comentaries { get; set; }

    }
}
