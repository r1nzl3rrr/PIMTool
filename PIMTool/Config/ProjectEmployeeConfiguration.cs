using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMTool.Core.Domain.Entities;

namespace PIMTool.Config
{
    public class ProjectEmployeeConfiguration : IEntityTypeConfiguration<ProjectEmployee>
    {
        public void Configure(EntityTypeBuilder<ProjectEmployee> builder)
        {
            builder.Property(pe => pe.Project_Id).IsRequired().HasColumnType("numeric(19,0)");
            builder.Property(pe => pe.Employee_Id).IsRequired().HasColumnType("numeric(19,0)");

            builder.HasKey(pe => new { pe.Project_Id, pe.Employee_Id});

            builder.HasOne<Project>(pe => pe.Project)
                    .WithMany(p => p.ProjectEmployee)
                    .HasForeignKey(pe => pe.Project_Id)
                    .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne<Employee>(pe => pe.Employee)
                    .WithMany(e => e.ProjectEmployee)
                    .HasForeignKey(pe => pe.Employee_Id)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
