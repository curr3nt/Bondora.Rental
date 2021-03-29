using Bondora.Rental.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Bondora.Rental.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly DomainInterface.DomainInterface _accessToInterface;
        private readonly EquipmentCollection _equipment;
        private readonly ModelSpec _modelSpec;
        private readonly InvoiceSpec _invoiceSpec;

        public CartController()
        {
            _accessToInterface = new DomainInterface.DomainInterface();
            _modelSpec = new EnglishDictionary();
            _equipment = EquipmentCollection.FromSampleFile(_modelSpec);
            _invoiceSpec = new EnglishInvoice();
        }

        private static Cart ReadCart(
            HttpRequest request, EquipmentCollection equipment, ModelSpec spec)
        {
            List<CartCompactItem> items = new List<CartCompactItem>();
            var cartCookie = request.Cookies["cart"];
            if (!string.IsNullOrWhiteSpace(cartCookie))
                items = JsonSerializer.Deserialize<List<CartCompactItem>>(cartCookie);
            return Cart.Validate(items, equipment, spec);
        }

        private static void ClearCart(HttpResponse response)
        {
            response.Cookies.Delete("cart");
        }

        public IActionResult Index()
        {
            return View("Cart", ReadCart(Request, _equipment, _modelSpec));
        }

        public IActionResult Confirm()
        {
            var cart = ReadCart(Request, _equipment, _modelSpec);

            var invoice = _accessToInterface.CalculateInvoice(cart);

            ClearCart(Response);

            return File(Encoding.UTF8.GetBytes(
                invoice.ToFile(_invoiceSpec)), "text/plain", _invoiceSpec.FileName());
        }
    }
}
