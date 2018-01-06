using BankingRules.Models;
using System;
using System.Data.Entity.ModelConfiguration;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.Data.Repository
{
    public class Repository<T> : IDisposable, IRepository<T> where T : BaseEntity
    {
        public readonly DbContext dbContext;
        internal DbSet<T> dbSet;
        public Repository()
        {
            dbContext = new ApplicationDbContext();
            this.dbSet = dbContext.Set<T>();
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> funcExpr)
        {
            var rules = dbContext.Set<T>().Where(funcExpr);
            return rules;
        }
        public IQueryable<T> GetAll()
        {
            var rules = dbContext.Set<T>().Where(p => !p.IsDeleted);
            return rules;
        }
        public T GetById(Guid id)
        {
            var rule = dbContext.Set<T>().FirstOrDefault(p => p.Id == id && !p.IsDeleted);
            return rule;
        }
        public void Update(T t)
        {
            dbContext.Entry(t).State = EntityState.Modified;
        }

        public void UpdateMany(List<T> Entities)
        {
            foreach ( var entity in Entities)
            {
                dbContext.Entry(entity).State = EntityState.Modified;
            }
        }
        public void Add(T t)
        {
            this.dbSet.Add(t);
        }
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
        private bool disposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    dbContext.Dispose();
            }
            this.disposed = true;
        }

        void IDisposable.Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
