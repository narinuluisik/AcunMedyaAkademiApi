using Microsoft.AspNetCore.Mvc;

namespace AcunMedyaAkademiWebUI.ViewComponents.Default
{
	public class SubscribePartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}

}