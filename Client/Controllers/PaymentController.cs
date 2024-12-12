using Client.Models;
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View("Payment");
        }
        [HttpPost]
        public async Task<IActionResult>  ProcessPayment()
        {
            string PaymentError = HttpContext.Session.GetString("PaymentError");
            TempData["PaymentError"] = null;
            IBookstore bookStoreService = ServiceProxy.Create<IBookstore>(new Uri("fabric:/transaction/Bookstore"));
            IBank bankService = ServiceProxy.Create<IBank>(new Uri("fabric:/transaction/Bank"));
            var objAsJson = HttpContext.Session.GetString("Order");
            Book order = JsonConvert.DeserializeObject<Book>(objAsJson);
            order.Price = await bookStoreService.GetItemPrice(order.Title);

            if (objAsJson == null)
            {
                return NotFound("Object not found in session");
            }
            ViewBag.BookName = order.Title;
            ViewBag.Quantity = order.Amount;
            ViewBag.TotalPrice = order.Price;
            //ViewBag.Balance = await bankService.GetUserBalance(1); 
            if (!string.IsNullOrEmpty(PaymentError))
            {
                TempData["PaymentError"] = PaymentError;
            }

            return View("PaymentPage");
        }
    }
}
