using Microsoft.EntityFrameworkCore;

namespace satchel
{
	public class ShoppingListContext : DbContext
	{
		public DbSet<ShoppingListItem> ShoppingListItems {get;set;}

		public ShoppingListContext(DbContextOptions<ShoppingListContext> options): base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<ShoppingListItem>().ToTable("ShoppingListItems");
		}


	}
}