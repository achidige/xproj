//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public abstract partial class Domain
    {
        public Domain()
        {
            this.DomainVariables = new HashSet<DomainVariable>();
            this.SpecDomains = new HashSet<SpecDomain>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Domaintype Classification { get; set; }
        public Purpose Purpose { get; set; }
    
        public virtual ICollection<DomainVariable> DomainVariables { get; set; }
        public virtual ICollection<SpecDomain> SpecDomains { get; set; }
    }
}