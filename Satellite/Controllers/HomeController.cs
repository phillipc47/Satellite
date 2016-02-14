using System.Web.Mvc;

namespace Satellite.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			//TODO: Add content linkage
			ViewBag.Message = "Simple application for integrating with NASA API's and demonstrating basic concepts";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}