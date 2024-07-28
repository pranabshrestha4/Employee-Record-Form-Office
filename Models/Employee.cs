using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeRecordFourm.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public decimal Salary { get; set; }
        public string EmergencyContact { get; set; }
        public DateTime HireDate { get; set; }
        public string PhotoUrl { get; set; }
        // Add a property for file upload
        [NotMapped] // This attribute tells Entity Framework not to map this property to the database.
        public HttpPostedFileBase PhotoFile { get; set; }
    }
}