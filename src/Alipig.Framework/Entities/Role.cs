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
    public class Role : Entity
    {
        [DataMember]
        [Display(Name = "所属网站")]
        public virtual Guid SiteId { get; set; }
        [DataMember]
        [Display(Name = "角色名")]
        public virtual string Name { get; set; }
        [DataMember]
        [Display(Name = "角色描述")]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual int Order { get; set; }
        [DataMember]
        public virtual string PermissionJsonData { get; set; }
    }
}
