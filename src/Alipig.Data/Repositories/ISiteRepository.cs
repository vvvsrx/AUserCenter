using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;

namespace Alipig.Data.Repositories
{
    public interface ISiteRepository : IRepository<Site>
    {
        bool Add(Site site);

        bool EditById(Site site);

        bool DeleteByIds(Guid[] ids);

        bool SetOrderByIds(Guid[] ids);
    }
}
