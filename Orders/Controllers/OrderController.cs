using Microsoft.AspNetCore.Mvc;
using Ninject;
using Orders.Models.DB;
using Orders.Models.DTO;
using Orders.Models.Filter;
using Orders.Service;
using Orders.Service.Orders;

namespace Orders.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _order;
        public OrderController(IOrderService order)
        {
            _order = order;
        }
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int providerId)
        {
            var filter = new OrderFilter(providerId, startDate, endDate);
            var orders = await _order.GetByFilterAsync(filter);
            return View(orders);
        }

        [Route("/DetailInfo/{id}")]//
        public async Task<IActionResult> DetailInfo(int id)
        {
            var response = await _order.GetOnceById(id); 
            return View(response);
        }

        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Route("/Order")]//
        public async Task<IActionResult> CreateOrderAsync(string Number, int ProviderId, DateTime Date)
        {
            var responce = await _order.CreateAsync(new OrderDto(Number, ProviderId, Date));
            if(responce == null)
			{
                return BadRequest("Error");
			}
            return RedirectToAction(nameof(Index));
        }
    }
}
