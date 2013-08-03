﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SpecToolModelContext : DbContext
    {
        public SpecToolModelContext()
            : base("name=SpecToolModelContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<MDR> MDRs { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Domain> Domains { get; set; }
        public DbSet<Variable> Variables { get; set; }
        public DbSet<Study> Studies { get; set; }
        public DbSet<DomainVariable> DomainVariables { get; set; }
        public DbSet<CodeList> CodeLists { get; set; }
        public DbSet<CodeListValues> CodeListValues { get; set; }
        public DbSet<SpecDomain> SpecDomains { get; set; }
        public DbSet<SpecDomainVariable> SpecDomainVariables { get; set; }
        public DbSet<SpecCodeListValues> SpecCodeListValues { get; set; }
    }
}