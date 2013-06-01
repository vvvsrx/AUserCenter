using System;
using System.Collections.Generic;
using Alipig.Framework.Entities;
using NHibernate;
namespace Alipig.Framework.NHHelper
{
    public interface IRepository<T> where T : Entity
    {
        T Load(Guid id);
        T Get(Guid id);
        T Get(int id);
        IList<T> GetAll();
        IQueryOver<T, T> GetAllAtAll();
        IQueryOver<T, T> GetAllByNotDel();
        Guid Save(T entity);
        void Update(T entity);
        void Delete(Guid id);
        void PhysicsDelete(T entity);
        void Commit();
    }
}
