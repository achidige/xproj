using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
namespace SampleMVC4.Models
{
    public class CDISCModelContext : DbContext
    {

        public CDISCModelContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Variable> Variables { get; set; }

        public DbSet<Component> Components { get; set; }

        public DbSet<Domain> Domains { get; set; }

        public DbSet<Study> Studies { get; set; }

           
    }

    
    [Table("Component")]
    public class Component
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    [Table("Study")]
    public class Study
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public Component Component { get; set; }
    }

    [Table("Domain")]    
    public class Domain
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Variable> Variables { get; set; }
    }

    [Table("Variable")]        
    public class Variable
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
 
    }


}