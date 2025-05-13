using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebUI.ViewComponents.Default
{
	public class _FooterPartial : ViewComponent

	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}

}