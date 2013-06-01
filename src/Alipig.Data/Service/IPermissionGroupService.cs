using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public interface IPermissionGroupService : IDisposable
    {
        bool CreatePermissionGroup(PermissionGroup model);

        bool ModifyPermissionGroup(PermissionGroup model);

        bool ModifyPermissionGroup(Guid id,string name);

        void DeletePermissionGroup(Guid id);

        IList<PermissionGroup> GetAllBySiteId(Guid siteId);

        PermissionGroup GetById(Guid id);

        bool SetOrderByIds(Guid[] ids);
    }
}
