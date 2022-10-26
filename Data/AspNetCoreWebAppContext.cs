using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNetCoreWebApp.Models;

namespace AspNetCoreWebApp.Data
{
    public class AspNetCoreWebAppContext : DbContext
    {
        public AspNetCoreWebAppContext (DbContextOptions<AspNetCoreWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<AspNetCoreWebApp.Models.Movie> Movie { get; set; } = default!;
    }
}
