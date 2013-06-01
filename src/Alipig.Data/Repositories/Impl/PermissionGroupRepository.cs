using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;
using NHibernate.Criterion;

namespace Alipig.Data.Repositories
{
    public class PermissionGroupRepository : RepositoryNhbImpl<PermissionGroup> , IPermissionGroupRepository
    {

        private readonly IPermissionItemRepository _permissionItemRepository;

        public PermissionGroupRepository(IPermissionItemRepository permissionItemRepository)
        {
            this._permissionItemRepository = permissionItemRepository;
        }

        public bool Add(Guid siteId, string name)
        {
            int? Order;
            Order = GetAllAtAll().Where(x=>x.SiteId == siteId)
                .Select(
                    Projections.ProjectionList()
                        .Add(Projections.Max<PermissionGroup>(x=>x.Order)))
                        .List<int?>().First();
            if (Order == null)
            {
                Order = 0;
            }
            PermissionGroup pg = new PermissionGroup 
            { 
                SiteId = siteId,
                Name = name,
                Order = Convert.ToInt32(Order)
            };
            try
            {
                Save(pg);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
            }
        }

        public bool EditById(Guid id, string name)
        {
            var model = Get(id);
            model.Name = name;

            try
            {
                Update(model);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
            }
        }

        public bool DeleteByIds(Guid[] ids)
        {
            try
            {
                foreach (var item in ids)
                {
                    Delete(item);
                    _permissionItemRepository.DeleteByGroupId(item);
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
                    var tx = ids[i-1];
                    var model = Load(tx);
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

        public IList<Guid> GetSiteIdsByIds(Guid[] ids)
        {
            return GetAllAtAll()
                .Where(x => x.ID.IsIn(ids))
                .SelectList(list => list
                    .Select(Projections.Distinct(Projections.Property<PermissionGroup>(x => x.SiteId))))
                    .OrderBy(x=>x.Order).Asc
                    .List<Guid>();
        }

        public IList<PermissionGroup> GetAllBySiteId(Guid siteId)
        {
            return GetAllAtAll()
                .Where(x => x.SiteId == siteId)
                .Where(x => x.IsDel == 0)
                .OrderBy(x => x.Order).Asc.List();
        }
    }
}
