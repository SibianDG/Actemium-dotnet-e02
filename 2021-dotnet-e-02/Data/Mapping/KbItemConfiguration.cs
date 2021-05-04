using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2021_dotnet_e_02.Models.Enums;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class KbItemConfiguration : IEntityTypeConfiguration<ActemiumKbItem>
    {
        public void Configure(EntityTypeBuilder<ActemiumKbItem> builder)
        {
            builder.ToTable("ACTEMIUMKBITEM");

            builder.HasKey(t => t.KbItemId);
            builder.Property(t => t.KbItemId).HasColumnName("KBITEMID");
            
            builder.Property(t => t.Title).HasColumnName("TITLE");
            builder.Property(t => t.Type).HasColumnName("TYPE")
                .HasConversion(v => v.ToString().ToUpper(),
                    v => (KbItemType)Enum.Parse(typeof(KbItemType), v));
            builder.Property(t => t.Keywords).HasColumnName("KEYWORDS");
            builder.Property(t => t.Text).HasColumnName("TEXT");
        }
    }
}
