using CNX_Domain.Entities;
using CNX_Domain.Interfaces.Repository;
using CNX_Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CNX_Repository.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : EntityBase<T>
    {
        protected CnxContext _context;
        protected DbSet<T> _dbSet;

        protected Repository(CnxContext context)
        {
            this._context = context;
            _dbSet = _context.Set<T>();
        }

        public void Delete(T obj) =>
            this._dbSet.Remove(obj);

        public void Dispose() =>
            this._context.Dispose();

        public virtual IList<T> GetAll() =>
            this._dbSet.ToList();

        public virtual T GetById(string id) =>
            this._dbSet.Find(id);

        public T GetFirst(Expression<Func<T, bool>> predicate) =>
            this._dbSet.AsNoTracking().FirstOrDefault(predicate);

        public bool HasAny(Expression<Func<T, bool>> predicate) =>
            this._dbSet.AsNoTracking().Any(predicate);

        public T Insert(T obj)
        {
            this._dbSet.Add(obj);
            this._context.SaveChanges();
            return obj;
        }

        public void InsertAll(IList<T> obj) =>
            this._dbSet.AddRange(obj);

        public int SaveChanges() =>
            this._context.SaveChanges();

        public IList<T> Search(Expression<Func<T, bool>> predicate) =>
            this._dbSet.AsNoTracking().Where(predicate).ToList();

        public virtual T Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
