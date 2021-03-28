using Microsoft.AspNetCore.Mvc;

namespace Bondora.Rental.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
