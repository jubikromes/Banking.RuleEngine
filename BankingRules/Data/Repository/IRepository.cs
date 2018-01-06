using BankingRules.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankingRules.Data.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {       
        T GetById(Guid id);
        IQueryable<T> Find(Expression<Func<T, bool>>  funcExpr);
        IQueryable<T> GetAll();
        void Update(T t);
        void SaveChanges();
        void Add(T t);
    }
}
