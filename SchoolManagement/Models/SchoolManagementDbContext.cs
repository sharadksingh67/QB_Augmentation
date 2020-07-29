using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SchoolManagement.Models
{
    public class SchoolManagementDbContext:DbContext
    {
        public DbSet<school> schools { get; set; }
        public SchoolManagementDbContext():base("SchoolManagement")
        {

        }
    }
}