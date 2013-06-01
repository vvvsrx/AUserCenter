using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using NHibernate.Transform;

namespace Alipig.Data.Repositories
{
    public class RoleRepository : RepositoryNhbImpl<Role>, IRoleRepository
    {

        private readonly IUserRoleMappingRepository _userRoleMappingRepository;

        public RoleRepository(IUserRoleMappingRepository userRoleMappingRepository)
        {
            this._userRoleMappingRepository = userRoleMappingRepository;
        }

        public bool Add(Role role)
        {
            int? Order;
            Order = GetAllAtAll().Where(x => x.SiteId == role.SiteId)
                .Select(
                    Projections.ProjectionList()
                        .Add(Projections.Max<PermissionGroup>(x => x.Order)))
                        .List<int?>().First();
            if (Order == null)
            {
                Order = 0;
            }
            role.Order = Convert.ToInt32(Order);
            try
            {
                Save(role);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public bool EditById(Role role)
        {
            var model = Get(role.ID);
            try
            {
                model.Name = role.Name;
                model.Description = role.Description;
                Update(model);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public bool SetPermissionJsonDataById(Guid id, string permissionJsonData)
        {
            var model = Get(id);
            try
            {
                model.PermissionJsonData = permissionJsonData;
                Update(model);
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
                _userRoleMappingRepository.DeleteByRoleIds(ids);
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

        public IList<Guid> GetSiteIdsByIds(Guid[] ids)
        {
            return GetAllAtAll()
                .Where(x => x.ID.IsIn(ids))
                .SelectList(list => list
                    .Select(Projections.Distinct(Projections.Property<PermissionGroup>(x => x.SiteId))))
                    .List<Guid>();
        }

        public IList<Role> GetAllBySiteId(Guid siteId)
        {
            return GetAllByNotDel()
                .Where(x => x.SiteId == siteId)
                .OrderBy(x => x.Order).Asc
                .ThenBy(x => x.CreateTime).Asc
                .List<Role>();
        }

        public IList<Role> GetAllBySiteIdAndUserId(Guid siteId, Guid userId)
        {
            //UserRoleMapping userrolemapping = null;
            Role role = null;
            return Session.QueryOver<UserRoleMapping>()
                .Left.JoinAlias(urm => urm.RoleRow, () => role)
                .Where(() => role.SiteId == siteId)
                .And(urm => urm.UserId == userId)
                .OrderBy(() => role.Order).Asc
                .ThenBy(() => role.CreateTime).Asc
                .SelectList(list=>list
                    .Select(() => role.ID).WithAlias(() => role.ID)
                    .Select(() => role.IsDel).WithAlias(() => role.IsDel)
                    .Select(() => role.CreateTime).WithAlias(() => role.CreateTime)
                    .Select(() => role.UpdateTime).WithAlias(() => role.UpdateTime)
                    .Select(() => role.Version).WithAlias(() => role.Version)
                    .Select(() => role.AutoId).WithAlias(() => role.AutoId)
                    .Select(() => role.SiteId).WithAlias(() => role.SiteId)
                    .Select(() => role.Name).WithAlias(() => role.Name)
                    .Select(() => role.Description).WithAlias(() => role.Description)
                    .Select(() => role.Order).WithAlias(() => role.Order)
                    .Select(() => role.PermissionJsonData).WithAlias(() => role.PermissionJsonData))
                .TransformUsing(Transformers.AliasToBean<Role>())
                .List<Role>();
        }
    }
}
