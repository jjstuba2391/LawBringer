using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LawBringer.Controllers
{
    public class ReceiptController : Controller
    {
        // GET: Receipt
        public ActionResult Index()
        {
            var receipt = new Models.Receipt();

            return View(receipt);
        }
        [HttpPost]
        public ActionResult Index(Models.Receipt model)
        {
            

            return View(model);
        }
    }
}