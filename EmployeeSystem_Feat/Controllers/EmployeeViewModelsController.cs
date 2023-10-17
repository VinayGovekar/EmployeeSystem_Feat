using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeSystem_Feat.Data;
using EmployeeSystem_Feat.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EmployeeSystem_Feat.Controllers
{
    public class EmployeeViewModelsController : Controller
    {


        private readonly IConfiguration _configuration;
        public EmployeeViewModelsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET: EmployeeViewModels
        public IActionResult Index()
        {
            DataTable dtl = new DataTable();
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("TestConnection")))
            {
                sql.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("GetAllEmployees", sql);
                sqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlDa.Fill(dtl);
            }
            return View(dtl);
        }

        // GET: EmployeeViewModels/AddOrEdit/
        public IActionResult AddOrEdit(int? id)
        {
            var EmployeeViewModel = new EmployeeViewModel();
            if (id > 0)
                EmployeeViewModel = FetchEmployeeByID(id);
            return View(EmployeeViewModel);
        }

        // POST: EmployeeViewModels/AddOrEdit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEdit(int id, [Bind("EmpId,EmpName,Salary,Manager")] EmployeeViewModel employeeViewModel)
        {
       
            if (ModelState.IsValid)
            {
                using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("TestConnection")))
                {
                    sql.Open();
                    SqlCommand sqlCmd = new SqlCommand("EmployeeAddOREdit",sql);
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("EmpId",employeeViewModel.EmpId);
                    sqlCmd.Parameters.AddWithValue("EmpName", employeeViewModel.EmpName);
                    sqlCmd.Parameters.AddWithValue("Salary", employeeViewModel.Salary);
                    sqlCmd.Parameters.AddWithValue("Manager", employeeViewModel.Manager);
                    sqlCmd.ExecuteNonQuery();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employeeViewModel);
        }

        // GET: EmployeeViewModels/Delete/5
        public IActionResult Delete(int? id)
        {
            var employee = FetchEmployeeByID(id);
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("TestConnection")))
            {
                sql.Open();
                SqlCommand sqlCmd = new SqlCommand("DeleteEmployeeByID", sql);
                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("EmpId", id);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }

        public EmployeeViewModel FetchEmployeeByID(int? Id)
        {
            var employeeViewModel = new EmployeeViewModel();

            DataTable dtl = new DataTable();
            using (SqlConnection sql = new SqlConnection(_configuration.GetConnectionString("TestConnection")))
            {
                sql.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("GetEmployeeByID", sql);
                sqlDa.SelectCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("EmpId", Id);
                sqlDa.Fill(dtl);
                if(dtl.Rows.Count==1)
                {
                    employeeViewModel.EmpId = Convert.ToInt32(dtl.Rows[0]["EmpId"].ToString());

                    employeeViewModel.EmpName = dtl.Rows[0]["EmpName"].ToString();

                    employeeViewModel.Salary = Convert.ToInt32(dtl.Rows[0]["Salary"].ToString());

                    employeeViewModel.Manager = Convert.ToInt32(dtl.Rows[0]["Manager"].ToString());
                }
                return employeeViewModel;
            }

        }
    }
}
