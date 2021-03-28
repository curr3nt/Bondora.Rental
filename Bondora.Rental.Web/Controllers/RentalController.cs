using Bondora.Rental.Web.DomainInterface;
using Bondora.Rental.Web.Equipment;
using Bondora.Rental.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bondora.Rental.Web.Controllers
{
    public class RentalController : Controller
    {
        private readonly AccessToInterface _accessToInterface;

        public RentalController()
        {
            _accessToInterface = new AccessToInterface();
        }

        public IActionResult Invoice()
        {
            var equipment = EquipmentCollection.FromSampleFile();
            var iorderthis = new List<EquipmentOrder>
            {
                new EquipmentOrder { Name = "KamAZ truck", RentalDays = 30 },
                new EquipmentOrder { Name = "Bosch jackhammer", RentalDays = 1 },
            };

            var invoice = _accessToInterface.CalculateInvoice(iorderthis, equipment);

            return File(Encoding.UTF8.GetBytes(invoice.ToFile()), "text/plain", "invoice.txt");
        }
    }
}
