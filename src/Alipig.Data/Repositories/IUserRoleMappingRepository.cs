using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;

namespace Alipig.Data.Repositories
{
    public interface IUserRoleMappingRepository : IRepository<UserRoleMapping>
    {
        bool Add(Guid userId, Guid roleId);

        bool DeleteByUserIdAndRoleId(Guid userId, Guid roleId);

        void DeleteByRoleIds(Guid[] ids);

        void DeleteByRUserIds(Guid[] ids);
    }
}
