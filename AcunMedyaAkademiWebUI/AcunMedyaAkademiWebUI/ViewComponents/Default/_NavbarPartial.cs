using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebUI.ViewComponents.Default
{
	public class _NavbarPartial : ViewComponent

	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}

}