using LawBringer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawBringer.Controllers
{
    public class LawyerController : Controller
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
        // GET: Product/List
        public ActionResult List()
        {
            return View(db.Lawyers);
        }
        // GET: Product
        public ActionResult Index(int? id)
        {
            return View(db.Lawyers.Find(id));
        }


        [HttpPost]


        public ActionResult Index(Lawyer model, int helpType, DateTime daySelected)
        {

            Guid cartID;
            Cart cart = null;
            if (Request.Cookies.AllKeys.Contains("cartID"))
            {

                cartID = Guid.Parse(Request.Cookies["cartID"].Value);
                cart = db.Carts.Find(cartID);
            }
            if (cart == null)
            {
                cartID = Guid.NewGuid();
                cart = new Cart
                {
                    ID = cartID,
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow
                };
                db.Carts.Add(cart);
                Response.AppendCookie(new HttpCookie("cartID", cartID.ToString()));
            }

            
            
            cart.LawyerID = model.Id;
            cart.Day = daySelected;
            cart.HelpTypeID = helpType;

            cart.DateLastModified = DateTime.UtcNow;

            db.SaveChanges();


            TempData.Add("NewItem", model.Name);


            return RedirectToAction("Index", "Cart");

        }     
    }
}