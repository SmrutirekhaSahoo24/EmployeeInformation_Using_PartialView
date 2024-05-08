using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeInformation.DAL;
using EmployeeInformation.Models;

namespace EmployeeInformation.Controllers
{
    public class EmployeeController : Controller
    {
        Employee_DAL _employeeDAL = new Employee_DAL();

        // GET: Employee
        public ActionResult Index()
        {
            var employeeList = _employeeDAL.GetAllEmployees();
            if (employeeList.Count == 0)
            {
                TempData["InfoMessage"] = "Currently no Employees are available in the DataBase...!";
            }
            return View(employeeList);
        }

        // GET: Employee/Details/5
        /*public ActionResult Details(string id)
        {
            try
            {
                var employeeDetail = _employeeDAL.GetEmployeeDetailsById(id).FirstOrDefault();
                if (employeeDetail == null)
                {
                    TempData["ErrorMessage"] = "Employee not availavle with id " + id;
                    return RedirectToAction("Index");
                }
                return View(employeeDetail);
            }


            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        } */

        // GET: Employee/Create
        // GET: Employee/Create
        public ActionResult Create(string id)
        {
            Employee employeeDetail = null;
            if (id != null)
            {
                employeeDetail = _employeeDAL.GetEmployeeDetailsById(id).FirstOrDefault();
                if (employeeDetail == null)
                {
                    TempData["ErrorMessage"] = "Employee not found with ID: " + id;
                    return RedirectToAction("Index");
                }
            }
            else
            {
                employeeDetail = new Employee();
            }
            return PartialView("Create", employeeDetail);
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrEmpty(employee.Emp_Id))
                    {
                        // If Emp_Id is empty, it's a new employee
                        bool isInserted = _employeeDAL.InsertEmployee(employee);
                        TempData["SuccessMessage"] = isInserted ? "Employee details saved successfully." : "Failed to save employee details.";
                    }
                    else
                    {
                        // If Emp_Id is not empty, it's an existing employee being edited
                        bool isUpdated = _employeeDAL.UpdateEmployeeDetails(employee);
                        TempData["SuccessMessage"] = isUpdated ? "Employee details updated successfully." : "Failed to update employee details.";
                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred: " + ex.Message;
            }
            return View(employee);
        }


        // GET: Employee/Edit/5
        /*public ActionResult Edit(string id)
        {
            try
            {
                var employeeDetail = _employeeDAL.GetEmployeeDetailsById(id).FirstOrDefault();
                if (employeeDetail == null)
                {
                    TempData["ErrorMessage"] = "Employee not availavle with id " + id;
                    return RedirectToAction("Index");
                }
                return View(employeeDetail);
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

        }

        // POST: Employee/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateEmployee(Employee employee)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _employeeDAL.UpdateEmployeeDetails(employee);
                    if (IsUpdated)
                    {
                        TempData["SuccessMessage"] = "Employee details updated successfully...!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to update the employee details...!";
                    }
                }
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();

            }
        }*/

        // GET: Employee/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                var employeeDetail = _employeeDAL.GetEmployeeDetailsById(id).FirstOrDefault();
                if (employeeDetail == null)
                {
                    TempData["ErrorMessage"] = "Employee not availavle with id " + id;
                    return RedirectToAction("Index");
                }
                return View(employeeDetail);
            }


            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(string id)
        {
            try
            {
                // TODO: Add delete logic here
                string result = _employeeDAL.DeleteEmployee(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}