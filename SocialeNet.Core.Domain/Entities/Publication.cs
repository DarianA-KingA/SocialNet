using SocialeNet.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeNet.Core.Domain.Entities
{
    public class Publication : AuditableBaseEntity
    {
        public int UserId { get; set; }

        //navigation property
        public User User { get; set; }
        public ICollection<Picture> Pictures { get; set; }
        public ICollection<Comentary> Comentaries { get; set; }


    }
}
