using Microsoft.AspNetCore.Mvc;

namespace IntentMyDataKart.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
