using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Entities
{
    /// <summary>
    /// 用户性别
    /// </summary>
    [Serializable]
    public enum UserGender : byte
    {
        /// <summary>
        /// 男
        /// </summary>
        Male = 0,

        /// <summary>
        /// 女
        /// </summary>
        Female = 1
    }
}
