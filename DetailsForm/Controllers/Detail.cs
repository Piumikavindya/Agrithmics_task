using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DetailsForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Detail : ControllerBase
    {
    private IConfiguration _configuration;
        public Detail(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetNotes")]
        public JsonResult GetNotes()
        {
            string query = "select * from dbo.Data";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("demo");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();

                }
       
            }
        
            return new JsonResult(table);
        }

        [HttpPost]
        [Route("AddNotes")]
        public JsonResult AddNotes([FromBody] string name, [FromBody] string addres, [FromBody] string contactNo)
        {
            string query = "insert into dbo.Data (name, addres, contactNo) values (@name, @addres, @contactNo)";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("demo");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@name", name);
                    myCommand.Parameters.AddWithValue("@addres", addres);
                    myCommand.Parameters.AddWithValue("@contactNo", contactNo);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

    }
}
