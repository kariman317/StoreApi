using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IProductRepository product;
        private readonly ICartRepository cart;
        private readonly UserManager<User> userManager;

        private Task<User> GetCurrentUserAsync() =>

        userManager.GetUserAsync(HttpContext.User);

        public CartController(IProductRepository product,
            ICartRepository cart, UserManager<User> userManager)
        {
            this.product = product;
            this.cart = cart;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Cart> carts = cart.GetAll();
            if (carts == null)
                return BadRequest("Empty Cart");
            return Ok(carts);
        }

        [HttpGet("{id:int}", Name = "getOneCart")]
        public IActionResult GetById(int id)
        {
            Cart crt= cart.GetById(id);
            if (crt == null)
                return BadRequest("Not Found");
            return Ok(crt);
        }

       

        [HttpPost]
        public IActionResult New(Cart crt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cart.Insert(crt);
                    string url = Url.Link("getOneCart", new { id = crt.Id });
                    return Created(url, product);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Cart crt)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cart.Update(id, crt);
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
                    cart.Delete(id);
                    return StatusCode(204, "Data Deleted");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }
    }
}
