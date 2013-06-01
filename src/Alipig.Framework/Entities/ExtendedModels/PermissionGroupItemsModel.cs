using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Alipig.Framework.Entities
{
    [DataContract]
    [Serializable]
    public class PermissionGroupItemsModel
    {
        /// <summary>
        /// 权限组
        /// </summary>
        [DataMember]
        public PermissionGroup Group { get; set; }

        /// <summary>
        /// 权限项列表
        /// </summary>
        [DataMember]
        public List<PermissionItem> Items { get; set; }

        public PermissionGroupItemsModel()
        {
            Group = new PermissionGroup();
            Items = new List<PermissionItem>();
        }
    }
}
