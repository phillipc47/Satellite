using System.Xml.Serialization;

namespace Satellite.ServiceClient.Model
{
	public class GeoCodeAddressModel
	{
		[XmlType(AnonymousType = true, Namespace = XmlNamespaces.SoapNamespace)]
		[XmlRoot(Namespace = XmlNamespaces.SoapNamespace, IsNullable = false)]
		public class Envelope
		{
			
			public EnvelopeBody Body { get; set; }
		}


		[XmlTypeAttribute(AnonymousType = true, Namespace = XmlNamespaces.SoapNamespace)]
		public class EnvelopeBody
		{
			[XmlElementAttribute(Namespace = XmlNamespaces.GeoServices)]
			public GeocodeAddressNonParsed GeocodeAddressNonParsed { get; set; }
		}


		[XmlTypeAttribute(AnonymousType = true, Namespace = XmlNamespaces.GeoServices)]
		[XmlRootAttribute(Namespace = XmlNamespaces.GeoServices, IsNullable = false)]
		public class GeocodeAddressNonParsed
		{
			private string internalShouldCalculateCensus;
			private string internalShouldReturnReferenceGeometry;
			private string internalShouldNotStoreTransactionDetails;

			private bool isValidBoolean(string value)
			{
				bool result;
				return bool.TryParse(value, out result);
			}

			private string scrubBoolean(string value)
			{
				return isValidBoolean(value) ? value : string.Empty;
			}

			public GeocodeAddressNonParsed()
			{
				censusYear = "TwoThousandTen";
				shouldNotStoreTransactionDetails = "false";
				shouldCalculateCensus = "false";
				shouldReturnReferenceGeometry = "false";
				version = "4.01";
			}

			public string streetAddress { get; set; }
			public string city { get; set; }
			public string state { get; set; }
			public string zip { get; set; }
			public string apiKey { get; set; }
			public string version { get; set; }

			public string shouldCalculateCensus
			{
				get { return internalShouldCalculateCensus; }
				set { internalShouldCalculateCensus = scrubBoolean(value); }
			}

			public string censusYear { get; set; }

			public string shouldReturnReferenceGeometry
			{
				get { return internalShouldReturnReferenceGeometry; }
				set { internalShouldReturnReferenceGeometry = scrubBoolean(value); }
			}

			public string shouldNotStoreTransactionDetails
			{
				get { return internalShouldNotStoreTransactionDetails; }
				set { internalShouldNotStoreTransactionDetails = scrubBoolean(value); }
			}
		}
	}
}
