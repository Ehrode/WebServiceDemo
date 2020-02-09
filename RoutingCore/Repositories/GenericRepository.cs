using WebServiceDemo.Core.Interfaces;
using WebServiceDemo.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using WebServiceDemo.Core.Helpers;

namespace WebServiceDemo.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _table;
        public GenericRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public virtual async Task<T> GetById(object id, IEnumerable<string> includes = null)
        {
            var set = ((IObjectContextAdapter)_context).ObjectContext.CreateObjectSet<T>();
            var entitySet = set.EntitySet;
            var key = entitySet.ElementType.KeyMembers[0];
            var keyProperty  = typeof(T).GetProperty(key.Name);
            
            var tableAsQueryable = _table.AsQueryable();

            if (includes != null)
            {
                foreach (var includeString in includes)
                {
                    tableAsQueryable = tableAsQueryable.Include(includeString);
                }
            }

            return await tableAsQueryable
                .FirstOrDefaultAsync(PropertyEqualsExtension.PropertyEquals<T, object>(keyProperty, id));
        }

        public virtual async Task<IEnumerable<T>> Find(Specification<T> specification = null,
            IEnumerable<string> includes = null, int page = 0, int pageSize = 0)
        {
            var records = _table
                .Where(specification != null ? specification.ToExpression() : e => true);

            if (includes != null)
            {
                foreach (var includeString in includes)
                {
                    records = records.Include(includeString);
                }
            }

            records.Skip(page * pageSize);

            return await (pageSize > 0 ? records.Take(pageSize) : records).ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _table.ToListAsync();
        }

        public virtual async Task Insert(T obj)
        {
            _table.Add(obj);
            await _context.SaveChangesAsync();
        }
        public virtual async Task Update(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public virtual async Task Delete(object id)
        {
            var existing = _table.Find(id);
            _table.Remove(existing ?? throw new InvalidOperationException());
            await _context.SaveChangesAsync();
        }
        public virtual async Task Save()
        {
           await _context.SaveChangesAsync();
        }
    }
}
