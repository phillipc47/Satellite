using System.Collections.Generic;

namespace Satellite.Envrionment.Configuration
{
	public interface IConfigurationManager
	{
		string ReadEntry(string key);
		IList<string> ReadEntries(string key, char separator = ',');
	}
}
