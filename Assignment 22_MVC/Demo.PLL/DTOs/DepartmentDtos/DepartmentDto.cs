using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DTOs.DepartmentDtos
{
    public class DepartmentDto
    {
        public int DeptId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        [Display(Name="Creation Date")]
        public DateOnly DateOfCreation { get; set; }
    }
}
