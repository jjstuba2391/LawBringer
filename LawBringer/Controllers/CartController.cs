using LawBringer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawBringer.Controllers
{
    public class CartController : Controller
    {
        protected DBLawDoggsEntities db = new DBLawDoggsEntities();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Cart
        public ActionResult Index()
        {
            Guid cartID = Guid.Parse(Request.Cookies["CartID"].Value);
            

            return View(db.Carts.Find(cartID));
        }
        // POST: Cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.Cart model)
        {
            var cart = db.Carts.Find(model.ID);
            db.SaveChanges();
            return View(cart);
        }
    }
}