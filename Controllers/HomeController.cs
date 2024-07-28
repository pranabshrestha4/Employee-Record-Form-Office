using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeRecordFourm.Models;

namespace EmployeeRecordFourm.Controllers
{
    public class HomeController : Controller
    {
        // EDMX Database
        EmployeeRecordFourmEntities _context = new EmployeeRecordFourmEntities();

        // Action Method for getting GET Request for Index Page
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // Action Method for handling POST Request for Index Page
        [HttpPost]
        public ActionResult Index(Employee emp)
        {
            if (ModelState.IsValid)
            {
                string uploadDirectory = Server.MapPath("~/Content/Photos");
                if (emp.PhotoFile != null && emp.PhotoFile.ContentLength > 0)
                {
                    // Save the uploaded file to the folder
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(emp.PhotoFile.FileName);
                    string filePath = Path.Combine(uploadDirectory, fileName);
                    emp.PhotoFile.SaveAs(filePath);

                    // Set the PhotoUrl property to the path of the saved file
                    emp.PhotoUrl = "/Content/Photos/" + fileName;
                }

                // Save the employee data to the database
                _context.emptables.Add(new emptable
                {
                    Name = emp.Name,
                    Address = emp.Address,
                    Contact = emp.Contact,
                    Email = emp.Email,
                    Designation = emp.Designation,
                    Salary = emp.Salary,
                    EmergencyContact = emp.EmergencyContact,
                    HireDate = emp.HireDate,
                    PhotoUrl = emp.PhotoUrl
                });

                _context.SaveChanges();
                return RedirectToAction("Confirmation");
            }
            return View(emp);
        }

        // Action Method for getting GET Request for confirmation Page
        [HttpGet]
        public ActionResult Confirmation()
        {
            return View();
        }

        // Action Method for Getting GET Request for ViewDetails Page
        [HttpGet]
        public ActionResult ViewDetails(string searchQuery, int page = 1, int pageSize = 10)
        {
            // Get a list of employees from the database
            var dbemp = _context.emptables.ToList();

            // Create a list to store the converted employees
            var modelemp = dbemp.Select(worker => new Employee
            {
                Id = worker.Id,
                Name = worker.Name,
                Address = worker.Address,
                Contact = worker.Contact,
                Email = worker.Email,
                Designation = worker.Designation,
                Salary = worker.Salary,
                EmergencyContact = worker.EmergencyContact,
                HireDate = worker.HireDate,
                PhotoUrl = worker.PhotoUrl
            }).ToList();

            // If a search query is provided, filter the results
            if (!string.IsNullOrEmpty(searchQuery))
            {
                modelemp = modelemp.Where(emp => emp.Contact.Contains(searchQuery)).ToList();
            }

            // Paginate the data
            var paginatedData = modelemp.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Create a view model to pass the paginated data and pagination information
            var viewModel = new EmployeePaginationViewModel
            {
                Employees = paginatedData,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = modelemp.Count
            };

            return View(viewModel);
        }

        // Action Method for redirecting Index page in Add employee button
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return RedirectToAction("Index");
        }

        // Action Method for getting GET Request for Edit page
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            // Find the employee by ID in the database
            var emp = _context.emptables.Find(Id);
            // Create a view model for the employee
            var model = new Employee
            {
                Id = emp.Id,
                Name = emp.Name,
                Address = emp.Address,
                Contact = emp.Contact,
                Email = emp.Email,
                Designation = emp.Designation,
                Salary = emp.Salary,
                EmergencyContact = emp.EmergencyContact,
                HireDate = emp.HireDate,
                PhotoUrl = emp.PhotoUrl

            };

            // Render the Edit view with the employee data
            return View(model);
        }


        // Action Method for handling POST Request for Edit page
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if (ModelState.IsValid)
            {
                // Find the employee by ID in the database
                var existingEmp = _context.emptables.Find(emp.Id);

                if (existingEmp != null)
                {
                    // Update the employee's information with the new data
                    existingEmp.Name = emp.Name;
                    existingEmp.Address = emp.Address;
                    existingEmp.Contact = emp.Contact;
                    existingEmp.Email = emp.Email;
                    existingEmp.Designation = emp.Designation;
                    existingEmp.Salary = emp.Salary;
                    existingEmp.EmergencyContact = emp.EmergencyContact;
                    existingEmp.HireDate = emp.HireDate;

                    // Check if a new photo file is uploaded
                    if (emp.PhotoFile != null && emp.PhotoFile.ContentLength > 0)
                    {
                        string uploadDirectory = Server.MapPath("~/Content/Photos");
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(emp.PhotoFile.FileName);
                        string filePath = Path.Combine(uploadDirectory, fileName);
                        emp.PhotoFile.SaveAs(filePath);
                        existingEmp.PhotoUrl = "/Content/Photos/" + fileName;
                    }

                    // Save the changes to the database
                    _context.SaveChanges();
                    return RedirectToAction("Update");
                }
                else
                {
                    // Employee not found, return an error or redirect to a different page
                    ModelState.AddModelError("", "Employee not found.");
                }
            }

            // If the model state is not valid, return to the Edit view with errors
            return View(emp);
        }

        // Action Method for getting GET Request to redirect to update page
        [HttpGet]
        public ActionResult Update()
        {
            return View();
        }

        // Action Method for getting GET Request for Delete page
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            // Find the employee by ID in the database
            var emp = _context.emptables.Find(Id);
            // Create a view model for the employee
            var model = new Employee
            {
                Id = emp.Id,
                Name = emp.Name,
                Address = emp.Address,
                Contact = emp.Contact,
                Email = emp.Email,
                Designation = emp.Designation,
                Salary = emp.Salary,
                EmergencyContact = emp.EmergencyContact,
                HireDate = emp.HireDate,
                PhotoUrl = emp.PhotoUrl
            };
            return View(model);
        }

        // Action Method for handling POST Request to delete an employee
        [HttpPost]
        public ActionResult DeleteConfirmed(int Id)
        {
            // Find the employee by ID in the database
            var emp = _context.emptables.Find(Id);
            // Remove the employee from the database
            _context.emptables.Remove(emp);
            _context.SaveChanges();
            return RedirectToAction("ViewDetails");
        }
    }
}
