using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;
using NUnit.Framework;
using Satellite.ServiceClient.Client;

namespace Satellite.ServiceClient.Tests.Client
{
	[TestFixture]
	public class GeoCodeAddressNonParsedClientTest
	{
		[Test]
		public void DoIt()
		{
			GeoCodeAddressNonParsedClient client = new GeoCodeAddressNonParsedClient();
			client.SendData();
		}
	}
}
