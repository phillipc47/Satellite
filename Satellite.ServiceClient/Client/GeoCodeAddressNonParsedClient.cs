using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Satellite.ServiceClient.Model;
using Satellite.ServiceClient.Serialize;

namespace Satellite.ServiceClient.Client
{
	public class GeoCodeAddressNonParsedClient
	{
		public HttpWebRequest CreateWebRequest()
		{
			//string url =
			//	@"https://geoservices.tamu.edu/Services/Geocode/WebService/GeocoderService_V04_01.asmx?op=GeocodeAddressNonParsed";
			string url =
				@"https://geoservices.tamu.edu/Services/Geocode/WebService/GeocoderService_V04_01.asmx";

			//"https://geoservices.tamu.edu/GeocodeAddressNonParsed"

			HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
			webRequest.Headers.Add(@"SOAPAction: ""https://geoservices.tamu.edu/GeocodeAddressNonParsed""");
													 
			webRequest.ContentType = "text/xml;charset=\"utf-8\"";
			webRequest.Accept = "text/xml";
			webRequest.Method = "POST";
			webRequest.ProtocolVersion = HttpVersion.Version11;

			return webRequest;
		}

		public void SendData()
		{
			GeoCodeAddressModel.Envelope envelope = new GeoCodeAddressModel.Envelope();
			GeoCodeAddressModel.EnvelopeBody envelopeBody = new GeoCodeAddressModel.EnvelopeBody();
			GeoCodeAddressModel.GeocodeAddressNonParsed addressData = new GeoCodeAddressModel.GeocodeAddressNonParsed
			{
				//TODO: Config API Key and encrypt
				apiKey = "",
				state = "California",
				streetAddress = "1600 Amphitheatre Pkwy",
				city = "Mountain View",
				zip = "94043"
			};

			envelopeBody.GeocodeAddressNonParsed = addressData;
			envelope.Body = envelopeBody;

			BasicSerializer<GeoCodeAddressModel.Envelope> s = new BasicSerializer<GeoCodeAddressModel.Envelope>();
			string sample = s.Serialze(envelope);
			
			HttpWebRequest request = CreateWebRequest();

			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] data = encoding.GetBytes(sample);
			request.ContentLength = data.Length;

			//XmlDocument soapEnvelopeXml = new XmlDocument();
			//soapEnvelopeXml.LoadXml(sample);

			using (Stream stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
				stream.Close();
			}

			using (WebResponse response = request.GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
				{
					string soapResult = streamReader.ReadToEnd();
					Console.WriteLine(soapResult);
				}
			}
		}
	}
}
