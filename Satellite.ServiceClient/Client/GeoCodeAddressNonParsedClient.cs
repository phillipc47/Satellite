using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Satellite.Envrionment;
using Satellite.ServiceClient.Model;
using Satellite.ServiceClient.Serialize;

namespace Satellite.ServiceClient.Client
{
	public class GeoCodeAddressNonParsedClient
	{
		private static class RequestValues
		{
			public const string SoapAction = @"SOAPAction: ""https://geoservices.tamu.edu/GeocodeAddressNonParsed""";
			public const string Method = "POST";
			public const string Xml = "text/xml";
			public const string ContentType = Xml + ";charset=\"utf-8\"";
		}

		private IApplicationConfiguration ApplicationConfiguration { get; set; }

		public GeoCodeAddressNonParsedClient(IApplicationConfiguration applicationConfiguration)
		{
			ApplicationConfiguration = applicationConfiguration;
		}

		public HttpWebRequest CreateWebRequest()
		{
			HttpWebRequest webRequest = WebRequest.CreateHttp(ApplicationConfiguration.GeoCodeServiceUrl);
			webRequest.Headers.Add(RequestValues.SoapAction);

			webRequest.ContentType = RequestValues.ContentType;
			webRequest.Accept = RequestValues.Xml;
			webRequest.Method = RequestValues.Method;
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
				apiKey = ApplicationConfiguration.ApiKey,
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
