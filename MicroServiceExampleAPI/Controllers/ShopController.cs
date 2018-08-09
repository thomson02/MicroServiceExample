using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MicroServiceExampleAPI.Data;
using MicroServiceExampleAPI.Data.Entities;
using MicroServiceExampleAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MicroServiceExampleAPI.Controllers
{
    /// <summary>
    /// Shop controller.
    /// </summary>
    [ApiVersion("2.0")]
    public class ShopController : ApiControllerBase
    {
        private IExampleApiRepository _repository;
        private IMapper _mapper;

        public ShopController(IExampleApiRepository repository, IMapper _mapper)
        {
            _repository = repository;
            _mapper = _mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
            }
            catch(Exception e){
                return BadRequest("Failed to get Products");
            }
        }

        [HttpGet("{category}")]
        public IActionResult GetProductByCategory(string category)
        {
            try {
                var products = _repository.GetProductsByCategory(category);

                if (products.Any()){
                    return Ok(products);
                }

                return NotFound($"Failed to get Products by Category: {category}");
            }
            catch (Exception e)
            {
                return BadRequest($"Failed to get Products by Category: {category}");
            }
        }

        [HttpGet("orders")]
        public IActionResult GetOrders()
        {
            try
            {
                var orders = _repository.GetAllOrders();

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders));
            }
            catch (Exception e)
            {
                return BadRequest("Failed to get Orders");
            }
        }

        [HttpPost("orders")]
        public IActionResult CreateOrder([FromBody]OrderViewModel model){
            try {

                if (ModelState.IsValid){
                    var newOrder = new Order
                    {
                        OrderDate = model.OrderDate,
                        OrderNumber = model.OrderNumber,
                        Id = model.OrderId
                    };

                    if (newOrder.OrderDate == DateTime.MinValue) {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);

                    if (_repository.SaveAll())
                    {
                        var vm = new OrderViewModel
                        {
                            OrderId = newOrder.Id,
                            OrderDate = newOrder.OrderDate,
                            OrderNumber = newOrder.OrderNumber
                        };

                        return Created($"/api/v2/orders/{vm.OrderId}", vm);  // there is a better way to do this.
                    }
                }

                return BadRequest(ModelState);
            }
            catch(Exception e){
                return BadRequest();
            }
        }
    }
}
