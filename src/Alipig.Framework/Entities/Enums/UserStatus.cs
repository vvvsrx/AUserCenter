using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Entities
{
    /// <summary>
    /// 用户状态
    /// </summary>
    [Serializable]
    public enum UserStatus : byte
    {
        /// <summary>
        /// 正常的
        /// </summary>
        Natural = 0,

        /// <summary>
        /// 禁用的
        /// </summary>
        Disabled = 1
    }
}
