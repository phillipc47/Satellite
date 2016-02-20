using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Satellite.ServiceClient.Model;

namespace Satellite.ServiceClient
{
	public class BasicSerializer<T>
	{
		private string CleanString(string source)
		{
			return Regex.Replace(source, @"\t|\n|\r|[\s]{2,}|\?", string.Empty);
		}

		private bool IsFitForInteger(long valueToCheck)
		{
			return (valueToCheck >= int.MinValue && valueToCheck <= int.MaxValue);
		}

		public string Serialze(T data)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));

			using (var memoryStream = new MemoryStream())
			{
				using (var streamWriter = new StreamWriter(memoryStream, new UTF8Encoding(false)))
				{
					serializer.Serialize(streamWriter, data);

					if (IsFitForInteger(memoryStream.Length))
					{
						string encode = Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
						return CleanString(encode);
					}
				}
			}
			return string.Empty;
		}
	}
}
