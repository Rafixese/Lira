using Microsoft.EntityFrameworkCore;

namespace Lira.Data;

public class AuthDbContext:DbContext
{
    public DbSet<AppUser> AppUsers { get; set; }
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId);
            entity.Property(e => e.Username).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(250);
            entity.Property(e => e.Admin);

            entity.HasData(new AppUser
            {
                UserId = 1,
                Email = "rafixese@gmail.com",
                Username = "Rafixese",
                Password = "admin",
                Admin = true
            });

        });
    }
}