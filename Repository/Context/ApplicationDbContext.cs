using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Context
{
	public class ApplicationDbContext : DbContext
	{
		public virtual DbSet<Persons> Persons { get; set; }
		public virtual DbSet<Employees> Employees { get; set; }
		public virtual DbSet<Assets> Assets { get; set; }
        public virtual DbSet<EmployeesHasAssets> EmployeesHasAssets { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

