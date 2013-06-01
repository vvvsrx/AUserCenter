using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Entities
{
    public enum UserAccountStatuses
    {
        ApprovalPending = 0,
        Approved = 1,
        Banned = 2,
        Disapproved = 3,
        NotActive = 11
    }
}
