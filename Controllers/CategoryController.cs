using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.DTO;
using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository category;

        public CategoryController(ICategoryRepository category)
        {
            this.category = category;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            List<Category> catList = category.GetAll();
            if (catList == null)
            {
                return BadRequest("Empty Category");
            }
            return Ok(catList);
        }

        [HttpGet("{id:int}", Name = "getOneCategory")]
        public IActionResult GetByID(int id)
        {
            Category cat = category.GetById(id);
            if (cat == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(cat);
        }

        [HttpGet("{Name:alpha}")]
        [AllowAnonymous]
        public IActionResult GetByName(string Name)
        {
            Category cat = category.GetByName(Name);
            if (cat == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(cat);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Category cat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    category.Insert(cat);
                    string url = Url.Link("getOneCategory", new { id = cat.Id });
                    return Created(url, cat);
                }
                catch (Exception ex )
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Category cat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    category.Update(id, cat);

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
            if (ModelState.IsValid)
            {
                try
                {
                    category.Delete(id);

                    return StatusCode(204, "Data deleted");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpGet("Details/{CategoryId:int}")]
        public IActionResult GetProductsByCategoryId(int CategoryId)
        {
            List<Product> products = category.GetProductsbyCategoryID(CategoryId);

            Category catModel = category.GetById(CategoryId);

            CategoryWithProductsDTO categoryWithProductDTO =
               new CategoryWithProductsDTO();

            categoryWithProductDTO.CateogryId = catModel.Id;
            categoryWithProductDTO.CategoryName = catModel.Name;

            foreach (var item in products)
            {
                ProductDetailsDTO temp = new ProductDetailsDTO();
                temp.ProductId = item.Id;
                temp.ProductName = item.Name;
                categoryWithProductDTO.ProductList.Add(temp);
            }
            return Ok(categoryWithProductDTO);
        }
    }
}
