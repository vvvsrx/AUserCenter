using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Entities
{
    /// <summary>
    /// 权限项显示样式
    /// </summary>
    [Serializable]
    public enum PermissionItemDisplayStyle : byte
    {
        /// <summary>
        /// 单选框
        /// </summary>
        CheckBox = 0,

        /// <summary>
        /// 文本框
        /// </summary>
        TextBox = 1,

        /// <summary>
        /// 下拉框
        /// </summary>
        DropDownList = 2,

        /// <summary>
        /// 树视图
        /// </summary>
        TreeView = 3
    }
}
