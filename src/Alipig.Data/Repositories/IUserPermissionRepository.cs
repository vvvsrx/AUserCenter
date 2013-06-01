using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;

namespace Alipig.Data.Repositories
{
    public interface IUserPermissionRepository : IRepository<UserPermission>
    {
        bool AddOrEditByUserIdAndSiteId(Guid userId, Guid siteId, string permissionJsonData);

        UserPermission GetByUserIdAndSiteId(Guid userId, Guid siteId);

        bool DeleteByUserIds(Guid[] ids);
    }
}
