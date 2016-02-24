using System.Xml.Serialization;

namespace Satellite.ServiceClient.Model
{
	public class GeoCodeAddressResponseModel
	{
		[XmlType(AnonymousType=true, Namespace=XmlNamespaces.GeoServices)]
		[XmlRoot(Namespace=XmlNamespaces.GeoServices, IsNullable = false)]
		public class WebServiceGeocodeQueryResultSet
		{
			public decimal Version { get; set; }


			public string TransactionId { get; set; }


			public QueryStatusCode QueryStatusCode { get; set; }


			public WebServiceGeocodeQueryResultSetWebServiceGeocodeQueryResults WebServiceGeocodeQueryResults { get; set; }


			public string QueryStatusCodeName { get; set; }


			public byte QueryStatusCodeValue { get; set; }

			public decimal TimeTaken { get; set; }

			public bool ExceptionOccurred { get; set; }


			[XmlType(AnonymousType=true, Namespace=XmlNamespaces.GeoServices)]
			public class WebServiceGeocodeQueryResultSetWebServiceGeocodeQueryResults
			{
				public WebServiceGeocodeQueryResultSetWebServiceGeocodeQueryResultsWebServiceGeocodeQueryResult WebServiceGeocodeQueryResult { get; set; }
			}


			[XmlType(AnonymousType=true, Namespace=XmlNamespaces.GeoServices)]
			public class WebServiceGeocodeQueryResultSetWebServiceGeocodeQueryResultsWebServiceGeocodeQueryResult
			{
				public string TransactionId { get; set; }
				public decimal Latitude { get; set; }
				public decimal Longitude { get; set; }
				public decimal Version { get; set; }
				public string Quality { get; set; }
			}
		}
	}
}
