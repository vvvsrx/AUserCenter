using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Data.Service;
using Alipig.Framework.Entities;

namespace Alipig.Data.Extensions
{
    public class MiscService : IMiscService
    {
        private readonly IPermissionGroupService _permissionGroupService;
        private readonly IPermissionItemService _permissionItemService;
        public MiscService(IPermissionGroupService permissionGroupService, IPermissionItemService permissionItemService)
        {
            this._permissionGroupService = permissionGroupService;
            this._permissionItemService = permissionItemService;
        }

        public List<PermissionGroupItemsModel> GetPermissionGroupItemsBySiteId(Guid siteId)
        {
            // 一次性获取所有PermissionItem，不必循环由PermissionGroupId获取
            List<PermissionGroupItemsModel> list = new List<PermissionGroupItemsModel>();

            IList<PermissionGroup> groups = _permissionGroupService.GetAllBySiteId(siteId);
            IList<PermissionItem> items = _permissionItemService.GetAllBySiteId(siteId);
            foreach (PermissionGroup group in groups)
            {
                PermissionGroupItemsModel groupItem = new PermissionGroupItemsModel
                {
                    Group = group
                };
                foreach (PermissionItem item in items)
                {
                    if (item.PermissionGroupId == group.ID)
                    {
                        groupItem.Items.Add(item);
                    }
                }

                list.Add(groupItem);
            }

            return list;
        }
    }
}
