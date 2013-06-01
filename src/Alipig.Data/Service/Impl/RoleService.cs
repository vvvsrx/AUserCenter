using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Data.Repositories;
using Alipig.Framework;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public class RoleService : Disposable, IRoleService
    {
        private IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        public bool CreateRole(Role role)
        {
            var result = _roleRepository.Add(role);
            _roleRepository.Commit();
            return result;
        }

        public bool ModifyRole(Role role)
        {
            var result = _roleRepository.EditById(role);
            _roleRepository.Commit();
            return result;
        }

        public void DeleteRole(Guid id)
        {
            _roleRepository.Delete(id);
            _roleRepository.Commit();
        }

        public IList<Guid> GetSiteIdsByIds(Guid[] ids)
        {
            throw new NotImplementedException();
        }

        public IList<Role> GetAllBySiteId(Guid siteId)
        {
            return _roleRepository.GetAllBySiteId(siteId);
        }

        public IList<Role> GetAllBySiteIdAndUserId(Guid siteId, Guid userId)
        {
            return _roleRepository.GetAllBySiteIdAndUserId(siteId, userId);
        }


        public Role GetByAutoId(int id)
        {
            return _roleRepository.Get(id);
        }


        public bool DeleteByIds(Guid[] ids)
        {
            var result = _roleRepository.DeleteByIds(ids);
            _roleRepository.Commit();
            return result;
        }


        public bool SetOrderByIds(Guid[] ids)
        {
            var result = _roleRepository.SetOrderByIds(ids);
            _roleRepository.Commit();
            return result;
        }
    }
}
