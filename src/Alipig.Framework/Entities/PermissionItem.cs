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
    public class PermissionItem : Entity
    {
        [DataMember]
        [Display(Name = "所属分组")]
        public virtual Guid PermissionGroupId { get; set; }
        [DataMember]
        [Display(Name = "权限代码")]
        public virtual string Code { get; set; }
        [DataMember]
        [Display(Name = "显示名称")]
        public virtual string DisplayName { get; set; }
        [DataMember]
        [Display(Name = "显示类型")]
        public virtual PermissionItemDisplayStyle DisplayStyle { get; set; }
        [DataMember]
        [Display(Name = "Json数据地址")]
        public virtual string JsonDataUrl { get; set; }
        [DataMember]
        [Display(Name = "Json数据常量")]
        public virtual string JsonDataConst { get; set; }
        [DataMember]
        public virtual int Order { get; set; }
        public virtual PermissionGroup PermissionGroupRow { get; set; }
    }
}
