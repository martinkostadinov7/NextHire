using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class OffersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
