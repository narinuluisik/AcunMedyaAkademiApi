using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebUI.ViewComponents.Default
{
    public class _HeadPartial : ViewComponent

    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }

}