﻿using System.Xml.Serialization;

namespace Satellite.ServiceClient.Tests.Data
{
	[XmlType(AnonymousType = true)]
	[XmlRootAttribute(Namespace = "", IsNullable = false)]
	public class Note
	{
		public string to { get; set; }
		public string from { get; set; }
		public string heading { get; set; }
		public string body { get; set; }
	}

	[XmlTypeAttribute(AnonymousType = true)]
	[XmlRootAttribute(Namespace = "", IsNullable = false)]
	public class Notes
	{
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("note")]
		public Note[] notes { get; set; }
	}
}
