using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Desafio.Loja.Domain.Entities.Customer;

namespace Desafio.Loja.Infra.Data.EF.Write.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(50)");            

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Address)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.AddressMaxLength})");
            });

            builder.OwnsOne(c => c.Phone, tf =>
            {
                tf.Property(c => c.Number)
                    .IsRequired()
                    .HasMaxLength(Phone.NumberMaxLength)
                    .HasColumnName("Phone")
                    .HasColumnType($"varchar({Phone.NumberMaxLength})");
            });

            builder.ToTable("Customers");
        }
    }
}
