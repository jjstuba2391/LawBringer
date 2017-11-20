using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LawBringer.Models;

namespace LawBringer.Controllers
{ 
    public class CheckoutController : Controller
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
        // GET: Checkout
        public ActionResult Index()
        {
            Models.CheckoutDetails details = new Models.CheckoutDetails();
            Guid cartID = Guid.Parse(Request.Cookies["cartID"].Value);

            details.CurrentCart = db.Carts.Find(cartID);

            return View(details);
        }
        // POST: Checkout
        [HttpPost]
        public ActionResult Index(Models.CheckoutDetails model)
        {          
            Guid cartID = Guid.Parse(Request.Cookies["cartID"].Value);
            model.CurrentCart = db.Carts.Find(cartID);
            if (ModelState.IsValid)
            {
                string trackingnNumber = Guid.NewGuid().ToString().Substring(0, 8);
                db.Orders.Add(new Order
                {
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow,
                    TrackingNumber = trackingnNumber,
                    Tax = (model.CurrentCart.Lawyer.Price + model.CurrentCart.HelpType.StandardPrice ?? 0) * .1025m,
                    ServiceCharge = (model.CurrentCart.Lawyer.Price + model.CurrentCart.HelpType.StandardPrice ?? 0) * .10m,
                    Total = (model.CurrentCart.Lawyer.Price + model.CurrentCart.HelpType.StandardPrice ?? 0) *1.2025m,                 
                    Email = model.ContactEmail,
                    CustomerName = model.ContactName,
                    ShippingAddress1 = model.ShippingAddress,
                    ShippingCity = model.ShippingCity,
                    ShippingPostalCode = model.ShippingPostalCode,
                    ShippingState = model.ShippingState,
                    Day = model.CurrentCart.Day,
                    HelpTypeID = model.CurrentCart.HelpTypeID,
                    LawyerID = model.CurrentCart.LawyerID
                });
                db.SaveChanges();
                //TODO: send some confirmation emails to the person placing the order and the system admin
                //TODO: Reset the cart

                LawDoggsEmailService emailService = new LawDoggsEmailService();
                emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                {
                    Subject = "Your tracking number Information" + trackingnNumber,
                    Destination = model.ContactEmail,
                    Body = "Thank you for your support"
                });

                return RedirectToAction("Index", "Orders", new { id = trackingnNumber });
            }
            return View(model);
        }

    }
}
