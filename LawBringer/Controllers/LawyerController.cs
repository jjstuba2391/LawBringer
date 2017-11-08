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
        public ActionResult List()
        {
            List <Lawyer> Lawyers = new List<Lawyer>();
            Lawyers.Add(new Lawyer
            {
                ID = 1,
                Name = "Frank",
                City = "Chicago",
                Price = 100.00m,
                Image = "/Images/Lawyer1.jpg"
            });

            Lawyers.Add(new Lawyer
            {
                ID = 2,
                Name = "Tom",
                City = "Chicago",
                Price = 80.00m,
                Image ="/Images/Lawyer2.jpg"
               
            });         
            return View(Lawyers);
        }
        // GET: Lawyers
        public ActionResult Index(int? id)
        {
            var lawyer = new Models.Lawyer();
            lawyer.AvailableHelpTypes = new string[]
            {
                "Divorce", "Traffic Violation", "Criminal"
            };

            lawyer.AvailableDays = new DateTime[10];
            for(int i = 0; i < 10; i++)
            {
                lawyer.AvailableDays[i] = DateTime.Now.AddDays(i);
            }
            if( id == 1)
            {
                lawyer.Name = "Frank";
                lawyer.City = "Chicago";
                lawyer.Price = 100.00m;
                lawyer.Image = "/Images/Lawyer1.jpg";
               
            }
            else if(id==2)
            {
                lawyer.Name = "Tom";
                lawyer.City = "Chicago";
                lawyer.Price = 80.00m;
                lawyer.Image = "/Images/Lawyer2.jpg";
            }
            else
            {
                return HttpNotFound("This Product Does not Exist!");
            }
            return View(lawyer);
        }
        [HttpPost]
        public ActionResult Index(Lawyer model)
        {
            //TODO: Save the posted information to a database!

            //TODO: Rip out this cookie code later - we're going to use it for now to mock up site functionality
            Response.AppendCookie(new HttpCookie("LawyerName", model.Name));
            Response.AppendCookie(new HttpCookie("LawyerPrice", model.Price.ToString("C")));
            Response.AppendCookie(new HttpCookie("LawyerCity", model.City));
            Response.AppendCookie(new HttpCookie("LawyerHelp", model.HelpType));
            Response.AppendCookie(new HttpCookie("LawyerTimeSlot", model.TimeSlot.ToLongDateString()));


            //TODO: build up the cart controller!
            return RedirectToAction("Index", "Cart");

        }
    }
}