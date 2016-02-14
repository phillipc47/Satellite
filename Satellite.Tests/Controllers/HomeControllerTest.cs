using System.Web.Mvc;
using NUnit.Framework;
using Satellite.Controllers;

namespace Satellite.Tests.Controllers
{
	[TestFixture]
	public class HomeControllerTest
	{
		[Test]
		public void Index()
		{
			HomeController controller = new HomeController();

			ViewResult result = controller.Index() as ViewResult;

			Assert.IsNotNull(result);
		}

		[Test]
		public void About()
		{
			HomeController controller = new HomeController();

			ViewResult result = controller.About() as ViewResult;

			Assert.NotNull(result);
			Assert.NotNull(result.ViewBag);
			// TODO: Hard coded strings
			Assert.AreEqual("Simple application for integrating with NASA API's and demonstrating basic concepts", result.ViewBag.Message);
		}

		[Test]
		public void Contact()
		{
			HomeController controller = new HomeController();

			ViewResult result = controller.Contact() as ViewResult;

			Assert.IsNotNull(result);
		}
	}
}
