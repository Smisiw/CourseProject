using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data.Repositories
{
    public class CartRepository : IBaseRepository<Cart> 
    {
        private readonly ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Cart entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cart = await _db.Cart.FindAsync(id);
            if (cart != null)
            {
                _db.Cart.Remove(cart);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public  IQueryable<Cart> GetAll()
        {
            return  _db.Cart;
        }

        public bool Update(Cart entity)
        {
            _db.Cart.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
