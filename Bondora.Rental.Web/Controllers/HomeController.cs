using Bondora.Rental.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bondora.Rental.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index", new Home(new EnglishDictionary()));
        }
    }
}
