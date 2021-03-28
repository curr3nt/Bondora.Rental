using Bondora.Rental.Web.Equipment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
