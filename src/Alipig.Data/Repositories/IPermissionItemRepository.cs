using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;

namespace Alipig.Data.Repositories
{
    public interface IPermissionItemRepository : IRepository<PermissionItem>
    {
        bool Add(Guid permissionGroupId, string code, string displayName, PermissionItemDisplayStyle displayStyle, string jsonDataUrl, string jsonDataConst);

        bool EditById(Guid id, Guid permissionGroupId, string code, string displayName, PermissionItemDisplayStyle displayStyle, string jsonDataUrl, string jsonDataConst);

        bool DeleteByIds(Guid[] ids);

        bool DeleteByGroupId(Guid groupid);

        bool SetOrderByIds(Guid[] ids);

        PermissionItem GetBySiteIdAndCode(Guid siteId, string code);

        IList<Guid> GetSiteIdsByIds(Guid[] ids);

        IList<PermissionItem> GetAllBySiteId(Guid siteId);

        IList<PermissionItem> GetAllByPermissionGroupId(Guid permissionGroupId);
    }
}
