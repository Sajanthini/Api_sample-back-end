using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using API_Sample.Models;

namespace API_Sample.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController :ControllerBase
    {
        
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        //private readonly object Request;

        public EmployeesController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select EmployeeId , EmployeeName , Department, convert (varchar(10),DateofJoining,120)
            as DateofJoining,PhotoFileName from Employee";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesAppcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult(table);

        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            string query = @"Insert into Employee
            (EmployeeName,Department,DateofJoining,PhotoFileName)
            values (@EmployeeName,@Department,@DateofJoining,@PhotoFileName)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesAppcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    mycmd.Parameters.AddWithValue("@Department", emp.Department);
                    mycmd.Parameters.AddWithValue("@DateofJoining", emp.DateofJoining);
                    mycmd.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            string query = @"update Department 
                             set EmployeeName = @EmployeeName
                             Department = @Department
                             DateofJoining = @DateofJoining
                             PhotoFileName = @ PhotoFileName
                             where EmployeeId = @EmployeeId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesAppcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    mycmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    mycmd.Parameters.AddWithValue("@Department", emp.Department);
                    mycmd.Parameters.AddWithValue("@DateofJoining", emp.DateofJoining);
                    mycmd.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");

        }

        [HttpDelete("{Id}")]
        public JsonResult Delete(int Id)
        {
            string query = @"Delete from Employee
                             where EmployeeId = @EmployeeId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesAppcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@EmployeeID", Id);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Delete Successfully");

        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("abc.png");
            }





        }

    }

}


