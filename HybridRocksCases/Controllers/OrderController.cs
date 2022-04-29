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
        //// GETLİST
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrderList()
        {
            return await _HybridContext.Orders.ToListAsync();
        }
    }
}
