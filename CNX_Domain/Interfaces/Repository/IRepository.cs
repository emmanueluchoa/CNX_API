using CNX_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CNX_Domain.Interfaces.Repository
{
    public interface IRepository<T> : IDisposable where T : EntityBase<T>
    {
        T Insert(T obj);
        void InsertAll(IList<T> obj);
        T GetById(string id);
        IList<T> Search(Expression<Func<T, bool>> predicate);
        IList<T> GetAll();
        T GetFirst(Expression<Func<T, bool>> predicate);
        bool HasAny(Expression<Func<T, bool>> predicate);
        T Update(T obj);
        void Delete(T obj);
        int SaveChanges();
    }
}
