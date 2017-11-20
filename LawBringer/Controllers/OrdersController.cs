using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LawBringer.Models
{
    public class OrdersController : Controller
    {
        private DBLawDoggsEntities db = new DBLawDoggsEntities();

        // GET: Orders
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Single(x => x.TrackingNumber == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
