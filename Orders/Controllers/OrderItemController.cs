using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Models.DB;
using Orders.Models.DTO;
using Orders.Service.OrderItems;

namespace Orders.Controllers
{
    public class OrderItemController : Controller
    {
        IOrderItemService _orderItem;
        public OrderItemController(IOrderItemService order)
        {
            _orderItem = order;
        }
        // GET: OrderItemController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetailsCreate(int id)
        {
            ViewBag.OrderId = id;
            var model = new OrderItemDto();
            model.OrderId = id;
            return PartialView(model);
        }
        public async Task<IActionResult> DetailsEdit(int id)
        {
            var response = await _orderItem.GetOnceById(id);
            return PartialView(response);
        }

        [HttpPost]
        [Route("/OrderItem/Edit")]//
        public async Task<ActionResult> EditAsync([FromForm] OrderItem dto)
        {
            await _orderItem.EditAsync(dto);
            return RedirectToAction("DetailInfo", "Order", new { id = dto.OrderId });
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] OrderItemDto dto)
        {
            await _orderItem.AddOnceAsync(dto);
            return RedirectToAction("DetailInfo", "Order", new { id = dto.OrderId });
        }


        // GET: OrderItemController/Delete/5
        [HttpGet]
        [Route("/OrderItem/Delete/{id}")]//
        public async Task<IActionResult> DeleteOrderAsync(int Id)
        {
            var redirectId = await _orderItem.GetOnceById(Id);

            await _orderItem.DeleteAsync(Id);

            return RedirectToAction("DetailInfo", "Order", new { id = redirectId.OrderId });
        }
    }
}
