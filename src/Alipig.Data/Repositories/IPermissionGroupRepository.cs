using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;

namespace Alipig.Data.Repositories
{
    public interface IPermissionGroupRepository : IRepository<PermissionGroup>
    {
        bool Add(Guid siteId, string name);

        bool EditById(Guid id, string name);

        bool DeleteByIds(Guid[] ids);

        bool SetOrderByIds(Guid[] ids);

        IList<Guid> GetSiteIdsByIds(Guid[] ids);

        IList<PermissionGroup> GetAllBySiteId(Guid siteId);
    }
}
