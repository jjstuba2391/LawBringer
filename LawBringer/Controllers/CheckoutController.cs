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
                string trackingNumber = Guid.NewGuid().ToString().Substring(0, 8);
                decimal Tax = (model.CurrentCart.Lawyer.Price + model.CurrentCart.HelpType.StandardPrice ?? 0) * .1025m;
                decimal ServiceCharge = (model.CurrentCart.Lawyer.Price + model.CurrentCart.HelpType.StandardPrice ?? 0) * .10m;
                decimal Total = (model.CurrentCart.Lawyer.Price + model.CurrentCart.HelpType.StandardPrice ?? 0) * 1.2025m;


                #region pay for order
                string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantId"];
                string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
                string publicKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
                string privateKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PrivateKey"];
                Braintree.BraintreeGateway gateway = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);

                Braintree.TransactionRequest transaction = new Braintree.TransactionRequest();
                // transaction.Amount = 1m;
                transaction.Amount = Total;
                transaction.TaxAmount = Tax;
                transaction.OrderId = trackingNumber;
                
                //https://developers.braintreepayments.com/reference/general/testing/ruby
                transaction.CreditCard = new Braintree.TransactionCreditCardRequest
                {
                    CardholderName = "Test User",
                    CVV = "123",
                    Number = "4111111111111111",
                    ExpirationYear = DateTime.Now.AddMonths(1).Year.ToString(),
                    ExpirationMonth = DateTime.Now.AddMonths(1).ToString("MM")
                };

                var result = gateway.Transaction.Sale(transaction);
                #endregion

                #region save order
                Order o = new Order
                {
                    DateCreated = DateTime.UtcNow,
                    DateLastModified = DateTime.UtcNow,
                    TrackingNumber = trackingNumber,
                    Tax = Tax,
                    ServiceCharge = ServiceCharge,
                    Total = Total,                 
                    Email = model.ContactEmail,
                    CustomerName = model.ContactName,
                    ShippingAddress1 = model.ShippingAddress,
                    ShippingCity = model.ShippingCity,
                    ShippingPostalCode = model.ShippingPostalCode,
                    ShippingState = model.ShippingState,
                    Day = model.CurrentCart.Day,
                    HelpTypeID = model.CurrentCart.HelpTypeID,
                    LawyerID = model.CurrentCart.LawyerID
                };
                db.Orders.Add(o);
                db.SaveChanges();
                #endregion

                #region send email
                LawDoggsEmailService emailService = new LawDoggsEmailService();
                emailService.SendAsync(new Microsoft.AspNet.Identity.IdentityMessage
                {
                    Subject = "Your tracking number Information" + trackingNumber,
                    Destination = model.ContactEmail,
                    Body = "Thank you for your support"
                });
#endregion

                return RedirectToAction("Index", "Orders", new { id = trackingNumber });
            }
            return View(model);
        }

    }
}
