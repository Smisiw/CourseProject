using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data.Repositories
{
    public class BrandRepository : IBaseRepository<Brand>
    {
        private readonly ApplicationDbContext _db;
        public BrandRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Brand entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var brand = await _db.Brand.FindAsync(id);
            if (brand != null)
            {
                _db.Brand.Remove(brand);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<Brand> GetAll()
        {
            return _db.Brand;
        }

        public bool Update(Brand entity)
        {
            _db.Brand.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
