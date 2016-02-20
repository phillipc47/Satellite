namespace Satellite.ServiceClient.Model
{
	public class GeoCodeAddressModel
	{
		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
		public class Envelope
		{
			public EnvelopeBody Body { get; set; }
		}


		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
		public class EnvelopeBody
		{
			[System.Xml.Serialization.XmlElementAttribute(Namespace = "https://geoservices.tamu.edu/")]
			public GeocodeAddressNonParsed GeocodeAddressNonParsed { get; set; }
		}


		[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://geoservices.tamu.edu/")]
		[System.Xml.Serialization.XmlRootAttribute(Namespace = "https://geoservices.tamu.edu/", IsNullable = false)]
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
				censusYear = "2010";
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
