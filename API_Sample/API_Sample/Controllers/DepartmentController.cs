using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using API_Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace API_Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        
        [HttpGet]
        
        public JsonResult Get()
        {
            string query = @"select DepartmentId , DepartmentName from Department";

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
        public JsonResult Post(Department dep)
        {
            string query = @"Insert into Department values (@DepartmentName)";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesAppcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"update Department 
                             set DepartmentName = @DepartmentName
                             where DepartmentId = @DepartmentId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesAppcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    mycmd.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
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
            string query = @"Delete from Department
                             where DepartmentId = @DepartmentId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeesAppcon");
            SqlDataReader myReader;
            using (SqlConnection mycon = new SqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (SqlCommand mycmd = new SqlCommand(query, mycon))
                {
                    mycmd.Parameters.AddWithValue("@DepartmentId", Id);
                    myReader = mycmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Delete Successfully");

        }
    }

}


  
