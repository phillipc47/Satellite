using System.IO;
using System.Net;
using System.Text;
using Satellite.Envrionment;
using Satellite.ServiceClient.Model;
using Satellite.ServiceClient.Serialization;

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

		private IBasicSerializer<GeoCodeAddressModel.Envelope> RequestSerializer { get; set; }
		private IBasicSerializer<GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet> ResponseSerializer { get; set; }



		private GeoCodeAddressModel.Envelope CreateEnvelope(GeoCodeAddressModel.GeocodeAddressNonParsed addressData)
		{
			addressData.apiKey = ApplicationConfiguration.ApiKey;
			GeoCodeAddressModel.EnvelopeBody envelopeBody = new GeoCodeAddressModel.EnvelopeBody
			{
				GeocodeAddressNonParsed = addressData
			};

			GeoCodeAddressModel.Envelope envelope = new GeoCodeAddressModel.Envelope { Body = envelopeBody };
			return envelope;
		}

		public GeoCodeAddressNonParsedClient(IApplicationConfiguration applicationConfiguration, IBasicSerializer<GeoCodeAddressModel.Envelope> requestSerializer, IBasicSerializer<GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet> responseSerializer)
		{
			ApplicationConfiguration = applicationConfiguration;
			RequestSerializer = requestSerializer;
			ResponseSerializer = responseSerializer;
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

		private byte[] CreateContent(GeoCodeAddressModel.Envelope envelope)
		{
			ASCIIEncoding encoding = new ASCIIEncoding();
			byte[] data = encoding.GetBytes(RequestSerializer.Serialze(envelope));
			return data;
		}

		private bool SendData(HttpWebRequest request, byte[] content)
		{
			try
			{
				using (Stream stream = request.GetRequestStream())
				{
					stream.Write(content, 0, content.Length);
					stream.Close();
					return true;
				}
			}
			catch
			{
				//TODO: Decorate
				return false;
			}
		}

		private string ReadContent(HttpWebResponse response)
		{
			Stream responseStream = response.GetResponseStream();
			if (responseStream == null)
			{
				return string.Empty;
			}

			try
			{
				using (StreamReader streamReader = new StreamReader(responseStream))
				{
					return streamReader.ReadToEnd();
				}
			}
			catch
			{
				//TODO: Decorate
				return string.Empty;
			}
		}

		private string ExtractResponse(HttpWebRequest request)
		{
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				if (response.StatusCode != HttpStatusCode.OK)
				{
					//TODO: Decorate
					return string.Empty;
				}

				return ReadContent(response);
			}
		}

		public bool SendData(GeoCodeAddressModel.GeocodeAddressNonParsed addressData, out GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet result)
		{
			result = null;

			var envelope = CreateEnvelope(addressData);
			HttpWebRequest request = CreateWebRequest();
			byte[] content = CreateContent(envelope);

			if (SendData(request, content))
			{
				string response = ExtractResponse(request);
				if (!string.IsNullOrEmpty(response))
				{
					result = ResponseSerializer.DeSerialize(response);
					return result.QueryStatusCode == QueryStatusCode.Success;
				}
			}

			return false;
		}
	}
}
