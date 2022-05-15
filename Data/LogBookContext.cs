using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogBook.Models;

namespace LogBook.Data
{
    public class LogBookContext : DbContext
    {
        public LogBookContext (DbContextOptions<LogBookContext> options)
            : base(options)
        {
        }

        public DbSet<LogBook.Models.Log>? Log { get; set; }
    }
}
