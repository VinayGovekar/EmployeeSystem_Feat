using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeSystem_Feat.Models;

namespace EmployeeSystem_Feat.Data
{
    public class EmployeeSystem_FeatContext : DbContext
    {
        public EmployeeSystem_FeatContext (DbContextOptions<EmployeeSystem_FeatContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeSystem_Feat.Models.EmployeeViewModel> EmployeeViewModel { get; set; } = default!;
    }
}
