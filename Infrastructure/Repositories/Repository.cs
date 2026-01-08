using Domain.IRepositories;
using Domain.Models;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<Entity> : IRepository<Entity> where Entity : BaseModel
    {
        private readonly Context _context;
        private readonly DbSet<Entity> _dbSet;
        private readonly string[] immutableProps = { nameof(BaseModel.Id), nameof(BaseModel.CreatedAt) };
        public Repository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }
        public async Task Add(Entity entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        public async Task<Guid> AddAsync(Entity entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _dbSet.AddAsync(entity);
            return entity.Id;
        }
        public async Task<bool> SaveIncludeAsync(Entity entity, params string[] properties)
        {
            try
            {
                var localEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
                EntityEntry entry;

                if (localEntity is null)
                {
                    _dbSet.Attach(entity);
                    entry = _context.Entry(entity);
                }
                else
                {
                    entry = _context.Entry(localEntity);
                }

                if (entry == null)
                {
                    return false;
                }


                foreach (var property in entry.Properties)
                {
                    if (properties.Contains(property.Metadata.Name) && !immutableProps.Contains(property.Metadata.Name))
                    {
                        property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name)?.GetValue(entity);
                        property.IsModified = true;
                    }
                }

                entity.CreatedAt = DateTime.UtcNow;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task DeleteAsync(Entity entity)
        {
            entity.IsDeleted = true;
            await SaveIncludeAsync(entity, nameof(BaseModel.IsDeleted));
        }

        public async Task HardDelete(Entity entity)
        {
            _dbSet.Remove(entity);
        }


        public IQueryable<Entity> Get(Expression<Func<Entity, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public IQueryable<Entity> GetAll()
        {
            return _dbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<Entity> GetAllWithDeleted()
        {
            return _dbSet;
        }

        public async Task<Entity> GetByIDAsync(Guid id)
        {
            return await Get(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await Get(predicate).AnyAsync();
        }
        public async Task AddRangeAsync(IEnumerable<Entity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }
        public async Task DeleteRangeAsync(ICollection<Entity> entities)
        {
            foreach (var entity in entities)
            {

                entity.IsDeleted = true;
                await SaveIncludeAsync(entity, nameof(entity.IsDeleted));
            }
        }


        public async Task<Entity> GetWithIncludeAsync(Guid id, params string[] include)
        {
            IQueryable<Entity> query = _dbSet;

            foreach (var parm in include)
            {
                query = query.Include(parm);
            }

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task HardDeleteAsync(Entity entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task HardDeleteRangeAsync(ICollection<Entity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public async Task<int> CountAsync(Expression<Func<Entity, bool>> predicate = null)
        {
            if (predicate == null)
                return await _dbSet.CountAsync();

            return await _dbSet.CountAsync(predicate);
        }
    }
}
