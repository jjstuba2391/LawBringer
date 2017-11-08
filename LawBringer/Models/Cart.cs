using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawBringer.Models
{
    public class Cart
    {
        public Lawyer[] Lawyers { get; set; }

        public decimal Tax { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Total { get; set; }

        public static Cart BuildCart(HttpRequestBase request)
        {
            Cart cart = new Cart();

            cart.Lawyers = new Lawyer[1];
            //For the moment, getting product data from cookies.
            //TODO: Pull this out of a database at some point!
            cart.Lawyers[0] = new Lawyer();
            cart.Lawyers[0].Name = request.Cookies["LawyerName"].Value;
            cart.Lawyers[0].Price = decimal.Parse(request.Cookies["LawyerPrice"].Value.Replace("$", ""));
            cart.Lawyers[0].City = request.Cookies["LawyerCity"].Value;

            cart.Tax = cart.Lawyers[0].Price * .1025m;
            cart.ServiceCharge = cart.Lawyers[0].Price * .10m;
            cart.Total = cart.Lawyers[0].Price + cart.Tax + cart.ServiceCharge;

            return cart;
        }
    }
}