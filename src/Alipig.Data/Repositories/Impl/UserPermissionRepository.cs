using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;
using NHibernate;
using NHibernate.Criterion;

namespace Alipig.Data.Repositories
{
    public class UserPermissionRepository : RepositoryNhbImpl<UserPermission>, IUserPermissionRepository
    {

        public bool AddOrEditByUserIdAndSiteId(Guid userId, Guid siteId, string permissionJsonData)
        {
            if (Session.Get<User>(userId) == null || Session.Get<Site>(siteId) == null)
            {
                return false;
            }
            try
            {
                var model = GetAllAtAll().Where(x => x.UserId == userId).And(x => x.SiteId == siteId).SingleOrDefault<UserPermission>();
                if (model != null)
                {
                    model.PermissionJsonData = permissionJsonData;
                    Update(model);
                }
                else
                {
                    model = new UserPermission
                    {
                        UserId = userId,
                        SiteId = siteId,
                        PermissionJsonData = permissionJsonData
                    };
                    Save(model);
                }
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public UserPermission GetByUserIdAndSiteId(Guid userId, Guid siteId)
        {
            if (Session.Get<User>(userId) != null 
                && Session.Get<Site>(siteId) != null
                && GetAllAtAll().Where(x=>x.UserId == userId).And(x=>x.SiteId == siteId).RowCount() == 0)
            {
                var model = new UserPermission
                {
                    UserId = userId,
                    SiteId = siteId,
                    PermissionJsonData = "{}"
                };
                try
                {
                    Save(model);
                    return GetAllAtAll().Where(x => x.UserId == userId).And(x => x.SiteId == siteId).SingleOrDefault();
                }
                catch (NHibernate.HibernateException ex)
                {
                    return null;
                    throw;
                }
            }
            else
            {
                return null;
            }
        }


        public bool DeleteByUserIds(Guid[] ids)
        {
            try
            {
                var modelList = GetAllByNotDel().Where(x => x.UserId.IsIn(ids)).List<UserPermission>();

                EnsureTransaction();
                foreach (var item in modelList)
                {
                    item.UpdateTime = DateTime.Now;
                    item.IsDel = 1;
                    Session.Update(item);
                }
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                transaction.Rollback();
                transientEntities.Clear();
                return false;
            }
        }
    }
}
