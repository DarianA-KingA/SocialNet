using SocialeNet.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeNet.Core.Domain.Entities
{
    public class Users: AuditableBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool status { get; set; }
        public string ConfirmationCode { get; set; }

        //navigation property
        public ICollection<Publications> Publications { get; set; }
        public ICollection<Comentary> Comentaries { get; set; }

        public ICollection<Friends> Friends { get; set; }
    }
}
