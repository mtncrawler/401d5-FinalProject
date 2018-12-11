using JotFinalProject.Models;
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

        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ImageUploaded> ImageUploadeds { get; set; }
    }
}
