﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class JakesTestEntities1 : DbContext
    {
        public JakesTestEntities1()
            : base("name=JakesTestEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<ClassroomCourse> ClassroomCourses { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseStudent> CourseStudents { get; set; }
        public virtual DbSet<HelpType> HelpTypes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Lawyer> Lawyers { get; set; }
    }
}