using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data.Repositories
{
    public class UserRepository : IBaseRepository<User>
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(User entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _db.User.FindAsync(id);
            if (user != null)
            {
                _db.User.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public  IQueryable<User> GetAll()
        {
            return  _db.User;
        }

        public bool Update(User entity)
        {
            _db.User.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
