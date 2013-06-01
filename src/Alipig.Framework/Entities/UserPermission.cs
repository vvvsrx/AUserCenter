using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Alipig.Framework.Entities
{

    [DataContract]
    [Serializable]
    public class UserPermission : Entity
    {
        [DataMember]
        public virtual Guid UserId { get; set; }
        [DataMember]
        public virtual Guid SiteId { get; set; }
        [DataMember]
        public virtual string PermissionJsonData { get; set; }
    }
}
