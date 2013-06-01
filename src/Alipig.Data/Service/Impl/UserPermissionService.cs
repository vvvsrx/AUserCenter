using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public class UserPermissionService : Disposable, IUserPermissionService
    {
        public bool AddOrEditByUserIdAndSystemId(Guid userId, Guid systemId, string permissionJsonData)
        {
            throw new NotImplementedException();
        }

        public UserPermission GetByUserIdAndSystemId(Guid userId, Guid systemId)
        {
            throw new NotImplementedException();
        }
    }
}
