using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NgCookingModel.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
        public Repository(DbContext dbcontext) 
        {
             if (dbcontext == null)
                throw new ArgumentNullException("dbcontext");

            DbContext = dbcontext; 
            DbSet = DbContext.Set<T>();

        }
        public void Add(T entity)
        {
            DbSet.Add(entity);
        } 

        public void Delete(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException(); 
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = DbSet;
            return query.ToList(); 
        }

        public T GetById(int id)
        {
            return DbSet.Find(id); 
        }

        public IEnumerable<T> GetMany(System.Linq.Expressions.Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;  
        }
    }
}
