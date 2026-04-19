using Microsoft.AspNetCore.Mvc;

namespace IntentMyDataKart.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult CompanyRegistration()
        {
            return View();
        }
    }
}
