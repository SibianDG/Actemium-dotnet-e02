using _2021_dotnet_e_02.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021_dotnet_e_02.Data.Mapping
{
    public class KbItemConfiguration : IEntityTypeConfiguration<KbItem>
    {
        public void Configure(EntityTypeBuilder<KbItem> builder)
        {
            builder.ToTable("ACTEMIUMKBITEM");

            builder.HasKey(t => t.KBITEMID);
            builder.Property(t => t.TITLE);
            builder.Property(t => t.TYPE);
            builder.Property(t => t.KEYWORDS);
            builder.Property(t => t.TEXT);
        }
    }
}
