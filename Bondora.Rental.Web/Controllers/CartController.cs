using Bondora.Rental.Web.DomainInterface;
using Bondora.Rental.Web.Equipment;
using Bondora.Rental.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bondora.Rental.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly AccessToInterface _accessToInterface;
        private readonly EquipmentCollection _equipment;

        public CartController()
        {
            _accessToInterface = new AccessToInterface();
            _equipment = EquipmentCollection.FromSampleFile();
        }

        private static Cart ReadCart(HttpRequest request, EquipmentCollection equipment)
        {
            List<CartCompactItem> items = new List<CartCompactItem>();
            var cartCookie = request.Cookies["cart"];
            if (!string.IsNullOrWhiteSpace(cartCookie))
                items = JsonSerializer.Deserialize<List<CartCompactItem>>(cartCookie);
            return Cart.Validate(items, equipment);
        }

        public IActionResult Index()
        {
            return View("Cart", ReadCart(Request, _equipment));
        }

        public IActionResult Invoice()
        {
            var cart = ReadCart(Request, _equipment);

            var invoice = _accessToInterface.CalculateInvoice(cart);

            return File(Encoding.UTF8.GetBytes(invoice.ToFile()), "text/plain", "invoice.txt");
        }
    }
}
