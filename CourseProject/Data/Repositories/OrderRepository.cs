using CourseProject.Data.Interfaces;
using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateAsync(Order entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _db.Order.FindAsync(id);
            if (order != null)
            {
                _db.Order.Remove(order);
                await _db.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public  IQueryable<Order> GetAll()
        {
            return  _db.Order;
        }


        public bool Update(Order entity)
        {
            _db.Order.Update(entity);
            _db.SaveChanges();
            return true;
        }
    }
}
