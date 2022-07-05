using SocialeNet.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeNet.Core.Domain.Entities
{
    public class Picture : AuditableBaseEntity
    {
        public int PublicationId { get; set; }
        public string ImageUrl { get; set; }
        //navigation property
        public Publication Publication { get; set; }

    }
}
