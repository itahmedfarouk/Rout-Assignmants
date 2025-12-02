using Demo.DAL.Models.Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
	public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
	{
		public void Configure(EntityTypeBuilder<T> builder)
		{

			builder.Property(d => d.CreatedOn).HasDefaultValueSql("GETDATE()"); // Set default value to current date and time on creation
			builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GETDATE()"); // Set default value to current date and time on modification
		}
	}
}
