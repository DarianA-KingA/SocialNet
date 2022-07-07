using SocialeNet.Core.Domain.Entities;
using SocialNet.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.Commentary
{
    public class ComentaryViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PublicationId { get; set; }

        public string UserComentary { get; set; }
        public SaveUserViewModel UserOwner { get; set; }
        //navigation property
        public Users User { get; set; }

        public Publications Publication { get; set; }
    }
}
