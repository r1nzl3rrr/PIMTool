using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PIMTool.Core.Domain.Entities;
using System.Reflection.Emit;

namespace PIMTool.Config
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Id).IsRequired().HasColumnType("numeric(19,0)");
            builder.Property(e => e.Visa).HasColumnType("char(3)");
            builder.Property(e => e.First_Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Last_Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(e => e.Birth_Date).IsRequired().HasColumnType("date");                    
        }
    }
}