using System.Xml.Serialization;
using Satellite.ServiceClient.Model;

namespace Satellite.Data.Test
{
	[XmlType(AnonymousType = true)]
	[XmlRoot(Namespace = XmlNamespaces.SoapNamespace, IsNullable = false)]
	public class Note
	{
		public string to { get; set; }
		public string from { get; set; }
		public string heading { get; set; }
		public string body { get; set; }
	}

	[XmlType(AnonymousType = true)]
	[XmlRoot(Namespace = "", IsNullable = false)]
	public class Notes
	{
		/// <remarks/>
		[XmlElement("note")]
		public Note[] notes { get; set; }
	}
}
