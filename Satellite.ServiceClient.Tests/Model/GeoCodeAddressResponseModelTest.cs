using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using NUnit.Framework;
using Satellite.Data.Test;
using Satellite.ServiceClient.Model;
using Satellite.ServiceClient.Serialization;

namespace Satellite.ServiceClient.Tests.Model
{
	[TestFixture]
	public class GeoCodeAddressResponseModelTest
	{
		private void CheckResult(GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet expected, GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet queryResult)
		{
			Assert.AreEqual(expected.QueryStatusCode, queryResult.QueryStatusCode);
			Assert.AreEqual(expected.ExceptionOccurred, queryResult.ExceptionOccurred);
			Assert.AreEqual(expected.QueryStatusCodeName, queryResult.QueryStatusCodeName);
			Assert.AreEqual(expected.QueryStatusCodeValue, queryResult.QueryStatusCodeValue);
			Assert.AreEqual(expected.TimeTaken, queryResult.TimeTaken);
			Assert.AreEqual(expected.TransactionId, queryResult.TransactionId);
			Assert.AreEqual(expected.Version, queryResult.Version);

			var expectedDetail = expected.WebServiceGeocodeQueryResults.WebServiceGeocodeQueryResult;
			var resultDetail = queryResult.WebServiceGeocodeQueryResults.WebServiceGeocodeQueryResult;

			Assert.AreEqual(expectedDetail.TransactionId, resultDetail.TransactionId);
			Assert.AreEqual(expectedDetail.Latitude, resultDetail.Latitude);
			Assert.AreEqual(expectedDetail.Longitude, resultDetail.Longitude);
			Assert.AreEqual(expectedDetail.Quality, resultDetail.Quality);
			Assert.AreEqual(expectedDetail.Version, resultDetail.Version);
		}

		private string CleanString(string source)
		{
			return Regex.Replace(source, @"\t|\n|\r|[\s]{2,}", string.Empty);
		}

		[Test]
		public void Deserialize()
		{
			XmlSerializer serializer = new XmlSerializer(typeof(GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet));
			byte[] buffer = Encoding.UTF8.GetBytes(GeoCodeAddressModelResponseSample.StringSample);

			using (var stream = new MemoryStream(buffer))
			{
				var rawResult = serializer.Deserialize(stream);
				Assert.IsNotNull(rawResult);
				Assert.AreEqual(typeof(GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet), rawResult.GetType());

				GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet queryResult = (GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet)rawResult;
				GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet expected = GeoCodeAddressModelResponseSample.TypedSample;
				CheckResult(expected, queryResult);
			}
		}


		[Test]
		public void Serialize()
		{
			BasicSerializer<GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet> serializer = new BasicSerializer<GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet>();

			// Object -> String
			string objectSerialized = serializer.Serialze(GeoCodeAddressModelResponseSample.TypedSample);

			// String -> Object
			GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet stringSerialized = serializer.DeSerialize(GeoCodeAddressModelResponseSample.StringSample);
			// And Back
			string result = serializer.Serialze(stringSerialized);

			// Make sure they are the same
			Assert.AreEqual(objectSerialized, result);
		}
	}
}
