using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    [ServiceContract]
    public interface ISiteService : IDisposable
    {
        IList<Site> GetAllSite();

        bool CreateSite(Site site);

        bool ModifySite(Site site);

        void DeleteSite(int id);

        bool AddDomain(Guid id, string domain);

        bool IsDomainOnly(Guid id, string domain);

        Site GetSite(int id);

        Site GetSite(Guid id);
    }
}
