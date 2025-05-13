using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebUI.ViewComponents.Default
{
	public class _HeaderPartial : ViewComponent

	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}

}