using OrdersIssuesWebApplication.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrdersIssuesWebApplication.web.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Order> incompletedOrders { get; set; }
    }
}