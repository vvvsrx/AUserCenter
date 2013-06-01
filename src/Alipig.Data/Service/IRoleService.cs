using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    [ServiceContract]
    public interface IRoleService : IDisposable
    {
        bool CreateRole(Role role);

        bool ModifyRole(Role role);

        void DeleteRole(Guid id);

        bool DeleteByIds(Guid[] ids);

        bool SetOrderByIds(Guid[] ids);

        Role GetByAutoId(int id);

        IList<Guid> GetSiteIdsByIds(Guid[] ids);

        IList<Role> GetAllBySiteId(Guid siteId);

        IList<Role> GetAllBySiteIdAndUserId(Guid siteId, Guid userId);
    }
}
