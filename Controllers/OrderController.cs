using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreAPI.Models;
using StoreAPI.Services;
using System;
using System.Collections.Generic;

namespace StoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository order;

        public OrderController(IOrderRepository order)
        {
            this.order = order;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Order> orders = order.GetAll();
            if (orders == null)
                return BadRequest("Not Found");
            return Ok(orders);
        }

        [HttpGet("{id:int}", Name = "getOneOrder")]
        public IActionResult GetById(int id)
        {
            Order ordr = order.GetById(id);
            if (ordr == null)
                return BadRequest("Not Found");
            return Ok(ordr);
        }



        [HttpPost]
        public IActionResult New(Order ordr)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    order.Insert(ordr);
                    string url = Url.Link("getOneOrder", new { id = ordr.Id });
                    return Created(url, ordr);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest(ModelState);

        }

        [HttpPut("{id:int}")]
        public IActionResult Edit(int id, Order ord)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    order.Update(id, ord);
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
                    order.Delete(id);
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
