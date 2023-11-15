using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMTool.Core.Domain.Entities;

namespace PIMTool.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p => p.Id).IsRequired().HasColumnType("numeric(19,0)");
            builder.Property(p => p.Group_Id).IsRequired().HasColumnType("numeric(19,0)");
            builder.Property(p => p.Project_Number).IsRequired().HasColumnType("numeric(4,0)");
            builder.HasIndex(p => p.Project_Number).IsUnique();
            builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(50)");
            builder.Property(p => p.Customer).IsRequired().HasColumnType("varchar(50)");
            builder.Property(p => p.Status).IsRequired().HasColumnType("char(3)").HasAnnotation("ProjectStatusAttribute", true);
            builder.Property(p => p.Start_Date).IsRequired().HasColumnType("date");
            builder.Property(p => p.End_Date).IsRequired().HasColumnType("date");

            builder.HasOne<Group>(p => p.Group)
                    .WithMany(g => g.Projects)   
                    .HasForeignKey(p => p.Group_Id);
        }
    }
}
