using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Data.Repositories;
using Alipig.Framework;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public class UserRoleMappingService : Disposable, IUserRoleMappingService
    {
        private readonly IUserRoleMappingRepository _userRoleMappingRepository;

        public UserRoleMappingService(IUserRoleMappingRepository userRoleMappingRepository)
        {
            this._userRoleMappingRepository = userRoleMappingRepository;
        }

        public bool CreateUserRoleMapping(Guid userId, Guid roleId)
        {
            var result = _userRoleMappingRepository.Add(userId, roleId);
            _userRoleMappingRepository.Commit();
            return result;
        }

        public bool DeleteByUserIdAndRoleId(Guid userId, Guid roleId)
        {
            var result = _userRoleMappingRepository.DeleteByUserIdAndRoleId(userId, roleId);
            _userRoleMappingRepository.Commit();
            return result;
        }
    }
}
