using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialeNet.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public int Id { get; set; }//items's id
        public string CreatedBy { get; set; }//creator name
        public DateTime CreatedDate { get; set; }//creation date
        public string LastModifiedBy { get; set; }//name last person who modified the register
        public DateTime? LastModifiedDate { get; set; }//date of the last modification


    }
}
