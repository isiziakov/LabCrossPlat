using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab.Model
{
    public class Context : DbContext
    {
        private string _databasePath;

        public DbSet<EFlat> Flats { get; set; }
        public DbSet<District> Districts { get; set; }

        public Context(string databasePath)
        {
            _databasePath = databasePath;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EFlat>()
                .HasKey(e => e.flat_id);
        }
        }
    }