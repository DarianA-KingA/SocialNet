using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeNet.Core.Domain.Entities
{
    public class Friend
    {
        public int Id { get; set; }

        public int FromId { get; set; }
        public int ToId { get; set; }
         //Navigation Property 
        public User User { get; set; }


    }
}
