using Satellite.Envrionment.Configuration;
using Satellite.Envrionment.Configuration.Attributes;

namespace Satellite.Envrionment
{
	public class ApplicationConfiguration : IApplicationConfiguration
	{
		private IConfigurationManager ConfigurationManager { get; }
		//TODO: When injection is configured make private
		public IConfigurationKeyData ConfigurationKeyData { get; set; }

		public ApplicationConfiguration(IConfigurationManager configurationManager)
		{
			this.ConfigurationManager = configurationManager;
			//TODO: When needed, extract construction for better testing and isolation
			this.ConfigurationKeyData = new ConfigurationKeyData(typeof(VariableNames));
		}

		public static class VariableNames
		{
			[StaticKey]
			public const string GeoCodeService = "TexasGeoCodeServiceUrl";
			[StaticKey]
			public const string ApiKey = "TexasGeoCodeAPIKey";
		}

		public string GeoCodeServiceUrl => ConfigurationManager.ReadEntry(ConfigurationKeyData.LookupConfigurationKey(VariableNames.GeoCodeService));
		public string ApiKey => ConfigurationManager.ReadEntry(ConfigurationKeyData.LookupConfigurationKey(VariableNames.ApiKey));

	}
}
