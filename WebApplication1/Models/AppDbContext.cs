﻿using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Students> Student { get; set; } 

    }
}
