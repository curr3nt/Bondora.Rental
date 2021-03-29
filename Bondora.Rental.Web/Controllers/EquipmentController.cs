using Bondora.Rental.Web.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace Bondora.Rental.Web.Controllers
{
    public class EquipmentController : Controller
    {
        public IActionResult Index()
        {
            var equipmentCollection = EquipmentCollection.FromSampleFile();
            return View("EquipmentCollection", equipmentCollection);
        }
    }
}
