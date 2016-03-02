using Satellite.ServiceClient.Model;

namespace Satellite.Data.Test
{
	public static class GeoCodeAddressModelResponseSample
	{
		public static string StringSample;
		public static GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet TypedSample;

		static GeoCodeAddressModelResponseSample()
		{
			StringSample =
				@"<?xml version=""1.0"" encoding=""utf-8""?>
					<WebServiceGeocodeQueryResultSet xmlns=""https://geoservices.tamu.edu/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
						<Version>4.01</Version>
						<TransactionId>Transaction Id</TransactionId>
						<QueryStatusCode>Success</QueryStatusCode>
						<WebServiceGeocodeQueryResults>
							<WebServiceGeocodeQueryResult>
								<TransactionId>Transaction Id</TransactionId>
								<Latitude>37.4220296330465</Latitude>
								<Longitude>-122.084331436251</Longitude>
								<Version>0</Version>
								<Quality>Quality</Quality>
							</WebServiceGeocodeQueryResult>
						</WebServiceGeocodeQueryResults>
						<QueryStatusCodeValue>0</QueryStatusCodeValue>
						<TimeTaken>0.12345</TimeTaken>
						<ExceptionOccurred>false</ExceptionOccurred>
					</WebServiceGeocodeQueryResultSet>";

			TypedSample = new GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet()
			{
				QueryStatusCode = QueryStatusCode.Success,
				ExceptionOccurred = false,
				TimeTaken = new decimal(0.12345),
				TransactionId = "Transaction Id",
				Version = new decimal(4.01),
				WebServiceGeocodeQueryResults =
					new GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet.
						WebServiceGeocodeQueryResultSetWebServiceGeocodeQueryResults()
					{
						WebServiceGeocodeQueryResult =
							new GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet.
								WebServiceGeocodeQueryResultSetWebServiceGeocodeQueryResultsWebServiceGeocodeQueryResult()
							{

								Latitude = new decimal(37.4220296330465),
								Longitude = new decimal(-122.084331436251),
								Quality = "Quality",
								TransactionId = "Transaction Id"
							}
					}
			};
		}
	}
}