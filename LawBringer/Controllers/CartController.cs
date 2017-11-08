using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawBringer.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            var cart = Models.Cart.BuildCart(Request);

            return View(cart);
        }
        // POST: Cart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Models.Cart model)
        {
            model.Tax = model.Lawyers[0].Price * .1025m;
            model.ServiceCharge = model.Lawyers[0].Price * .10m;
            model.Total = model.Lawyers[0].Price + model.Tax + model.ServiceCharge;

            return View(model); 
        }

        
    }
}