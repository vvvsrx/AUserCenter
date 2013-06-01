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
    public class Site : Entity
    {
        [DataMember]
        [Display(Name = "网站名称")]
        public virtual string SiteName { get; set; }
        [DataMember]
        [Display(Name = "网站描述")]
        public virtual string Description { get; set; }
        [DataMember]
        public virtual int Order { get; set; }
        [DataMember]
        [Display(Name = "域名列表")]
        public virtual string DomainList { get; set; }
    }
}
