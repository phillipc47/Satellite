using Moq;
using NUnit.Framework;
using Satellite.Envrionment;
using Satellite.Envrionment.Configuration;

namespace Satellite.Environment.Tests
{
	[TestFixture]
	public class ApplicationConfigurationTest
	{
		private Mock<IConfigurationManager> ConfigurationManager { get; set; }
		private Mock<IConfigurationKeyData> ConfigurationKeyData { get; set; }
		private ApplicationConfiguration ApplicationConfiguration { get; set; }

		private static string BuildKey(string variableName)
		{
			return variableName;
		}

		private string ExpectReadEntry(string variableName, string expectedValue)
		{
			string key = BuildKey(variableName);
			ConfigurationManager.Setup(configurationManager => configurationManager.ReadEntry(key)).Returns(expectedValue);
			return key;
		}

		private void ExpectLookupConfigurationKey(string keyName, string expectedResult)
		{
			ConfigurationKeyData.Setup(configurationKeyData => configurationKeyData.LookupConfigurationKey(keyName))
				.Returns(expectedResult);
		}

		[SetUp]
		public void SetUp()
		{
			ConfigurationManager = new Mock<IConfigurationManager>();
			ConfigurationKeyData = new Mock<IConfigurationKeyData>();

			ApplicationConfiguration = new ApplicationConfiguration(ConfigurationManager.Object)
			{
				ConfigurationKeyData = ConfigurationKeyData.Object
			};
		}

		[Test]
		public void SingleEntryDelegates()
		{
			const string expectedResult = "foo";
			const string configurationKey = ApplicationConfiguration.VariableNames.GeoCodeService;

			string key = ExpectReadEntry(configurationKey, expectedResult);
			ExpectLookupConfigurationKey(configurationKey, key);

			string result = ApplicationConfiguration.GeoCodeServiceUrl;

			Assert.AreSame(expectedResult, result);
		}
	}
}
