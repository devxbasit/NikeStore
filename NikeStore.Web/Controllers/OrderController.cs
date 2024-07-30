using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NikeStore.Web.Models.Dto;
using NikeStore.Web.Service.IService;
using NikeStore.Web.Utility;

namespace NikeStore.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult OrderIndex()
        {
            return View();
        }

        [HttpGet]
        public async Task<ResponseDto> GetOrders()
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";
            if (!User.IsInRole(SD.Roles.Admin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            }

            ResponseDto response = await _orderService.GetAllOrder(userId);
            if (response != null && response.IsSuccess)
            {
                response.Result = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
            }
            else
            {
                response.Result = new List<OrderHeaderDto>();
            }

            return response;
        }


        public async Task<IActionResult> OrderDetail(int orderId)
        {
            OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
            string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            var response = await _orderService.GetOrder(orderId);
            if (response != null && response.IsSuccess)
            {
                orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
            }

            if (!User.IsInRole(SD.Roles.Admin) && userId != orderHeaderDto.UserId)
            {
                return NotFound();
            }

            return View(orderHeaderDto);
        }


        [HttpPost("OrderReadyForPickup")]
        public async Task<IActionResult> OrderReadyForPickup(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.OrderStatus.ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }

            return View("OrderIndex");
        }

        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.OrderStatus.Completed);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }

            return View("OrderIndex");
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, SD.OrderStatus.Cancelled);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }

            return View("OrderIndex");
        }
    }
}
