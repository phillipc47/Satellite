using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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

		public string Simple2(string baseUrl)
		{
			// Request the login page
			Uri url = new Uri(baseUrl);
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			request.Method = "GET";
			request.AllowAutoRedirect = false;
			// Exception raised below
			HttpWebResponse response = request.GetResponse() as HttpWebResponse;

			return string.Empty;
		}

		public string Simple(string baseUrl)
		{
			HttpWebRequest myRequest = CreateWebRequest();

			//HttpWebRequest myRequest = WebRequest.CreateHttp(ApplicationConfiguration.GeoCodeServiceUrl);
			//webRequest.Headers.Add(RequestValues.SoapAction);

			//webRequest.ContentType = RequestValues.ContentType;
			//webRequest.Accept = RequestValues.Xml;
			//webRequest.Method = RequestValues.Method;
			//webRequest.ProtocolVersion = HttpVersion.Version11;

			var encoding = new ASCIIEncoding();
			var postData = "SomeData";
			//var myRequest = (HttpWebRequest)WebRequest.Create(baseUrl);
			myRequest.Method = "POST";
			myRequest.ContentType = "application/x-www-form-urlencoded";
			myRequest.ContentLength = postData.Length;
			var newStream = myRequest.GetRequestStream();
			byte[] buffer = Encoding.ASCII.GetBytes(postData);
			newStream.Write(buffer, 0, buffer.Length);
			newStream.Close();

			var response = myRequest.GetResponse();
			var responseStream = response.GetResponseStream();
			var responseReader = new StreamReader(responseStream);
			var result = responseReader.ReadToEnd();

			return result;
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
				//TODO: Log Exception
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
				//TODO: Logging
				return string.Empty;
			}
		}

		private string ExtractResponse(HttpWebRequest request)
		{
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				if (response.StatusCode != HttpStatusCode.OK)
				{
					//TODO Logging
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
