using SocialeNet.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeNet.Core.Domain.Entities
{
    public  class Comentary: AuditableBaseEntity
    {
        public int UserId { get; set; }
        public int PublicationId { get; set; }

        public string UserComentary { get; set; }
        //navigation property
        public Users User { get; set; }

        public Publications Publication { get; set; }

    }
}
