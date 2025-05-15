using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebUI.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
