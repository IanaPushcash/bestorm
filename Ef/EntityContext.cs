using BestORM.Domain;
using Microsoft.EntityFrameworkCore;

namespace BestORM.Ef
{
    public class EntityContext : DbContext
	{
		public EntityContext()
		{

		}
		public DbSet<Customer> Customers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(Program.ConnectionString);
		}
	}

	
}
