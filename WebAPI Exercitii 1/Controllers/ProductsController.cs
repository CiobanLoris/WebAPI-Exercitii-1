using Microsoft.AspNetCore.Mvc;
using WebAPI_Exercitii_1.Models;

namespace WebAPI_Exercitii_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : Controller
    {
        public static readonly List<Product> _products = new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "apple",
                Description = "Description",
                ratings = new[] {1, 5,7},
                CreatedON = DateTime.Now
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "apple 2",
                Description = "Description",
                ratings = new[] {1, 4,10},
                CreatedON = DateTime.Now

            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "apple 2",
                Description = "Description",
                ratings = new[] {1, 4,2},
                CreatedON = DateTime.Now

            }
        };

        [HttpGet]
        public Product[] GetAllProducts()
        {
            return _products.ToArray();
        }

        [HttpPost("create product")]

        public IActionResult CreateProduct([FromBody]Product product)
        {
            if (product == null)
            {
                return BadRequest("Product is null");
            }
            foreach (var item in _products)
            {
                if(product.Id == item.Id)
                    return BadRequest("ID item allready exists");
            }
            _products.Add(product);
            return Ok(product);
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteProduct(Guid id)
        {
            foreach(var item in _products)
            {
                if(item.Id == id)
                { 
                    _products.Remove(item);
                     return Ok(item);
                }
            }
                return NotFound("Product not found");
        }

        [HttpGet("search-by-keyword{keyword}")]
        public List<Product> SearchByKeyword(string keyword) 
        {
            List<Product> products = new();
            foreach(var item in _products)
            {
                if(item.Name.Equals( keyword )|| item.Description.Equals( keyword))
                    products.Add(item);
            }
            return products;
        }


        [HttpGet("sort-ASC-rating")]

        public List<Product> SortByRatingASC()
        {
            List<Product> products = _products;
           for(int i = 0;i<products.Count;i++)
            {
                int avr = products[i].ratings.Sum() / products[i].ratings.Length;

                for (int j = i;j<products.Count;j++)
                {
                    int avr2 = products[j].ratings.Sum() / products[j].ratings.Length;
                    if(avr2 < avr)
                    {
                        Product temp = products[j];
                        products[j] = products[i];
                        products[i] = temp;
                    }
                }
            }

            return products;  
        }

        [HttpGet("sort-DSC-rating")]

        public List<Product> SortByRatingDSC()
        {
            List<Product> products = _products;
            for (int i = 0; i < products.Count; i++)
            {
                int avr = products[i].ratings.Sum() / products[i].ratings.Length;

                for (int j = i; j < products.Count; j++)
                {
                    int avr2 = products[j].ratings.Sum() / products[j].ratings.Length;
                    if (avr2 > avr)
                    {
                        Product temp = products[j];
                        products[j] = products[i];
                        products[i] = temp;
                    }
                }
            }

            return products;
        }

    }
}
