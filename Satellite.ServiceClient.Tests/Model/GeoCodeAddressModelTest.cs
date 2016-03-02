using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NUnit.Framework;
using Satellite.ServiceClient.Model;
using System.Xml.Serialization;
using Satellite.Data.Test;

namespace Satellite.ServiceClient.Tests.Model
{
	[TestFixture]
	public class GeoCodeAddressModelTest
	{
		private void CheckResult(GeoCodeAddressModel.GeocodeAddressNonParsed expected, GeoCodeAddressModel.Envelope envelope)
		{
			GeoCodeAddressModel.GeocodeAddressNonParsed result = envelope.Body.GeocodeAddressNonParsed;

			Assert.AreEqual(expected.apiKey, result.apiKey);
			Assert.AreEqual(expected.city, result.city);
			Assert.AreEqual(expected.censusYear, result.censusYear);
			Assert.AreEqual(expected.shouldCalculateCensus, result.shouldCalculateCensus);
			Assert.AreEqual(expected.shouldNotStoreTransactionDetails, result.shouldNotStoreTransactionDetails);
			Assert.AreEqual(expected.shouldReturnReferenceGeometry, result.shouldReturnReferenceGeometry);
			Assert.AreEqual(expected.state, result.state);
			Assert.AreEqual(expected.streetAddress, result.streetAddress);
			Assert.AreEqual(expected.version, result.version);
			Assert.AreEqual(expected.zip, result.zip);
		}

		private string CleanString(string source)
		{
			return Regex.Replace(source, @"\t|\n|\r|[\s]{2,}", string.Empty);
		}

		[Test]
		public void Deserialize()
		{
			XmlSerializer serializer = new XmlSerializer(typeof (GeoCodeAddressModel.Envelope));
			byte[] buffer = Encoding.UTF8.GetBytes(GeoCodeAddressModelSample.StringSample);

			using (var stream = new MemoryStream(buffer))
			{
				var rawResult = serializer.Deserialize(stream);
				Assert.IsNotNull(rawResult);
				Assert.AreEqual(typeof(GeoCodeAddressModel.Envelope), rawResult.GetType());

				GeoCodeAddressModel.Envelope envelope = (GeoCodeAddressModel.Envelope)rawResult;
				GeoCodeAddressModel.GeocodeAddressNonParsed expected = GeoCodeAddressModelSample.TypedSample.Body.GeocodeAddressNonParsed;
				CheckResult(expected, envelope);
			}
		}

		[Test]
		public void Serialize()
		{
			// I don't like the raw string comparison (too fragile) but that's the behavior we need to check; re-visit this in the future
			string result;
			XmlSerializer serializer = new XmlSerializer(typeof(GeoCodeAddressModel.Envelope));

			using (var memoryStream = new MemoryStream())
			{
				using (var streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(false)))
				{
					serializer.Serialize(streamWriter, GeoCodeAddressModelSample.TypedSample);
					result = Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
				}
			}

			Assert.IsFalse(string.IsNullOrEmpty(result));

			result = CleanString(result);
			string strippedSample = CleanString(GeoCodeAddressModelSample.StringSample);
			Assert.AreEqual(strippedSample, result);
		}

		[Test]
		public void InvalidBooleanValue()
		{
			const string invalidBoolean = "foo";
			GeoCodeAddressModel.GeocodeAddressNonParsed sample = new GeoCodeAddressModel.GeocodeAddressNonParsed();
			
			sample.shouldCalculateCensus = invalidBoolean;
			sample.shouldNotStoreTransactionDetails = invalidBoolean;
			sample.shouldReturnReferenceGeometry = invalidBoolean;

			Assert.AreEqual(string.Empty, sample.shouldCalculateCensus);
			Assert.AreEqual(string.Empty, sample.shouldReturnReferenceGeometry);
			Assert.AreEqual(string.Empty, sample.shouldNotStoreTransactionDetails);
		}

		[Test]
		public void DefaultValues()
		{
			GeoCodeAddressModel.GeocodeAddressNonParsed sample = new GeoCodeAddressModel.GeocodeAddressNonParsed();

			Assert.AreEqual("TwoThousandTen", sample.censusYear);
			Assert.AreEqual("false", sample.shouldCalculateCensus);
			Assert.AreEqual("false", sample.shouldReturnReferenceGeometry);
			Assert.AreEqual("false", sample.shouldNotStoreTransactionDetails);
		}
	}
}