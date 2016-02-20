using Satellite.ServiceClient.Model;

namespace Satellite.ServiceClient.Tests.Data
{
	internal static class GeoCodeAddressModelSample
	{
		public static string StringSample;
		public static GeoCodeAddressModel.Envelope TypedSample;

		static GeoCodeAddressModelSample()
		{
			StringSample = @"<?xml version=""1.0"" encoding=""utf-8""?>
				<Envelope xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns=""http://schemas.xmlsoap.org/soap/envelope/"">
					<Body>
						<GeocodeAddressNonParsed xmlns=""https://geoservices.tamu.edu/"">
							<streetAddress>123 Main Street</streetAddress>
							<city>someCity</city>
							<state>Texas</state>
							<zip>99011</zip>
							<apiKey>SomeAPIKey</apiKey>
							<version>1.1</version>
							<shouldCalculateCensus>true</shouldCalculateCensus>
							<censusYear>2010</censusYear>
							<shouldReturnReferenceGeometry>true</shouldReturnReferenceGeometry>
							<shouldNotStoreTransactionDetails>true</shouldNotStoreTransactionDetails>
						</GeocodeAddressNonParsed>
					</Body>
				</Envelope>";

			TypedSample = new GeoCodeAddressModel.Envelope
			{
				Body = new GeoCodeAddressModel.EnvelopeBody
				{
					GeocodeAddressNonParsed = new GeoCodeAddressModel.GeocodeAddressNonParsed()
					{
						apiKey = "SomeAPIKey",
						censusYear = "2010",
						city = "someCity",
						shouldCalculateCensus = "true",
						shouldNotStoreTransactionDetails = "true",
						shouldReturnReferenceGeometry = "true",
						state = "Texas",
						streetAddress = "123 Main Street",
						version = "1.1",
						zip = "99011"
					}
				}
			};
		}
	}
}
