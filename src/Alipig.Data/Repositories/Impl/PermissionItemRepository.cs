using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Alipig.Data.Repositories
{
    public class PermissionItemRepository : RepositoryNhbImpl<PermissionItem>, IPermissionItemRepository
    {
        public bool Add(Guid permissionGroupId, string code, string displayName, PermissionItemDisplayStyle displayStyle, string jsonDataUrl, string jsonDataConst)
        {
            try
            {
                PermissionGroup perGroup = Session.Get<PermissionGroup>(permissionGroupId);
                if (perGroup == null)
                {
                    return false;
                }
                Guid siteid = perGroup.SiteId;
                PermissionItem perItem = null;

                var piresult = Session.CreateSQLQuery("select 1 from t_permissionitem a left join t_permissiongroup b on a.F_PermissionGroupId=b.F_Id where b.F_SiteId='"+ siteid +"' and a.F_Code='"+ code +"'").List<dynamic>();

                //var piresult = Session.QueryOver<PermissionItem>(() => perItem)
                //    .Left.JoinAlias(x => x.PermissionGroupId, () => perGroup.ID)
                //    .Where(x => x.Code == code)
                //    .And(x => x.IsDel == 0)
                //    .And(() => perGroup.SiteId == siteid).RowCount();
                if (piresult.Count != 0)
                {
                    return false;
                }

                int? Order;
                Order = GetAllAtAll().Where(x => x.PermissionGroupId == permissionGroupId)
                    .And(x => x.DisplayName == displayName)
                    .Select(
                        Projections.ProjectionList()
                            .Add(Projections.Max<PermissionGroup>(x => x.Order)))
                            .List<int?>().First();
                if (Order == null)
                {
                    Order = 0;
                }
                var model = new PermissionItem
                {
                    PermissionGroupId = permissionGroupId,
                    Code = code,
                    DisplayName = displayName,
                    DisplayStyle = displayStyle,
                    JsonDataUrl = jsonDataUrl,
                    JsonDataConst = jsonDataConst,
                    Order = Convert.ToInt32(Order)
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

        public bool EditById(Guid id, Guid permissionGroupId, string code, string displayName, PermissionItemDisplayStyle displayStyle, string jsonDataUrl, string jsonDataConst)
        {
            try
            {
                PermissionGroup perGroup = Session.Get<PermissionGroup>(permissionGroupId);
                if (perGroup == null)
                {
                    return false;
                }
                var perItem = Get(id);
                perItem.Code = code;
                perItem.PermissionGroupId = permissionGroupId;
                perItem.DisplayName = displayName;
                perItem.DisplayStyle = displayStyle;
                perItem.JsonDataUrl = jsonDataUrl;
                perItem.JsonDataConst = jsonDataConst;
                Update(perItem);
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

        public bool DeleteByGroupId(Guid groupid)
        {
            try
            {
                EnsureTransaction();
                var item = Session.QueryOver<PermissionItem>().Where(x=>x.IsDel == 0).Where(x => x.PermissionGroupId == groupid).List <PermissionItem>().First();
                item.UpdateTime = DateTime.Now;
                item.IsDel = 1;
                Session.Update(item);
                return true;
            }
            catch
            {
                transaction.Rollback();
                transientEntities.Clear();
                return false;
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

        public PermissionItem GetBySiteIdAndCode(Guid siteId, string code)
        {
            PermissionItem perItem = null;
            PermissionGroup perGroup = null;
            return Session.QueryOver<PermissionItem>(() => perItem)
                    .Left.JoinAlias(x => x.PermissionGroupId, () => perGroup.ID)
                    .Where(x => x.Code == code)
                    .And(() => perGroup.SiteId == siteId)
                    .And(x => x.IsDel == 0)
                    .OrderBy(x => x.Order).Asc
                    .Select(x=>x)
                    .SingleOrDefault<PermissionItem>();
           // throw new NotImplementedException();
        }

        public IList<Guid> GetSiteIdsByIds(Guid[] ids)
        {
            PermissionItem perItem = null;
            PermissionGroup perGroup = null;
            return Session.QueryOver<PermissionItem>(() => perItem)
                    .Left.JoinAlias(x => x.PermissionGroupId, () => perGroup.ID)
                    .Where(x => x.ID.IsIn(ids))
                    .SelectList(list => list
                        .Select(Projections.Distinct(Projections.Property<PermissionGroup>(x => x.SiteId))))
                    .List<Guid>();
        }

        public IList<PermissionItem> GetAllBySiteId(Guid siteId)
        {
            var list = GetAll();
            PermissionItem perItem = null;
            PermissionGroup perGroup = null;
            return Session.QueryOver<PermissionItem>()
                    .Left.JoinAlias(pi => pi.PermissionGroupRow, () => perGroup)
                    .Where(() => perGroup.SiteId == siteId)
                    .And(pi => pi.IsDel == 0)
                    .OrderBy(pi => pi.Order).Asc
                    .ThenBy(pi => pi.CreateTime).Asc
                    .List<PermissionItem>();
            //Session.CreateCriteria<PermissionItem>()
            //    .CreateCriteria("PermissionGroup","pg",NHibernate.SqlCommand.JoinType.LeftOuterJoin)

            //return Session.CreateSQLQuery("select a.* from t_permissionitem a left join t_permissiongroup b on a.F_PermissionGroupId=b.F_Id where b.F_SiteId='"+ siteId.ToString() +"' order by a.F_Order asc, a.F_CreateTime asc").List<PermissionItem>();
        }

        public IList<PermissionItem> GetAllByPermissionGroupId(Guid permissionGroupId)
        {
            return GetAllAtAll()
                .Where(x => x.PermissionGroupId == permissionGroupId)
                .OrderBy(x => x.Order).Asc
                .ThenBy(x => x.CreateTime).Asc
                .List<PermissionItem>();
        }
    }
}
