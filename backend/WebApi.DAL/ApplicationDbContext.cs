using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Entities;
using WebApi.DAL.Enums;

namespace WebApi.DAL;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
		: base(options) { }

	public DbSet<User> Users { get; set; }
	public DbSet<UserToken> UserTokens { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<User>(builder =>
		{
			builder.ToTable("Users").HasKey(x => x.Id);

			builder.HasData(new User
			{
				Id = 1,
				Name = "admin",
				Password = "123",
				Role = Role.Admin,
			});

			builder.Property(x => x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.Password).IsRequired();
			builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

			builder.HasOne(x => x.UserToken)
				   .WithOne(x => x.User);
		});
	}
}
