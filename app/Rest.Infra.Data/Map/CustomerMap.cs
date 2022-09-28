using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rest.Domain.App;
using Rest.Infra.Data.Context;

namespace Rest.Infra.Data.Map
{
    public class CustomerMap
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder) 
        {
            modelBuilder.ToTable(DataBaseInformation.Customer_Table);

            modelBuilder.HasKey(p => p.Id);

            modelBuilder.Property(p => p.Id)
                        .HasColumnName(DataBaseInformation.ColumnId);

            modelBuilder.Property(p => p.Name)
                        .HasColumnName(DataBaseInformation.Customer_Name);

            modelBuilder.Property(p => p.LastName)
                        .HasColumnName(DataBaseInformation.Customer_LastName);
            
            modelBuilder.Property(p => p.AddressId)
                        .HasColumnName(DataBaseInformation.Customer_AddressId);

            modelBuilder.Property(p => p.Birth)
                        .HasColumnName(DataBaseInformation.Customer_Birth);

            modelBuilder.HasOne<Address>()
                        .WithOne()
                        .HasForeignKey<Customer>(f => f.AddressId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
