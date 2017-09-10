using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace OrdersIssuesWebApplication.data
{
    public class Repository
    {
        private string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddOrder(Order order)
        {
            order.Date = DateTime.Now;
            using (var context = new OrdersIssuesDataClassDataContext(_connectionString))
            {
                context.Orders.InsertOnSubmit(order);
                context.SubmitChanges();
            }
        }
        public IEnumerable<Order> GetOrders()
        {
            using(var context = new OrdersIssuesDataClassDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Order>(o => o.Issues);
                context.LoadOptions = loadOptions;
                return context.Orders.ToList();
            }
        }
        public IEnumerable<Order> GetIncompletedOrders()
        {
            return GetOrders().Where(o => !o.Completed).ToList();
        }
        public void MarkComplete(int OrderId)
        {
            using(var context = new OrdersIssuesDataClassDataContext(_connectionString))
            {
                context.Orders.FirstOrDefault(o => o.Id == OrderId).Completed = true;
                context.SubmitChanges();
            }
        }
        public Order GetOrder(int orderId)
        {
            using (var context = new OrdersIssuesDataClassDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Order>(o => o.Issues);
                context.LoadOptions = loadOptions;
                return context.Orders.FirstOrDefault(o => o.Id == orderId);
            }
        }
        public void AddIssue(Issue issue)
        {
            using (var context = new OrdersIssuesDataClassDataContext(_connectionString))
            {
                context.Issues.InsertOnSubmit(issue);
                context.SubmitChanges();
            }
        }
        public void MarkResolved(int issueId)
        {
            using (var context = new OrdersIssuesDataClassDataContext(_connectionString))
            {
                context.Issues.FirstOrDefault(i => i.Id == issueId).Resolved = true;
                context.SubmitChanges();
            }
        }
        public IEnumerable<Issue> GetIssues(int orderId)
        {
            using (var context = new OrdersIssuesDataClassDataContext(_connectionString))
            {
                var loadOptions = new DataLoadOptions();
                loadOptions.LoadWith<Issue>(i => i.Order);
                context.LoadOptions = loadOptions;
                return context.Issues.Where(i => i.OrderId == orderId).ToList();
            }
        }

    }
}
