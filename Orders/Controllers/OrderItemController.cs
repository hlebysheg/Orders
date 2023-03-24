using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        // GET: OrderItemController/Details/5
        public ActionResult Details(int id)
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

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] OrderItemDto dto)
        {
            await _orderItem.AddOnceAsync(dto);
            return RedirectToAction("DetailInfo", "Order", new { id = dto.OrderId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        // GET: OrderItemController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderItemController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderItemController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderItemController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
