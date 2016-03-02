using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Satellite.ServiceClient.Serialization
{
	public class BasicSerializer<T> : IBasicSerializer<T>
	{
		private string CleanString(string source)
		{
			return Regex.Replace(source, @"\t|\n|\r|[\s]{2,}|\?", string.Empty);
		}

		private bool IsFitForInteger(long valueToCheck)
		{
			return (valueToCheck >= int.MinValue && valueToCheck <= int.MaxValue);
		}

		private void AddSoapNamespace(XmlSerializerNamespaces namespaces)
		{
			namespaces.Add("soap", "http://schemas.xmlsoap.org/soap/envelope/");
		}

		private XmlSerializerNamespaces CreateSoapNamespace()
		{
			XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
			AddSoapNamespace(namespaces);
			return namespaces;
		}

		private XmlWriterSettings CreateSettings()
		{
			return new XmlWriterSettings()
			{
				OmitXmlDeclaration = true,
				ConformanceLevel = ConformanceLevel.Fragment,
				Indent = true
			};
		}

		public T DeSerialize(string data)
		{
			XmlSerializer serializer = new XmlSerializer(typeof (T));
			using (StringReader reader = new StringReader(data))
			{
				return (T)serializer.Deserialize(reader);
			}
		}

		public string Serialze(T data)
		{
			XmlSerializerNamespaces namespaces = CreateSoapNamespace();

			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (var memoryStream = new MemoryStream())
			{
				using (var streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(false)))
				{
					var settings = CreateSettings();

					using (var xmlWriter = XmlWriter.Create(streamWriter, settings))
					{
						// Workaround to WriteStartDocument issue with ConformanceLevel of Fragment
						xmlWriter.WriteWhitespace(string.Empty);
						serializer.Serialize(xmlWriter, data, namespaces);

						if (IsFitForInteger(memoryStream.Length))
						{
							string encode = Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
							return CleanString(encode);
						}
					}
				}
			}
			return string.Empty;
		}
	}
}
