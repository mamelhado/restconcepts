using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rest.Domain.App;
using Rest.Infra.Data.Context;

namespace Rest.Infra.Data.Map
{
    public class UserMap
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder) 
        {
            modelBuilder.ToTable(DataBaseInformation.User_Table);

            modelBuilder.HasKey(p => p.Id);

            modelBuilder.Property(p => p.Id)
                        .HasColumnName(DataBaseInformation.ColumnId);

            modelBuilder.Property(p => p.FirstName)
                        .HasColumnName(DataBaseInformation.User_FirstName);

            modelBuilder.Property(p => p.LastName)
                        .HasColumnName(DataBaseInformation.User_LastName);
            
            modelBuilder.Property(p => p.Username)
                        .HasColumnName(DataBaseInformation.User_Username);

            modelBuilder.Property(p => p.Role)
                        .HasColumnName(DataBaseInformation.User_Role);

            modelBuilder.Property(p => p.PaswordHash)
                .HasColumnName(DataBaseInformation.Pass_Hash)
                .HasConversion(p => BCrypt.Net.BCrypt.HashPassword(p)
                , p => p.ToString());
        }
    }
}
