using SocialeNet.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNet.Core.Application.ViewModels.Friend
{
    public class FriendViewModel
    {
        public int Id { get; set; }

        public int FromId { get; set; }

        public int ToId { get; set; }

        //Navigation Property 
        public Users User { get; set; }
    }
}
