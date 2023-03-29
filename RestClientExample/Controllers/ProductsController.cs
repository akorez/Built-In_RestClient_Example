using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestClientExample.Models;

namespace RestClientExample.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private List<Product> products = new List<Product>()
		{
			new Product() { Id = 1, Name = "Product 1", Price = 10.99M },
			new Product() { Id = 2, Name = "Product 2", Price = 19.99M },
			new Product() { Id = 3, Name = "Product 3", Price = 35.99M }
		};

		[HttpGet]
		public IEnumerable<Product> Get()
		{
			return products;
		}

		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var product = products.FirstOrDefault(p => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			return Ok(product);
		}

		[HttpPost]
		public IActionResult Post([FromBody] Product product)
		{
			if (product == null)
			{
				return BadRequest("Product cannot be null");
			}

			products.Add(product);
			return Ok(product);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Product product)
		{
			if (product == null || product.Id != id)
			{
				return BadRequest("Product ID mismatch");
			}

			var existingProduct = products.FirstOrDefault(p => p.Id == id);
			if (existingProduct == null)
			{
				return NotFound();
			}

			existingProduct.Name = product.Name;
			existingProduct.Price = product.Price;

			return Ok(existingProduct);
		}


		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var product = products.FirstOrDefault(p => p.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			products.Remove(product);
			return Ok();
		}
	}
}
