using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configurations
{
    public class ReaderConfiguration : IEntityTypeConfiguration<Reader>
    {
        public void Configure(EntityTypeBuilder<Reader> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.Email)
                .HasMaxLength(100);

            builder.Property(r => r.PhoneNumber)
                .HasMaxLength(20);

            builder.HasData(
                new Reader
                {
                    Id = 101,
                    FirstName = "Ivan",
                    LastName = "Petrov",
                    Email = "ivan.petrov@example.com",
                    PhoneNumber = "+359888123456",
                    RegistrationDate = new DateTime(2024, 1, 15)
                },
                new Reader
                {
                    Id = 102,
                    FirstName = "Maria",
                    LastName = "Ivanova",
                    Email = "maria.ivanova@example.com",
                    PhoneNumber = "+359888987654",
                    RegistrationDate = new DateTime(2024, 3, 20)
                },
                new Reader
                {
                    Id = 103,
                    FirstName = "Georgi",
                    LastName = "Dimitrov",
                    Email = "georgi.dimitrov@example.com",
                    PhoneNumber = "+359887111222",
                    RegistrationDate = new DateTime(2024, 6, 10)
                }
            );
        }
    }
}
