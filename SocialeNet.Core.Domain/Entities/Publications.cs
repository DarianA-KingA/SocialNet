using SocialeNet.Core.Domain.Common;
using System.Collections.Generic;

namespace SocialeNet.Core.Domain.Entities
{
    public class Publications : AuditableBaseEntity
    {
        public int UserId { get; set; }
        public string Body { get; set; }
        public string ImageUrl { get; set; }


        //navigation property
        public User User { get; set; }
        public ICollection<Comentary> Comentaries { get; set; }


    }
}
