using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data.Repositories
{
    public class PhoneRepository : IBaseRepository<Phone>
    {
        private readonly ApplicationDbContext _db;
        public PhoneRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Phone entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var phone = await _db.Phone.FindAsync(id);
            if (phone != null)
            {
                _db.Phone.Remove(phone);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<Phone> GetAll()
        {
            return  _db.Phone;
        }

        public bool Update(Phone entity)
        {
            _db.Phone.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
