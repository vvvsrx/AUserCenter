using System;
using System.Collections.Generic;
using Alipig.Framework.Entities;
using Alipig.Framework.Util;
using NHibernate;

namespace Alipig.Framework.NHHelper
{
    public class RepositoryNhbImpl<T> : Disposable, IRepository<T> where T : Entity
    {
        public RepositoryNhbImpl()
        {
            transientEntities = new List<Entity>();
        }

        protected virtual ISession Session
        {
            get { return NHibernateHelper.CreateSession(); }
        }
        protected readonly ICollection<Entity> transientEntities;
        protected ITransaction transaction;

        public virtual T Load(Guid id)
        {
            return Session.Load<T>(id);
        }

        public virtual T Get(Guid id)
        {
            return Session.Get<T>(id);
        }

        public virtual IList<T> GetAll()
        {
            return Session.QueryOver<T>().Where(x => x.IsDel == 0).OrderBy(x => x.ID).Desc.List<T>();
        }

        public virtual IQueryOver<T, T> GetAllAtAll()
        {
            return Session.QueryOver<T>();
        }


        public IQueryOver<T, T> GetAllByNotDel()
        {
            return Session.QueryOver<T>().Where(x=>x.IsDel == 0);
        }

        public virtual Guid Save(T entity)
        {
            try
            {
                EnsureTransaction();
                entity.CreateTime = DateTime.Now;
                entity.UpdateTime = DateTime.Now;
                entity.IsDel = 0;
                transientEntities.Add(entity);
                return entity.ID;
            }
            catch
            {
                transaction.Rollback();
                transientEntities.Clear();
                return new Guid();
                throw;
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                EnsureTransaction();
                entity.UpdateTime = DateTime.Now;
                Session.Update(entity);
            }
            catch
            {
                transaction.Rollback();
                transientEntities.Clear();
            }
        }

        public virtual void Delete(Guid id)
        {
            try
            {
                EnsureTransaction();
                var item = Session.Get<T>(id);
                item.UpdateTime = DateTime.Now;
                item.IsDel = 1;
                Session.Update(item);
            }
            catch
            {
                transaction.Rollback();
                transientEntities.Clear();
            }
        }

        public virtual void PhysicsDelete(T entity)
        {
            try
            {
                EnsureTransaction();
                Session.Delete(entity);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public virtual void Commit()
        {
            EnsureTransaction();
            try
            {
                transientEntities.Each(e => Session.Save(e));
                transaction.Commit();
                transientEntities.Clear();
            }
            catch
            {
                transaction.Rollback();
                transientEntities.Clear();
                throw;
            }
        }

        protected override void DisposeCore()
        {
            transientEntities.Clear();
            if (transaction != null)
            {
                if (transaction.IsActive)
                {
                    transaction.Rollback();
                }

                transaction.Dispose();
                transaction = null;
            }
            if (Session != null)
            {
                if (Session.IsOpen)
                {
                    Session.Close();
                }
                Session.Dispose();
            }
        }


        protected void EnsureTransaction()
        {
            if (transaction == null || !transaction.IsActive || transaction.WasCommitted || transaction.WasRolledBack)
            {
                transaction = Session.BeginTransaction();
            }
        }


        public T Get(int id)
        {
            return Session.QueryOver<T>().Where(x=>x.AutoId == id).SingleOrDefault();
        }
    }
}
