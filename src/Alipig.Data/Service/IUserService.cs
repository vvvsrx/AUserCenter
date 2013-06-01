using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    [ServiceContract]
    public interface IUserService : IDisposable
    {
        IList<User> GetAllUser();

        bool CreateUser(User user);

        bool ModifyUser(User user);

        void DeleteUser(Guid id);

        User GetById(Guid id);

        User GetByAutoId(int id);
    }
}
