using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alipig.Framework.Entities
{
    public enum LoginUserStatuses
    {
        AccountBanned = 3,
        AccountDisapproved = 4,
        AccountPending = 2,
        InvalidCredentials = 0,
        Success = 1,
        UnknownError = 100
    }
}
