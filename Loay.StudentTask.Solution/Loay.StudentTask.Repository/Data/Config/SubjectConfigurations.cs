using Loay.StudentTask.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loay.StudentTask.Repository.Data.Config
{
    internal class SubjectConfigurations : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
