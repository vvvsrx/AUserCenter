using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alipig.Data.Repositories;
using Alipig.Framework;
using Alipig.Framework.Entities;

namespace Alipig.Data.Service
{
    public class UserService : Disposable, IUserService
    {
        
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public IList<User> GetAllUser()
        {
            return _userRepository.GetAll();
        }


        public bool CreateUser(User user)
        {
            var result = _userRepository.Add(user);
            _userRepository.Commit();
            return result;
        }

        public bool ModifyUser(User user)
        {
            try
            {
                _userRepository.Update(user);
                _userRepository.Commit();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void DeleteUser(Guid id)
        {
            _userRepository.Delete(id);
            _userRepository.Commit();
        }

        public User GetById(Guid id)
        {
            return _userRepository.Get(id);
        }

        public User GetByAutoId(int id)
        {
            return _userRepository.Get(id);
        }
    }
}
