using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using EmployeeInformation.Models;

namespace EmployeeInformation.DAL
{
    public class Employee_DAL
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        //Get All Employees
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetAllEmployees";
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtEmployee = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployee);
                connection.Close();

                foreach (DataRow dr in dtEmployee.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        Emp_Id = dr["Emp_Id"].ToString(),
                        Emp_Name = dr["Emp_Name"].ToString(),
                        Designation = dr["Designation"].ToString(),
                    });
                }

            }
            return employeeList;
        }

        public bool InsertEmployee(Employee employee)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("AddEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmpId", employee.Emp_Id);
                command.Parameters.AddWithValue("@EmpName", employee.Emp_Name);
                command.Parameters.AddWithValue("@Designation", employee.Designation);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public List<Employee> GetEmployeeDetailsById(string id)
        {
            List<Employee> employeeList = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetEmployeeDetails";
                command.Parameters.AddWithValue("@EmpId", id);
                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtEmployee = new DataTable();

                connection.Open();
                sqlDA.Fill(dtEmployee);
                connection.Close();

                foreach (DataRow dr in dtEmployee.Rows)
                {
                    employeeList.Add(new Employee
                    {
                        Emp_Id = dr["Emp_Id"].ToString(),
                        Emp_Name = dr["Emp_Name"].ToString(),
                        Designation = dr["Designation"].ToString(),
                    });
                }

            }
            return employeeList;
        }


        //Update Employee Details
        public bool UpdateEmployeeDetails(Employee employee)
        {
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("UpdateEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmpId", employee.Emp_Id);
                command.Parameters.AddWithValue("@EmpName", employee.Emp_Name);
                command.Parameters.AddWithValue("@Designation", employee.Designation);

                connection.Open();
                id = command.ExecuteNonQuery();
                connection.Close();
            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Delete Product
        public string DeleteEmployee(string id)
        {
            string result = "";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DeleteEmployeeById", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmpId", id);
                command.Parameters.Add("@OUTPUTMESSAGE", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;

                connection.Open();
                command.ExecuteNonQuery();
                result = command.Parameters["@OUTPUTMESSAGE"].Value.ToString();

                connection.Close();
            }
            return result;
        }
    }
}