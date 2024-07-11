using SampleAPI.Entities;
using SampleAPI.Requests;

namespace SampleAPI.Repositories
{
    public interface IOrderRepository
    {
        // TODO: Create repository methods.

        // Suggestions for repo methods:
        // public GetRecentOrders();
        // public AddNewOrder();
        public List<Order> GetAll();
        public List<Order> GetAllDeleted();
        public List<Order> GetAllNotDeleted();
        public int AddNewOrder(Order order);
        public List<Order> GetRecentOrder();
        public bool IsRecordExists(string name);
    }
}
