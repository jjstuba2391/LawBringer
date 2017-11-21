using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace LawBringer.Controllers
{
    public class AccountController : Controller
    {     
        // GET: Account
        [Authorize]
        public ActionResult Index()
        {
           
            string merchantID = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantID"];
            string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
            string publickey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privatekey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PriavteKey"];
            Braintree.BraintreeGateway gateway = new Braintree.BraintreeGateway(environment, merchantID, publickey, privatekey);

            var customerGateway = gateway.Customer;
            Braintree.CustomerSearchRequest query = new Braintree.CustomerSearchRequest();
            query.Email.Is(User.Identity.Name);
            var matchedCustomers = customerGateway.Search(query);
            Braintree.Customer customer = null;
            if (matchedCustomers.Ids.Count == 0)
            {
                Braintree.CustomerRequest newCustomer = new Braintree.CustomerRequest();
                newCustomer.Email = User.Identity.Name;

                var result = customerGateway.Create(newCustomer);
                customer = result.Target;
            }
            else
            {
                customer = matchedCustomers.FirstItem;
            }
            return View(customer);        
        }
        [HttpPost]
        public ActionResult Index(string firstName, string lastName, string id)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Index", "Home");
            }
            string merchantId = System.Configuration.ConfigurationManager.AppSettings["Braintree.MerchantId"];
            string environment = System.Configuration.ConfigurationManager.AppSettings["Braintree.Environment"];
            string publicKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PublicKey"];
            string privateKey = System.Configuration.ConfigurationManager.AppSettings["Braintree.PrivateKey"];
            Braintree.BraintreeGateway gateway = new Braintree.BraintreeGateway(environment, merchantId, publicKey, privateKey);

            var customerGateway = gateway.Customer;
            Braintree.CustomerRequest request = new Braintree.CustomerRequest();
            request.FirstName = firstName;
            request.LastName = lastName;
            var result = customerGateway.Update(id, request);
            ViewBag.Message = "Updated Successfully";
            return View(result.Target);
        }

       
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            IdentityUser user = new IdentityUser { Email = username, UserName = username };

            IdentityResult result = userManager.Create(user, password);
            if (result.Succeeded)
            {
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                }, userIdentity);

                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = result.Errors;
            return View();
        }

        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(string username, string password, bool? staySignedIn)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByName(username);
            if (user != null)
            {
                bool isPasswordValid = userManager.CheckPassword(user, password);
                if (isPasswordValid)
                {
                    var claimsIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties
                    {
                        IsPersistent = staySignedIn ?? false,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    }, claimsIdentity);
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.error = new string[] { "Unable to Sign in" };
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string email)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByEmail(email);
            if(user != null)
            {
                string resetToken = userManager.GeneratePasswordResetToken(user.Id);
                string resetUrl = Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ResetPassword?email=" + email + "&token=" + resetToken;
                string message = string.Format("<a href=\"{0}\">Reset Your Password</a>", resetUrl);
                userManager.SendEmail(user.Id, "Your password reset token", message);
            }
            return RedirectToAction("forgotPasswordSent");
        }
        public ActionResult ForgotPasswordSent()
        {
            return View();
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(string email, string token, string newPassword)
        {

            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<IdentityUser>>();
            var user = userManager.FindByEmail(email);
            if(user != null)
            {
              IdentityResult result = userManager.ResetPassword(user.Id, token, newPassword);
                if(result.Succeeded)
                {
                    TempData["Message"] = "Your Password has been updated";
                    return RedirectToAction("SignIn");
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }    
}