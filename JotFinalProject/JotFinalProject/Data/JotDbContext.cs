using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JotFinalProject.Data
{
    public class JotDbContext : DbContext
    {
        public JotDbContext(DbContextOptions<JotDbContext> options) : base(options)
        {

        }
    }
}
