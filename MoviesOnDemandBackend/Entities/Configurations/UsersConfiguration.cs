using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoviesOnDemandBackend.Entities.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder
            .Property(u => u.Id)
            .IsRequired();
        
        builder
            .Property(u => u.Email)
            .HasMaxLength(60)
            .IsRequired();

        builder
            .Property(u => u.Username)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(u => u.PasswordHash)
            .IsRequired();
        
        builder
            .Property(u => u.PasswordSalt)
            .IsRequired();
        
        builder
            .Property(u => u.Role)
            .HasDefaultValue("user")
            .IsRequired();

        builder
            .Property(u => u.AccountCreated)
            .HasColumnType("Date")
            .IsRequired();

        HashPassword("admin123", out byte[] hash, out byte[] salt);
        
        builder
            .HasData(
                new User
                {
                    Id = 1,
                    Email = "admin@mod.com",
                    Username = "admin",
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Role = "admin",
                    AccountCreated = DateTime.Today
                }
            );
    }

    private void HashPassword(string password, out byte[] hash, out byte[] salt)
    {
        using (var hmac = new HMACSHA512())
        {
            salt = hmac.Key;
            hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}