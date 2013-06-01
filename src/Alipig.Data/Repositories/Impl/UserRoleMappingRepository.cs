using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;
using NHibernate.Criterion;

namespace Alipig.Data.Repositories
{
    public class UserRoleMappingRepository : RepositoryNhbImpl<UserRoleMapping>, IUserRoleMappingRepository
    {
        public bool Add(Guid userId, Guid roleId)
        {
            if (Session.Get<User>(userId) == null || Session.Get<Role>(roleId) == null)
            {
                return false;
            }
            if (GetAllAtAll().Where(x=>x.UserId == userId).And(x=>x.RoleId == roleId).RowCount() != 0)
            {
                return false;
            }

            try
            {
                var model = new UserRoleMapping
                {
                    UserId = userId,
                    RoleId = roleId
                };
                Save(model);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public bool DeleteByUserIdAndRoleId(Guid userId, Guid roleId)
        {
            try
            {
                EnsureTransaction();
                var model = Session.QueryOver<UserRoleMapping>()
                    .Where(x => x.UserId == userId)
                    .And(x => x.RoleId == roleId)
                    .SingleOrDefault();
                Session.Delete(model);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                transaction.Rollback();
                return false;
                throw;
            }
        }

        public void DeleteByRoleIds(Guid[] ids)
        {
            try
            {
                var mlist = GetAllAtAll()
                        .Where(x => x.RoleId.IsIn(ids))
                        .List();
                foreach (var item in mlist)
                {
                    this.PhysicsDelete(item);
                }
            }
            catch (NHibernate.HibernateException ex)
            {
                throw;
            }
        }

        public void DeleteByRUserIds(Guid[] ids)
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
            }
            catch (NHibernate.HibernateException ex)
            {
                transaction.Rollback();
                transientEntities.Clear();
            }
        }
    }
}
