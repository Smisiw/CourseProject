using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data.Repositories
{
    public class CellPhoneRepository : IBaseRepository<CellPhone>
    {
        private readonly ApplicationDbContext _db;
        public CellPhoneRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(CellPhone entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cellPhone = await _db.CellPhone.FindAsync(id);
            if (cellPhone != null)
            {
                _db.CellPhone.Remove(cellPhone);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public  IQueryable<CellPhone> GetAll()
        {
            return  _db.CellPhone;
        }

        public bool Update(CellPhone entity)
        {
            _db.CellPhone.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
