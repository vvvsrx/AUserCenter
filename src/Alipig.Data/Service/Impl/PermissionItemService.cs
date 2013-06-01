using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Data.Repositories;
using Alipig.Framework;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public class PermissionItemService : Disposable, IPermissionItemService
    {
        private IPermissionItemRepository _permissionItemRepository;
        public PermissionItemService(IPermissionItemRepository permissionItemRepository)
        {
            this._permissionItemRepository = permissionItemRepository;
        }

        public bool CreatePermissionItem(PermissionItem model)
        {
            var result = _permissionItemRepository.Add(model.PermissionGroupId, model.Code, model.DisplayName, model.DisplayStyle, model.JsonDataUrl, model.JsonDataConst);
            _permissionItemRepository.Commit();
            return result;
        }

        public bool ModifyPermissionItem(PermissionItem model)
        {
            var result = _permissionItemRepository.EditById(model.ID, model.PermissionGroupId, model.Code, model.DisplayName, model.DisplayStyle, model.JsonDataUrl, model.JsonDataConst);
            _permissionItemRepository.Commit();
            return result;
        }

        public void DeletePermissionItem(Guid[] ids)
        {
            _permissionItemRepository.DeleteByIds(ids);
            _permissionItemRepository.Commit();
        }

        public IList<PermissionItem> GetAllBySiteId(Guid siteId)
        {
            return _permissionItemRepository.GetAllBySiteId(siteId);
        }


        public IList<PermissionItem> GetAllByPermissionGroupId(Guid permissionGroupId)
        {
            return _permissionItemRepository.GetAllByPermissionGroupId(permissionGroupId);
        }


        public bool SetOrderByIds(Guid[] ids)
        {
            var result = _permissionItemRepository.SetOrderByIds(ids);
            _permissionItemRepository.Commit();
            return result;
        }
    }
}
