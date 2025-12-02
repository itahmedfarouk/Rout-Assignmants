using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data.Models
{

    #region // 1) Mapping By Convention (Default Behavior )
    //internal class Employee
    //{
    //    public int Id { get; set; } // Set Id As A Primary Key By Default [PubliC Numeric Property NNamed As Id Or ClassNameId(EmployeeId)]
    //    public string? Name { get; set; } // Nullable Refernce Type : Allow Null (Optional) -> Nvarchar(max)
    //    public double Salary { get; set; } // Required : Non - Nullable Value Type
    //    public int? Age { get; set; } // Nullable Value Type 
    //} 
    #endregion

    //===================================================================================================//


    #region // 2) Data Annotation { Attributes }
    //[Table("Employees",Schema ="Dbo")]
    //internal class Employee // Domain Model / POCO Class 
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }

    //    [Column(TypeName = "varchar")]
    //    [StringLength(50, MinimumLength = 10)]// Mapped IN DB
    //    [MaxLength(50)]// Mapped IN DB
    //    [MinLength(10)]// Not Mapped But Application Validation
    //    [Length(10,50)]// Not Mapped But Application Validation
    //    [Required(ErrorMessage ="Name Is Required ")]
    //    public string? Name { get; set; }

    //    [Column(TypeName ="decimal(12,2)")]
    //    public double Salary { get; set; }

    //    [Range(22,60)]
    //    [AllowedValues(20,30,40)]// Not Mapped But Application Validation
    //    [DeniedValues(12,13,14)]// Not Mapped But Application Validation
    //    public int? Age { get; set; }

    //    [EmailAddress] // Not Mapped But Application Validation
    //    [DataType(DataType.EmailAddress)] // Not Mapped But Application Validation
    //    public string? Email { get; set; }

    //    [Phone]// Not Mapped But Application Validation
    //    [DataType(DataType.PhoneNumber)]// Not Mapped But Application Validation
    //    public string? PhoneNumber { get; set; }

    //    [DataType(DataType.Password)]// Not Mapped But Application Validation
    //    [RegularExpression("")]
    //    public string? Password { get; set; }

    //    [NotMapped] // Because is A Derived Attribute 
    //    public double NetSalary => Salary - (Salary * 0.2); // Derived Attribute[Lambda Expression] => For Read Only
    //} 
    #endregion


    //===================================================================================================//

    #region //3) Fluent APIs  => DbContext {OnModelCreating()}
    //internal class Employee // Domain Model
    //{
    //    public int Id { get; set; } //
    //    public string? Name { get; set; } //
    //    public double Salary { get; set; } //
    //    public int? Age { get; set; } //



    //}
    #endregion

    #region Configration Classes => Enhanced Fluent APIs
    public class Employee // Domain Model
    {
        public int Id { get; set; } 
        public string? Name { get; set; } 
        public double Salary { get; set; } 
        public int? Age { get; set; }

        //public string Address { get; set; } // Shadow Property


        #region Work Relationship (1 To Many => Take The PK Of One As FK In Many)

        //////public Department WorkDepartment { get; set; }
        //// Same as
        //////public Department WorkDepartment { get; set; } = null!;
        ///// Same as
        //[Forgin Key By Naming Conventions]
        public int? DepartmentId { get; set; } // Detected By Default As A FK [Type_OfNavigationalProperty + ID]

        //public int WorkDepartmentId { get; set; } // Detected By Default As A FK [Name_OfNavigationalProperty + ID]
        //public required Department WorkDepartment { get; set; } = null!;


        //public int DepartmentDepartmentId { get; set; } // Detected By Default As A FK [Type_OfNavigationalProperty + PKName In Department Table ]
        //public int WorkDepartmentDepartmentId { get; set; } // Detected By Default As A FK [Name_OfNavigationalProperty + PKName In Department Table ]
        public virtual Department? WorkDepartment { get; set; } = null!;



        ////public Department? WorkDepartment { get; set; }


        #endregion


        #region Manage Relationship (1(optional) to 1(mandatory) => take the pk of optional as fk in mandatory)

        [InverseProperty(nameof(Department.Manger))]
        public virtual Department? DepartmentToManage { get; set; }


        #endregion


        #region 1 to 1 Mandatory From Bith Sides
        public Address EmpAddress { get; set; } = null!;


        #endregion
        #endregion
    }
}
