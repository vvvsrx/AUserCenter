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
    public class User : Entity
    {
        [DataMember]
        [Display(Name = "用户名")]
        public virtual string UserName { get; set; }
        [DataMember]
        [Display(Name = "密码")]
        public virtual string Password { get; set; }
        [DataMember]
        [Display(Name = "密码确认")]
        public virtual string passwordConfirm { get; set; }
        [DataMember]
        public virtual string PasswordSalt { get; set; }
        [DataMember]
        public virtual UserPasswordFormats PasswordFormat { get; set; }
        [DataMember]
        [Display(Name = "E-Mail")]
        public virtual string PrivateEmail { get; set; }
        [DataMember]
        public virtual Guid MembershipID { get; set; }
        [DataMember]
        [Display(Name = "昵称")]
        public virtual string Nickname { get; set; }
        [DataMember]
        public virtual string IPCreated { get; set; }
        [DataMember]
        public virtual string IPLastActivity { get; set; }
        [DataMember]
        public virtual UserAccountStatuses Status { get; set; }
        [DataMember]
        public virtual UserGender Gender { get; set; }
    }
}
