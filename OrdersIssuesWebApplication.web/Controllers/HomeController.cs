using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrdersIssuesWebApplication.data;
using OrdersIssuesWebApplication.web.Models;

namespace OrdersIssuesWebApplication.web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();
            Repository rep = new Repository(Properties.Settings.Default.ConStr);
            ivm.incompletedOrders = rep.GetIncompletedOrders();
            return View(ivm);
        }
        public ActionResult NewOrder()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult NewOrder(Order order)
        {
            Repository rep = new Repository(Properties.Settings.Default.ConStr);
            rep.AddOrder(order);
            return Redirect("/home/index");
        }
        [HttpPost]
        public void MarkComplete(int orderId)
        {
            Repository rep = new Repository(Properties.Settings.Default.ConStr);
            rep.MarkComplete(orderId);
        }
        public ActionResult GetOrders()
        {
            Repository rep = new Repository(Properties.Settings.Default.ConStr);
            var result = rep.GetIncompletedOrders().Select(o => new {
                id = o.Id,
                title = o.Title,
                amount = o.Amount,
                date = o.Date,
                Issues = o.Issues.Select(i => new
                {
                    note = i.Note,
                    orderId = i.OrderId,
                    resolved = i.Resolved
                }).ToList()
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SeeDetails(int orderId)
        {
            Repository rep = new Repository(Properties.Settings.Default.ConStr);
            return View(new SeeDetailsViewModel
            {
                Order = rep.GetOrder(orderId)
            });
        }
        public ActionResult NewIssue(int orderId)
        {
            return View(orderId);
        }
        [HttpPost]
        public ActionResult NewIssue(Issue issue)
        {
            Repository rep = new Repository(Properties.Settings.Default.ConStr);
            rep.AddIssue(issue);
            return Redirect($"/home/SeeDetails?orderId={issue.OrderId}");
        }
        public void MarkResolved(int issueId)
        {
            Repository rep = new Repository(Properties.Settings.Default.ConStr);
            rep.MarkResolved(issueId);
        }
        public ActionResult GetIssues(int orderId)
        {
            Repository rep = new Repository(Properties.Settings.Default.ConStr);
            var issues = rep.GetIssues(orderId);
            var result = rep.GetIssues(orderId).Select(i => new {
                id = i.Id,
                note = i.Note,
                resolved = i.Resolved,
                order = new  
                {
                    title = i.Order.Title,
                    date = i.Order.Date,
                    id = i.Order.Id
                }
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}