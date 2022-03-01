using Microsoft.AspNetCore.Mvc;

namespace Helperland.Controllers
{
    public class CustomerServiceController : Controller
    {
        public IActionResult CustomerDashboard()
        {
            return View();
        }
    }
}
