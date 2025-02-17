using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;

namespace CourseProject.Data.Repositories
{
    public class CartItemRepository : IBaseRepository<CartItem>
    {
        private readonly ApplicationDbContext _db;
        public CartItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(CartItem entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cartItem = await _db.CartItem.FindAsync(id);
            if (cartItem != null)
            {
                _db.CartItem.Remove(cartItem);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public IQueryable<CartItem> GetAll()
        {
            return _db.CartItem;
        }

        public bool Update(CartItem entity)
        {
            _db.CartItem.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
