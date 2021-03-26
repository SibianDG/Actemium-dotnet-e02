using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class KbItemConfiguration : IEntityTypeConfiguration<ActemiumKbItem>
    {
        public void Configure(EntityTypeBuilder<ActemiumKbItem> builder)
        {
            builder.ToTable("ACTEMIUMKBITEM");

            builder.HasKey(t => t.KbItemId);
            builder.Property(t => t.Title);
            builder.Property(t => t.Type);
            builder.Property(t => t.Keywords);
            builder.Property(t => t.Text);
        }
    }
}
