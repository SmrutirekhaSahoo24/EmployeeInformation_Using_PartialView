using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeInformation.Models
{
    public class Employee
    {
        [Key]
        [DisplayName("Employee ID")]
        public string Emp_Id { get; set; }

        [Required]
        [DisplayName("Employee Name")]
        public string Emp_Name { get; set; }

        [Required]
        public string Designation { get; set; }
    }
}