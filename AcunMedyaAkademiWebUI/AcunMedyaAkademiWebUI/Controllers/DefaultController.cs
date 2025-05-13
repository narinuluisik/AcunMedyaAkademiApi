using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebUI.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
