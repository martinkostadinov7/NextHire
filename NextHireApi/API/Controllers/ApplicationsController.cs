using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ApplicationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
