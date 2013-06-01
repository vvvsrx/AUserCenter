using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Framework.Entities;
using Alipig.Framework.NHHelper;

namespace Alipig.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        bool Add(User user);

        bool EditPasswordByUserName(string userName, string oldPassword, string newPassword, UserPasswordFormats userPasswordFormat);

        bool DeleteByIds(Guid[] ids);

        bool SetStatusByIds(Guid[] ids, UserAccountStatuses status);

        User GetByUserName(string userName);

        bool ValidateUser(string userName, string password, out UserAccountStatuses accountStatus);
    }
}
