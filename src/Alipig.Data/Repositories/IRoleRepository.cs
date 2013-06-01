using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;

namespace Alipig.Data.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        bool Add(Role role);

        bool EditById(Role role);

        bool SetPermissionJsonDataById(Guid id, string permissionJsonData);

        bool DeleteByIds(Guid[] ids);

        bool SetOrderByIds(Guid[] ids);

        IList<Guid> GetSiteIdsByIds(Guid[] ids);

        IList<Role> GetAllBySiteId(Guid siteId);

        IList<Role> GetAllBySiteIdAndUserId(Guid siteId, Guid userId);
    }
}
