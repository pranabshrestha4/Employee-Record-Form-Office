﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeRecordFourm.Models
{
    public class EmployeePaginationViewModel
    {
        public List<Employee> Employees { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}