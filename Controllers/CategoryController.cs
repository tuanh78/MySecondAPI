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
    public class CategoryController : ControllerBase
    {
        private string connectionString = "Data Source=DESKTOP-G8AU2K2;Initial Catalog=cuahangkinhmat;Integrated Security=True";
        private IDbConnection db;

        public CategoryController()
        {
            db = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Lấy danh sách danh mục sản phẩm
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Trả về danh sách danh mục</response>
        /// <response code="204">If the item is null</response>
        /// CreatedBy: PTAnh
        [HttpGet]
        public IActionResult Get()
        {
            string sql = "Proc_GetCategories";
            var categories = db.Query<Category>(sql, commandType: CommandType.StoredProcedure);
            return StatusCode(StatusCodes.Status200OK, categories);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id">Id sản phẩm</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            string sql = "Proc_GetCategoryById";
            var category = db.QueryFirstOrDefault<Category>(sql, new { CategoryId = id }, commandType: CommandType.StoredProcedure);
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            string sql = "Proc_InsertCategory";
            var result = db.Execute(sql, param: category, commandType: CommandType.StoredProcedure);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Category category)
        {
            if (id != category.CategoryId.ToString())
            {
                return BadRequest("Nhap sai id roi ");
            }

            string sql = "Proc_UpdateCategory";
            var result = db.Execute(sql, param: category, commandType: CommandType.StoredProcedure);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            string sql = "Proc_DeleteCategory";
            var result = db.Execute(sql, new { CategoryId = id }, commandType: CommandType.StoredProcedure);
            return StatusCode(StatusCodes.Status508LoopDetected, result);
        }
    }
}