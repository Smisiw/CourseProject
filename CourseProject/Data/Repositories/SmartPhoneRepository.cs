using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data.Repositories
{
    public class SmartPhoneRepository : IBaseRepository<SmartPhone>
    {
        private readonly ApplicationDbContext _db;
        public SmartPhoneRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(SmartPhone entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var smartPhone = await _db.SmartPhone.FindAsync(id);
            if (smartPhone != null)
            {
                _db.SmartPhone.Remove(smartPhone);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<SmartPhone> GetAll()
        {
            return _db.SmartPhone;
        }

        public bool Update(SmartPhone entity)
        {
            _db.SmartPhone.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
