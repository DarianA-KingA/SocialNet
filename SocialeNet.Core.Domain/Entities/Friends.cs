using SocialeNet.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeNet.Core.Domain.Entities
{
    public class Friends: AuditableBaseEntity
    {

        public int FromId { get; set; }

        public int ToId { get; set; }
         
        //Navigation Property 
        public Users User { get; set; }


    }
}
