using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySecondAPI.Model;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MySecondAPI.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private string connectionString = "Data Source=DESKTOP-G8AU2K2;Initial Catalog=cuahangkinhmat;Integrated Security=True";

        //string connectionString = "server=47.241.69.179;user id=dev;password=12345678;port=3306;database=MF0_NVManh_CukCuk02;AllowZeroDateTime=true";
        private IDbConnection dbConnection;

        public ProductController()
        {
            dbConnection = new SqlConnection(connectionString);
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            //string sql = "Proc_GetCustomers";
            //var products = dbConnection.Query<Customer>(sql, commandType: CommandType.StoredProcedure);
            string sql = "Proc_GetProducts";
            var products = dbConnection.Query<Product>(sql, commandType: CommandType.StoredProcedure);
            return StatusCode(StatusCodes.Status200OK, products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            string sql = "Proc_GetProductById";
            var product = dbConnection.QueryFirstOrDefault<Product>(sql, new { ProductId = id }, commandType: CommandType.StoredProcedure);
            return StatusCode(StatusCodes.Status200OK, product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            //product.ProductId = Guid.NewGuid();
            //product.ProductCategoryId = Guid.NewGuid();
            //product.ProductSupplierId = Guid.NewGuid();
            string sql = "Proc_InsertProduct";
            var result = dbConnection.Execute(sql, param: product, commandType: CommandType.StoredProcedure);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Product product)
        {
            if (id != product.ProductId.ToString())
            {
                return BadRequest("Nhap id ngu vcl dm tu anh!");
            }
            string sql = "Proc_UpdateProduct";
            var result = dbConnection.Execute(sql, param: product, commandType: CommandType.StoredProcedure);
            return Ok();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            string sql = "Proc_DeleteProduct";
            var result = dbConnection.Execute(sql, new { ProductId = id }, commandType: CommandType.StoredProcedure);
            return StatusCode(StatusCodes.Status508LoopDetected, result);
        }
    }
}