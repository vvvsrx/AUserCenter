using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Data.Extensions
{
    public interface IMiscService
    {
        List<PermissionGroupItemsModel> GetPermissionGroupItemsBySiteId(Guid systemId);
    }
}
