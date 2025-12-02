using Assignment01_Ef_Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment01_Ef_Core.Configurations
{
    public class CourseInstConfig : IEntityTypeConfiguration<Course_Inst>
    {
        public void Configure(EntityTypeBuilder<Course_Inst> entity)
        {
            entity.ToTable("Course_Inst");
            entity.HasKey(e => new { e.inst_ID, e.Course_ID });
        }
    }
}
