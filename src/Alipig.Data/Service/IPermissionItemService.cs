using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public interface IPermissionItemService : IDisposable
    {
        bool CreatePermissionItem(PermissionItem model);

        bool ModifyPermissionItem(PermissionItem model);

        void DeletePermissionItem(Guid[] ids);

        bool SetOrderByIds(Guid[] ids);

        IList<PermissionItem> GetAllBySiteId(Guid siteId);

        IList<PermissionItem> GetAllByPermissionGroupId(Guid permissionGroupId);
    }
}
