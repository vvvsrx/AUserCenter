using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Alipig.Framework.Entities
{

    [DataContract]
    [Serializable]
    public class UserRoleMapping : Entity
    {
        [DataMember]
        public virtual Guid UserId { get; set; }
        [DataMember]
        public virtual Guid RoleId { get; set; }

        public virtual Role RoleRow { get; set; }
    }
}
