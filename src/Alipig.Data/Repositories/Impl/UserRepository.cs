using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;
using Alipig.Framework.Util;

namespace Alipig.Data.Repositories
{
    public class UserRepository : RepositoryNhbImpl<User>, IUserRepository
    {
        private readonly IUserPermissionRepository _userPermissionRepository;
        private readonly IUserRoleMappingRepository _userRoleMappingRepository;

        public UserRepository(IUserPermissionRepository userPermissionRepository, IUserRoleMappingRepository userRoleMappingRepository)
        {
            this._userPermissionRepository = userPermissionRepository;
            this._userRoleMappingRepository = userRoleMappingRepository;
        }

        public bool Add(User user)
        {

            if (GetAllByNotDel().Where(x => x.UserName == user.UserName).RowCount() > 0)
            {
                return false;
            }
            try
            {
                user.PasswordFormat = UserPasswordFormats.EncryptedFromSpaceBuilder;
                user.PasswordSalt = PasswordUtil.GenerateSalt();
                user.Status = UserAccountStatuses.Approved;
                user.Password = PasswordUtil.EncodePassword(user.Password, user.PasswordFormat, user.PasswordSalt);
                Save(user);
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public bool EditPasswordByUserName(string userName, string oldPassword, string newPassword, UserPasswordFormats userPasswordFormat)
        {
            UserAccountStatuses statuses;
            if (this.ValidateUser(userName,oldPassword,out statuses))
            {
                var model = this.GetByUserName(userName);
                if (model == null)
                {
                    return false;
                }
                model.Password = PasswordUtil.EncodePassword(newPassword, userPasswordFormat, model.PasswordSalt);
                model.PasswordFormat = userPasswordFormat;
                Update(model);
                return true;
            }
            return false;
        }

        public bool DeleteByIds(Guid[] ids)
        {
            try
            {
                foreach (var item in ids)
                {
                    //删除用户
                    Delete(item);
                    //删除登录状态

                    //删除登录限制

                    //删除用户权限
                    _userPermissionRepository.DeleteByUserIds(ids);
                    //删除用户临时权限

                    //删除用户角色关系
                    _userRoleMappingRepository.DeleteByRUserIds(ids);
                    //删除操作日志

                }
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public bool SetStatusByIds(Guid[] ids, UserAccountStatuses status)
        {
            try
            {
                foreach (var item in ids)
                {
                    var model = this.Get(item);
                    model.Status = status;
                    this.Update(model);
                }
                return true;
            }
            catch (NHibernate.HibernateException ex)
            {
                return false;
                throw;
            }
        }

        public User GetByUserName(string userName)
        {
            return GetAllByNotDel().Where(x => x.UserName.ToLower() == userName.ToLower()).SingleOrDefault();
        }

        public bool ValidateUser(string userName, string password, out UserAccountStatuses accountStatus)
        {
            accountStatus = UserAccountStatuses.Approved;

            string storedPassword = String.Empty;

            UserPasswordFormats encryptedFromSpaceBuilder = UserPasswordFormats.EncryptedFromSpaceBuilder;

            string passwordSalt = String.Empty;

            dynamic UserPasswordWithFormat_Get = GetAllByNotDel()
                .Where(x => x.UserName.ToLower() == userName.ToLower())
                .Select(x => x.Password, x => x.PasswordFormat, x => x.PasswordSalt, x => x.Status).List<dynamic>().First();

            storedPassword = UserPasswordWithFormat_Get[0];
            encryptedFromSpaceBuilder = UserPasswordWithFormat_Get[1];
            passwordSalt = UserPasswordWithFormat_Get[2];
            accountStatus = UserPasswordWithFormat_Get[3];

            if (String.IsNullOrEmpty(storedPassword))
            {
                return false;
            }

            return PasswordUtil.CheckPassword(password, storedPassword, encryptedFromSpaceBuilder, passwordSalt);
        }
    }
}
