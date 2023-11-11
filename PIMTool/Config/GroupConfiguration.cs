using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PIMTool.Core.Domain.Entities;

namespace PIMTool.Config
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(g => g.Id).IsRequired().HasColumnType("numeric(19,0)");
            builder.Property(g => g.Group_Leader_Id).HasColumnType("numeric(19,0)");
        }
    }
}
