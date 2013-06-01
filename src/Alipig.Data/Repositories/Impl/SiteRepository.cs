using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;
using NHibernate.Criterion;

namespace Alipig.Data.Repositories
{
    public class SiteRepository : RepositoryNhbImpl<Site>, ISiteRepository
    {
        public bool Add(Site site)
        {
            int? Order = 0;
            Order = GetAllByNotDel()
                .Select(
                    Projections.ProjectionList()
                        .Add(Projections.Max<PermissionGroup>(x => x.Order)))
                        .List<int?>().First();
            if (Order == null)
            {
                Order = 0;
            }

            site.Order = Convert.ToInt32(Order);
            try
            {
                Save(site);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public bool EditById(Site site)
        {
            //var model = Get(id);
            //model.SiteName = name;
            //model.Description = description;
            //model.DomainList = domainList;
            try
            {
                Update(site);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public bool DeleteByIds(Guid[] ids)
        {
            try
            {
                foreach (var item in ids)
                {
                    Delete(item);
                }
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public bool SetOrderByIds(Guid[] ids)
        {
            try
            {
                for (int i = 1; i <= ids.Length; i++)
                {
                    var model = Get(ids[i - 1]);
                    model.Order = i;
                    Update(model);
                }
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }
    }
}
