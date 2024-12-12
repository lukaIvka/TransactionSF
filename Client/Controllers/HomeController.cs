using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Common.Interfaces;
using Newtonsoft.Json;
using Client.Models;

namespace Client.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidateInput(string book, uint quantity)
        {
            string retval = "";

            if (string.IsNullOrWhiteSpace(book))
            {
                TempData["ValidationError"] = "You must select a book!";
                return RedirectToAction("Index");
            }

            if (quantity == 0)
            {
                TempData["ValidationError"] = "Quantity must be at least 1.";
                return RedirectToAction("Index");
            }

            /*ITransaction _transaction = ServiceProxy.Create<ITransaction>(
                new Uri("fabric:/transaction/TransactionCoordination"));
            bool povrat = await _transaction.Prepare();
            */


            IValidate _validationService = ServiceProxy.Create<IValidate>(
                      new Uri("fabric:/transaction/validation"));
            retval = await _validationService.Validate(book, quantity);

            if (string.IsNullOrEmpty(retval))
            {
                TempData["ValidationError"] = "Bad input";
                return RedirectToAction("Index");
            }

            if (retval.Equals("success"))
            {
                TempData["ValidationError"] = "";
                Book order = new Book() { Title = book, Amount = (int)quantity };
                var obj = JsonConvert.SerializeObject(order);
                HttpContext.Session.SetString("Order", obj);
                HttpContext.Session.Remove("PaymentError");
                return RedirectToAction("Index", "Payment");
            }
            return View();
            

            
                
            

        }
        /*
        // Priprema za kupovinu
        try
        {
            double pricePerItem = _bookstoreService.GetItemPrice(book);
            double totalCost = pricePerItem * quantity;

            TempData["ValidationError"] = $"Purchase validated! Total cost: {totalCost:C}.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ValidationError"] = $"An error occurred: {ex.Message}";
            return RedirectToAction("Index");
        }*/

    }
}
