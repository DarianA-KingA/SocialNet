using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.Commentary
{
    public class SaveComentaryViewModel
    {
        public int UserId { get; set; }

        public int PublicationId { get; set; }

        public string UserComentary { get; set; }
    }
}
