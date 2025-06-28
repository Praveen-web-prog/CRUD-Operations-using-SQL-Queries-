using DumpApplication.WebApi.Data;
using DumpApplication.WebApi.Models.Entities;
using DumpApplication.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace DumpApplication.WebApi.Controllers
{

    [Route("api/employees")]
    [ApiController] 
    public class EmployeesController : ControllerBase 
    {

 // ApplicationDbContext oda object ah namma constructor ku pass pannudhula , andha velaya paakradhu yaar nu paatha 
 // DI -> dependency injection !!! 
        private readonly ApplicationDbContext dbContext;

        private readonly string DB_Connection = "Server=localhost\\MSSQLSERVER01;Database=EmployeesDb;Trusted_connection=true;TrustServerCertificate=true";
        public EmployeesController(ApplicationDbContext dbContext)
        {

            this.dbContext = dbContext;
        }



        [HttpGet]
        public IActionResult GetEmployee() {
            return Ok(dbContext.EmployeeDatas.ToList());
        }

        [HttpPost]
        public IActionResult PostEmployee([FromBody] AddEmployeeDto addEmployeeDto)
        {
            Guid guidId = Guid.NewGuid(); // idhu vandhu pudhu pudhu Guid ah produce pannite irrukum 
        // ippo inga -> namma DB la table create pannumbodhu (primary key AutoIncrement nu potukalam)
        // appdi potuta inga (Guid guidId = Guid.NewGuid();) pannanu nu thevai illa

            using (SqlConnection sql_DB = new SqlConnection(DB_Connection)) 
            {

                sql_DB.Open();
                var sql_String = @"
              INSERT INTO EmployeeDatas
              (Id , Name , Email , Phone , PhoneNumber)
              VALUES(@Id , @Name , @Email , @Phone , @PhoneNumber)
             ";

                using (SqlCommand insert = new SqlCommand(sql_String,sql_DB))
                {

                    insert.Parameters.AddWithValue("@Id", guidId);
                    insert.Parameters.AddWithValue("@Name", addEmployeeDto.Name);
                    insert.Parameters.AddWithValue("@Email", addEmployeeDto.Email);
                    insert.Parameters.AddWithValue("@Phone", addEmployeeDto.Phone);
                    insert.Parameters.AddWithValue("@PhoneNumber", addEmployeeDto.PhoneNumber);


                    insert.ExecuteNonQuery(); // ivan than edho kuruma pandran 
                    // DB la data store pandradhuku 

                }
            }

            return Ok("Inserted Successfully !!");
        }



        // this is the get response is to get the information about single employee
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployer1(Guid id) {

            List<AddEmployeeDto> A = new List<AddEmployeeDto>();

            using (SqlConnection sqlConnection = new SqlConnection(DB_Connection))
            {

                sqlConnection.Open();


                var sql_String = @"
                 SELECT * FROM EmployeeDatas
                 WHERE Id = @id
                 ";
             
                using (SqlCommand cmd = new SqlCommand(sql_String, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader read = cmd.ExecuteReader();
                    // inga vandhu execute reader nu oruthan andha kuruma va pandran !!

                    while (read.Read())
                    {
                        A.Add(new AddEmployeeDto{ 
                            Name = read.GetString(1), 
                            Email = read.GetString(2), 
                            Phone = read.GetString(3), 
                            PhoneNumber = read.GetDecimal(4) 
                        }); 
                    }

                }

            }
         
            return Ok(A);
        }






        // http update -> by using this we can update our object !
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id , UpdateEmployeeDto updateEmployee)
        {
            
                using (SqlConnection sql = new SqlConnection(DB_Connection))
                {
                    sql.Open();

                    string sql_String = @"
                 UPDATE EmployeeDatas
                 SET Name = @Name , Email = @Email , Phone = @Phone , PhoneNumber = @PhoneNumber
                 WHERE Id = @id
                 
                ";

                    using (SqlCommand cmd = new SqlCommand(sql_String, sql))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@Name", updateEmployee.Name);
                        cmd.Parameters.AddWithValue("@Email", updateEmployee.Email);
                        cmd.Parameters.AddWithValue("@Phone", updateEmployee.Phone);
                        cmd.Parameters.AddWithValue("@PhoneNumber", updateEmployee.PhoneNumber);
                        


                        cmd.ExecuteNonQuery();
                    }
                }

                return Ok("Successfully Updated");

            
           
        }


    // here we can delete the data or object or an employee's information ??
        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            using (SqlConnection sql = new SqlConnection(DB_Connection))
            {
                sql.Open();
                string sql_String = @"
                 DELETE FROM EmployeeDatas
                 WHERE Id = @id
                ";

                using (SqlCommand cmd = new SqlCommand(sql_String , sql))
                {
                    cmd.Parameters.AddWithValue("@id",id);

                    cmd.ExecuteNonQuery();
                }
            }

         return Ok("Deleted Sucessfully !!");
        }
        
    }
}
