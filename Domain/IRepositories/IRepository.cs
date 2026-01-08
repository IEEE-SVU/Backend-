using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface  IRepository<Entity> where Entity : BaseModel
    {
        Task Add(Entity entity);
        Task<bool> SaveIncludeAsync(Entity entity, params string[] properties);
        //Task SaveInclude(Entity entity, params string[] properties);
        Task DeleteAsync(Entity entity);
        Task HardDelete(Entity entity);
        IQueryable<Entity> GetAll();
        IQueryable<Entity> GetAllWithDeleted();
        IQueryable<Entity> Get(Expression<Func<Entity, bool>> predicate);

        Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate);
        // Task<Entity> GetByID(int id);

        Task<Entity> GetByIDAsync(Guid id);
        Task<Guid> AddAsync(Entity entity);
        Task AddRangeAsync(IEnumerable<Entity> entities);
        Task DeleteRangeAsync(ICollection<Entity> entities);
        Task HardDeleteAsync(Entity entity);
        Task HardDeleteRangeAsync(ICollection<Entity> entity);
        Task SaveChangesAsync();
    }
}
