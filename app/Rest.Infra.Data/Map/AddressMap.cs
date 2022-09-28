using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rest.Domain.App;
using Rest.Infra.Data.Context;

namespace Rest.Infra.Data.Map
{
    public class AddressMap
    {
        public void Configure(EntityTypeBuilder<Address> modelBuilder) 
        {
            modelBuilder.ToTable(DataBaseInformation.Address_Table);

            modelBuilder.HasKey(p => p.Id);

            modelBuilder.Property(p => p.Id)
                        .HasColumnName(DataBaseInformation.ColumnId);

            modelBuilder.Property(p => p.Street)
                        .HasColumnName(DataBaseInformation.Address_Street);

            modelBuilder.Property(p => p.Number)
                        .HasColumnName(DataBaseInformation.Address_Number);

            modelBuilder.Property(p => p.SuplementarInfo)
                        .HasColumnName(DataBaseInformation.Address_SuplementarInfo);

            modelBuilder.Property(p => p.State)
                        .HasColumnName(DataBaseInformation.Address_State);

            modelBuilder.Property(p => p.City)
                        .HasColumnName(DataBaseInformation.Address_City);

            modelBuilder.Property(p => p.ZipCode)
                        .HasColumnName(DataBaseInformation.Address_ZipCode);
        }
    }
}
