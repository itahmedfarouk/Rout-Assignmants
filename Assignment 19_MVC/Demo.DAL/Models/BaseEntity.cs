using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; } // User Id
        public DateTime CreatedOn { get; set; } // Insertion Date
        public int LastModifiedBy { get; set; }// User Id
        public DateTime LastModifiedOn  { get; set; } //Update Date
        public bool IsDeleted { get; set; } //To Apply Soft Delete

    }
}
