using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository product;

        public ProductController(IProductRepository product )
        {
            this.product = product;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            List<Product> products = product.GetAll();
            if (products == null)
                return BadRequest("Empty List");
            return Ok(products);
        }

        [HttpGet("{id:int}", Name = "getOneProduct")]
        public IActionResult GetById(int id)
        {
            Product prd = product.GetById(id);
            if (prd == null)
                return BadRequest("Not Found");
            return Ok(prd);
        }

        [HttpGet("{Name:alpha}")]
        [AllowAnonymous]
        public IActionResult GetByName(string Name)
        {
            Product prd = product.GetByName(Name);
            if (product == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult New(Product prd)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    product.Insert(prd);
                    string url = Url.Link("getOneProduct", new { id = product.Id });
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Product prd)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    product.Update(id, prd);
                    return StatusCode(204, "Data Saved");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
           try
                {
                    product.Delete(id);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
           

        }
        [HttpPost("uploadImage"), DisableRequestSizeLimit]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();



                //everything else is the same
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
