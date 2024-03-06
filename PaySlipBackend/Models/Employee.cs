using System.ComponentModel.DataAnnotations;

namespace PaySlipBackend.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        public string  EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string DOJ { get; set; }
        public string PAN { get; set; }

    }

    public class EmployeeDTO
    {
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string DOJ { get; set; }
        public string PAN { get; set; }

    }


}
