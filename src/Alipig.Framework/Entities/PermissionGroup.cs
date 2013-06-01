using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Alipig.Framework.Entities
{
    [DataContract]
    [Serializable]
    public class PermissionGroup : Entity
    {
        [DataMember]
        public virtual Guid SiteId { get; set; }
        [DataMember]
        [Display(Name = "分组名称")]
        public virtual string Name { get; set; }
        [DataMember]
        public virtual int Order { get; set; }
    }
}
