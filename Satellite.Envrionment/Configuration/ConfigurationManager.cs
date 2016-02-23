using System;
using System.Collections.Generic;
using System.Configuration;

namespace Satellite.Envrionment.Configuration
{
	public class ApplicationConfigurationManager : IConfigurationManager
	{
		private void ValidateKey(string key, out string rawValue)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key", "Attempting to read a configuration entry without a valid key");
			}

			rawValue = ConfigurationManager.AppSettings[key];
			if (string.IsNullOrWhiteSpace(rawValue))
			{
				throw new ApplicationException("Unable to read a configuration entry for the specified key [" + key + "]");
			}
		}

		public string ReadEntry(string key)
		{
			string rawValue;
			ValidateKey(key, out rawValue);
			return rawValue.Trim();
		}

		public IList<string> ReadEntries(string key, char separator = ',')
		{
			string rawValue;
			ValidateKey(key, out rawValue);

			string[] splitValues = rawValue.Split(separator);

			return new List<string>(splitValues);
		}
	}
}
