using Bondora.Rental.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bondora.Rental.Web.Controllers
{
    public class EquipmentController : Controller
    {
        public IActionResult Index()
        {
            var equipment = EquipmentCollection.FromSampleFile(new EnglishDictionary());
            return View("EquipmentCollection", equipment);
        }
    }
}
