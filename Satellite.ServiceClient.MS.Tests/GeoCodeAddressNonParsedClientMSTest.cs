using System;
using System.IO;
using System.Net;
using System.Net.Fakes;
using System.Text;
using System.Xml.Serialization;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.QualityTools.Testing.Fakes.Shims;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Satellite.Data.Test;
using Satellite.Envrionment;
using Satellite.ServiceClient.Client;
using Satellite.ServiceClient.Model;
using Satellite.ServiceClient.Serialization;

namespace Satellite.ServiceClient.MS.Tests
{
	/* MSTest and MSTestRunner are required to utilize MS Fakes */
	[TestClass]
	public class GeoCodeAddressNonParsedClientMSTest
	{
		private static class ExpectedValues
		{
			public const string Url = @"http://www.bing.com";
			public const string ApiKey = @"SomeKey";
		}

		private void ExpectLookupUrl()
		{
			ApplicationConfiguration.Setup(applicationConfiguration => applicationConfiguration.GeoCodeServiceUrl).Returns(ExpectedValues.Url);
		}

		private void ExpectLookupApiKey()
		{
			ApplicationConfiguration.Setup(applicationConfiguration => applicationConfiguration.ApiKey).Returns(ExpectedValues.ApiKey);
		}

		private Stream BuildStream(string data = "")
		{
			if (string.IsNullOrEmpty(data))
			{
				return new MemoryStream();
			}

			MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(data));

			StreamWriter streamWriter = new StreamWriter(memoryStream);
			streamWriter.Write(data);
			streamWriter.Flush();
			memoryStream.Position = 0;

			return memoryStream;
		}

		private Mock<IApplicationConfiguration> ApplicationConfiguration { get; set; }
		private Mock<IBasicSerializer<GeoCodeAddressModel.Envelope>> RequestSerializer { get; set; }
		private Mock<IBasicSerializer<GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet>> ResponseSerializer { get; set; }

		private GeoCodeAddressNonParsedClient Client { get; set; }

		private GeoCodeAddressModel.GeocodeAddressNonParsed CreateAddressData()
		{
			GeoCodeAddressModel.GeocodeAddressNonParsed addressData = new GeoCodeAddressModel.GeocodeAddressNonParsed
			{
				state = "California",
				streetAddress = "1600 Amphitheatre Pkwy",
				city = "Mountain View",
				zip = "94043"
			};
			return addressData;
		}

		[TestInitialize]
		public void Initialize()
		{
			ApplicationConfiguration = new Mock<IApplicationConfiguration>();	
			ExpectLookupUrl();
			ExpectLookupApiKey();

			RequestSerializer = new Mock<IBasicSerializer<GeoCodeAddressModel.Envelope>>();
			ResponseSerializer = new Mock<IBasicSerializer<GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet>>();

			Client = new GeoCodeAddressNonParsedClient(ApplicationConfiguration.Object, RequestSerializer.Object, ResponseSerializer.Object);

			ShimBehaviors.BehaveAsDefaultValue();
		}

		private GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet SendData(HttpStatusCode statusCode, GeoCodeAddressModel.GeocodeAddressNonParsed addressData)
		{
			using (ShimsContext.Create())
			{
				var requestShim = new ShimHttpWebRequest();
				var responseShim = new ShimHttpWebResponse();

				ShimWebRequest.CreateHttpString = (url) => requestShim.Instance;
				requestShim.HeadersGet = () => new WebHeaderCollection();

				ExpectSerialize(GeoCodeAddressModelSample.StringSample);
				GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet serviceResponse = GeoCodeAddressModelResponseSample.TypedSample;
				ExpectDeSerialize(serviceResponse);

				using (Stream requestStream = BuildStream(), responseStream = BuildStream(GeoCodeAddressModelResponseSample.StringSample))
				{
					requestShim.GetRequestStream = () => requestStream;
					requestShim.GetResponse = () => responseShim.Instance;

					responseShim.StatusCodeGet = () => statusCode;
					responseShim.GetResponseStream = () => responseStream;

					GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet result;
					Assert.IsTrue(Client.SendData(addressData, out result));
					Assert.IsNotNull(result);

					return result;
				}
			}
		}

		private void ExpectSerialize(string expectedResult)
		{
			RequestSerializer.Setup(serializer => serializer.Serialze(It.IsAny<GeoCodeAddressModel.Envelope>())).Returns(expectedResult);
		}

		private void ExpectDeSerialize(GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet ExpectedResult)
		{
			ResponseSerializer.Setup(serializer => serializer.DeSerialize(It.IsAny<string>())).Returns(ExpectedResult);
		}

		[TestMethod]
		public void EverythingGoesAsItShould()
		{
			GeoCodeAddressModel.GeocodeAddressNonParsed addressData = CreateAddressData();
			GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet result = SendData(HttpStatusCode.OK, addressData);

			int brk = 5;
		}
	}
}
