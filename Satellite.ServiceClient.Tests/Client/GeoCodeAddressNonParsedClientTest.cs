using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Satellite.Envrionment;
using Satellite.ServiceClient.Client;

namespace Satellite.ServiceClient.Tests.Client
{
	[TestFixture]
	public class GeoCodeAddressNonParsedClientTest
	{
		private class ExpectedValues
		{
			public const string Url = @"https://geoservices.tamu.edu/Services/Geocode/WebService/GeocoderService_V04_01.asmx";
			public const string ApiKey = @"";
		}
		private Mock<IApplicationConfiguration> ApplicationConfiguration { get; set; }

		private void ExpectLookupUrl()
		{
			ApplicationConfiguration.Setup(applicationConfiguration => applicationConfiguration.GeoCodeServiceUrl).Returns(ExpectedValues.Url);
			ApplicationConfiguration.Setup(applicationConfiguration => applicationConfiguration.ApiKey).Returns(ExpectedValues.ApiKey);
		}

		[SetUp]
		public void SetUp()
		{
			ApplicationConfiguration = new Mock<IApplicationConfiguration>();
			ExpectLookupUrl();
		}

		[Test]
		public void DoIt()
		{
			GeoCodeAddressNonParsedClient client = new GeoCodeAddressNonParsedClient(ApplicationConfiguration.Object);
			client.SendData();
		}
	}
}
