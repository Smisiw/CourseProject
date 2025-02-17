using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data.Repositories
{
    public class OrderItemRepository : IBaseRepository<OrderItem>
    {
        private readonly ApplicationDbContext _db;
        public OrderItemRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(OrderItem entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orderItem = await _db.OrderItem.FindAsync(id);
            if (orderItem != null)
            {
                _db.OrderItem.Remove(orderItem);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public  IQueryable<OrderItem> GetAll()
        {
            return  _db.OrderItem;
        }

        public bool Update(OrderItem entity)
        {
            _db.OrderItem.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
