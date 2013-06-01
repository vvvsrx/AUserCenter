using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    [ServiceContract]
    public interface IUserPermissionService : IDisposable
    {
        [OperationContract]
        bool AddOrEditByUserIdAndSystemId(Guid userId, Guid systemId, string permissionJsonData);

        [OperationContract]
        UserPermission GetByUserIdAndSystemId(Guid userId, Guid systemId);
    }
}
