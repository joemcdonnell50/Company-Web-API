using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Company_COREMvc.Models
{
    public class Company_COREMvcContext : DbContext
    {
        public Company_COREMvcContext (DbContextOptions<Company_COREMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Company_COREMvc.Models.Company> Company { get; set; }
    }
}
