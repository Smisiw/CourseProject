using CourseProject.Data.Models;

namespace CourseProject.Data.Interfaces
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetAll();
        Task<bool> CreateAsync(T entity);
        bool Update(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
