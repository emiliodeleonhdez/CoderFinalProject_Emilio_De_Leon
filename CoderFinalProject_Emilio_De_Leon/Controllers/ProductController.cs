using CoderFinalProject_Emilio_De_Leon.Models;
using CoderFinalProject_Emilio_De_Leon.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Data.SqlClient;
using System.Text.Json;

namespace CoderFinalProject_Emilio_De_Leon.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private ProductRepository repository = new ProductRepository();

        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("[action]")]

        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto>? ProductList = repository.ListProducts();
                return Ok(ProductList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpPost]
        [Route("[action]")]

        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                Producto createProducto = repository.CreateProduct(producto);
                return StatusCode(StatusCodes.Status201Created, createProducto);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpDelete]
        [Route("[action]")]

        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool DeleteProduct = repository.DeleteProduct(id);
                if (DeleteProduct)
                {
                    return Ok();
                }
                else { return NotFound(); }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpPut]
        [Route("[action]")]

        public ActionResult Put([FromBody] Producto producto)
        {
            try
            {
                bool UpdateProduct = repository.UpdateProduct(producto);
                if (UpdateProduct)
                {
                    return Ok("Product updated");
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
