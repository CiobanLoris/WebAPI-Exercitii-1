using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using WebAPI_Exercitii_1.Models;

namespace WebAPI_Exercitii_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        public static readonly List<Store> _stores = new List<Store>
        {
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "name",
                Country="",
                City="City 1",
                MonthlyIncome = 40,
                OwnerName = "Owner 1",
                ActiveSince = DateTime.Now,
            },
            new Store
            {
                Id = Guid.NewGuid(),
                Name = "name 2",
                Country="",
                City="City 2",
                MonthlyIncome = 35,
                OwnerName = "Owner 1",
                ActiveSince = DateTime.MinValue,
            }
        };

        [HttpGet]
        public Store[] GetAllStores()
        {
            return _stores.ToArray();
        }

        [HttpPost]
        public IActionResult CreateStore([FromBody]Store store)
        {
            if (store == null)
            {
                return BadRequest("Store is null");
            }
            foreach(var existingStore in _stores)
            {
               if(store.Id ==  existingStore.Id )
                    return BadRequest("Store with the same Id alreay exists");
            }
            _stores.Add(store);
            return Ok(store);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteStore(Guid id)
        {
           
            foreach (var existingStore in _stores)
            {
                if (existingStore.Id == id)
                    _stores.Remove(existingStore);
                return Ok();
            }
            return NotFound("Store not found");
            
        }

        [HttpPut("transfer-ownership/{storeId}")]
        public IActionResult TransferOwnership(Guid storeId,[FromBody] string name)
        {

            foreach (var existingStore in _stores)
            {
                if (existingStore.Id == storeId)
                    existingStore.Name = name;
                return Ok();
            }
            return NotFound("Store not found");

        }

        [HttpGet("search-by-keyword/{keyword}")]
        public List<Store> KeyWordSearch(string keyword )
        {
            List<Store> storesfound =  new ();
            foreach (var existingStore in _stores)
            {
                if (keyword.Equals(existingStore.Name) || keyword.Equals(existingStore.Country) || keyword.Equals(existingStore.City) || keyword.Equals(existingStore.OwnerName))
                    storesfound.Add(existingStore);
            }
            return storesfound;
            

        }

        [HttpGet("sort-by-MonthyIncome")]
        public List<Store> MonthlyIncomeSort()
        {
            
            return _stores.OrderBy(x => x.MonthlyIncome).ToList();  

        }

        [HttpGet("sort-by-oldest")]
        public Store OldestSort()
        {

             _stores.OrderByDescending(x => x.ActiveSince).ToList();
            return _stores[0];

        }

    }





}
