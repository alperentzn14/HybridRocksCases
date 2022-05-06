using HybridRocksCases.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HybridRocksCases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly HybridDbContext _HybridContext;
        public OrderController(HybridDbContext HybridContext)
        {
            _HybridContext = HybridContext;
        }

        // POST
        [HttpPost]
        public async Task<ActionResult<addOrderDTO>> AddOrder(addOrderDTO order)
        {
            var orderItem = new Order
            {
                UserId = order.UserId,
                ProductId = order.ProductId,
                IsStatus = order.IsStatus
            };

            _HybridContext.Orders.Add(orderItem);
            await _HybridContext.SaveChangesAsync();

            return order;
        }
        //// GET
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Order>>> GetOrderDetail(int id)
        {
            var ordersDetail = await _HybridContext.Orders
                .Where(o => o.Id == id)
                .Include(p => p.Product)
                .Include(u => u.User)
                .ToListAsync();

            return ordersDetail;
        }

        // PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var orderItem = await _HybridContext.Orders.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            orderItem.ProductId= order.ProductId;
            orderItem.UserId= order.UserId;
            orderItem.IsStatus = order.IsStatus;

            try
            {
                await _HybridContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!OrderItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        private bool OrderItemExists(int id)
        {
            return _HybridContext.Orders.Any(o => o.Id == id);
        }

        //// GET
        /*[HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrderDetails()
        {


            var results = from o in _HybridContext.Orders
                            join p in _HybridContext.Products on o.ProductId equals p.Id
                            select new
                            {
                                ProductName = p.ProductName,
                                Id = o.Id
                                //DepartmentName = d.Name
                            };
            return results;
        }*/



        /* [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>>  GetOrderDetailList() {

    var result = (from p in _HybridContext.Products
                           join o in _HybridContext.Orders on p.Id equals o.ProductId
                           join c in _HybridContext.Users on o.UserId equals c.Id
                           select new
                           {

                               p.ProductName,
                               p.Price,
                               c.Name+' '+ c.Surname,
                              

                           });

    return await result;
          }*/


    }

}
