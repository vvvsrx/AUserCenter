using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Data.Repositories;
using Alipig.Framework;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public class PermissionGroupService :Disposable, IPermissionGroupService
    {
        private readonly IPermissionGroupRepository _permissionGroupRepository;

        public PermissionGroupService(IPermissionGroupRepository permissionGroupRepository)
        {
            this._permissionGroupRepository = permissionGroupRepository;
        }

        public bool CreatePermissionGroup(PermissionGroup model)
        {
            var result = _permissionGroupRepository.Add(model.SiteId, model.Name);
            _permissionGroupRepository.Commit();
            return result;
        }

        public bool ModifyPermissionGroup(PermissionGroup model)
        {
            var result = _permissionGroupRepository.EditById(model.ID, model.Name);
            _permissionGroupRepository.Commit();
            return result;
        }

        public void DeletePermissionGroup(Guid id)
        {
            var ids = new Guid[1];
            ids[0] = id;
            _permissionGroupRepository.DeleteByIds(ids);
            _permissionGroupRepository.Commit();
        }

        public IList<PermissionGroup> GetAllBySiteId(Guid siteId)
        {
            return _permissionGroupRepository.GetAllBySiteId(siteId);
        }


        public PermissionGroup GetById(Guid id)
        {
            return _permissionGroupRepository.Get(id);
        }


        public bool SetOrderByIds(Guid[] ids)
        {
            var result = _permissionGroupRepository.SetOrderByIds(ids);
            _permissionGroupRepository.Commit();
            return result;
        }


        public bool ModifyPermissionGroup(Guid id, string name)
        {
            var result = _permissionGroupRepository.EditById(id, name);
            _permissionGroupRepository.Commit();
            return result;
        }
    }
}
