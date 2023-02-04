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
    }
}