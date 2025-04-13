using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.CodeDom;

namespace Project.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Create
        void Add(TEntity entity);
        // Read
        Task<IEnumerable<TEntity>> GetAllAsync();
        // Update
        void Update(TEntity entity);
        // Delete
        void Delete(TEntity entity);
        // Additional
        Task<TEntity?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}