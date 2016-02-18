using System.ComponentModel.DataAnnotations;
using Microsoft.SqlServer.Server;

namespace Satellite.Models.GeoCode
{
	public class GeoCodeViewModel
	{
		[Required]
		public int PostalCode { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }

	}
}