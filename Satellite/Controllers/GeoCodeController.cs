using System.Web.Mvc;
using Satellite.Models.GeoCode;

namespace Satellite.Controllers
{
    public class GeoCodeController : Controller
    {
		[HttpGet]
        public ActionResult Index()
        {
            return View();
        }

		[HttpPost]
	    public ActionResult Search(GeoCodeViewModel model)
	    {
			return RedirectToAction("Index");
		}
    }
}