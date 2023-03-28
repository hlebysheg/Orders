using Microsoft.AspNetCore.Mvc;
using Ninject;
using Orders.Models.DB;
using Orders.Models.DTO;
using Orders.Models.Filter;
using Orders.Service;
using Orders.Service.Orders;
using Orders.Service.Providers;

namespace Orders.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _order;
        IProviderService _provider;
        public OrderController(IOrderService order, IProviderService provider)
        {
            _order = order;
            _provider = provider;
        }
        public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate, int providerId, FilterColum? column)
        {
            var providers = await _provider.GetProviderListAsync();
            ViewBag.provider = providers;
            var filter = new OrderFilter(providerId, startDate, endDate, column);
            var orders = await _order.GetByFilterAsync(filter);
            return View(orders);
        }

        [Route("/DetailInfo/{id}")]//
        public async Task<IActionResult> DetailInfo(int id)
        {
            var response = await _order.GetOnceById(id); 
            return View(response);
        }

        public async Task<IActionResult> Create()
        {
            var providers = await _provider.GetProviderListAsync();
            ViewBag.provider = providers;
            return PartialView();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _order.GetOnceById(id);
            return PartialView(response);
        }

        [HttpPost]
        [Route("/Order")]//
        public async Task<IActionResult> CreateOrderAsync(string Number, int ProviderId, DateTime Date)
        {
            var responce = await _order.CreateAsync(new OrderDto(Number, ProviderId, Date));
            if(responce == null)
			{
                TempData["ErrorMessage"] = "You have already created this order";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Route("/Order/Edit")]//
        public async Task<IActionResult> EditOrderAsync(int Id,string Number, int ProviderId, DateTime Date)
        {
            var dto = new OrderDto(Number, ProviderId, Date);
            var responce = await _order.UpdateAsync(Id, dto);
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/Order/Delete/{id}")]//
        public async Task<IActionResult> DeleteOrderAsync(int Id)
        {
            var responce = await _order.DeleteAsync(Id);

            return RedirectToAction(nameof(Index));
        }
    }
}
