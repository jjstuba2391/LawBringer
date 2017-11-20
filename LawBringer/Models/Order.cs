//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LawBringer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }
        public string Email { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress1 { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingPostalCode { get; set; }
        public decimal Total { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal Tax { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateLastModified { get; set; }
        public int HelpTypeID { get; set; }
        public System.DateTime Day { get; set; }
        public int LawyerID { get; set; }
    
        public virtual HelpType HelpType { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
