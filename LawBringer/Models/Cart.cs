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
    
    public partial class Cart
    {
        public System.Guid ID { get; set; }
        public string AspNetUserId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateLastModified { get; set; }
        public int HelpTypeID { get; set; }
        public System.DateTime Day { get; set; }
        public int LawyerID { get; set; }
    
        public virtual HelpType HelpType { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
