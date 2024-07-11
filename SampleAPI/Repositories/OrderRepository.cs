using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public int AddNewOrder(Order order)
        {
            
            using (var context = new SampleApiDbContext())
            {
                context.Orders.Add(order);
                context.SaveChanges();
                return order.Id;
            }
        }

        public List<Order> GetAll()
        {
            using (var context = new SampleApiDbContext())
            {
              return context.Orders.ToList();
                
            }
        }

        public List<Order> GetAllDeleted()
        {
            using (var context = new SampleApiDbContext())
            {
                return context.Orders.Where(order => order.Deleted == true).ToList();

            }
        }

        public List<Order> GetAllNotDeleted()
        {
            using (var context = new SampleApiDbContext())
            {
                return context.Orders.Where(order => order.Deleted == false).ToList();

            }
        }

        public List<Order> GetRecentOrder()
        {
            using (var context = new SampleApiDbContext())
            {
                var oneDayAgo = DateTime.UtcNow.AddDays(-1);
                return context.Orders
                    .Where(o => o.Deleted == false && o.EntryDate >= oneDayAgo)
                    .OrderByDescending(o => o.EntryDate)
                    .ToList();
            }
        }

        public bool IsRecordExists(string name)
        {
            using (var context = new SampleApiDbContext())
            {
                if(context.Orders.Any(order => order.ProductName == name))
                    return true;
                else
                    return false; ;
            }
        }
    }
}
