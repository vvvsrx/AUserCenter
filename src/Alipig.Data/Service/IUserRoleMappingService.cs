using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Alipig.Data.Service
{
    [ServiceContract]
    public interface IUserRoleMappingService : IDisposable
    {

        bool CreateUserRoleMapping(Guid userId, Guid roleId);

        bool DeleteByUserIdAndRoleId(Guid userId, Guid roleId);
    }
}
