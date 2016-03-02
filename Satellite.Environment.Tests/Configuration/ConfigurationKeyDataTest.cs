using NUnit.Framework;
using Satellite.Envrionment.Configuration;
using Satellite.Envrionment.Configuration.Attributes;

namespace Satellite.Environment.Tests.Configuration
{
	[TestFixture]
	public class ConfigurationKeyDataTest
	{
		private ConfigurationKeyData ConfigurationKeyData { get; set; }

		public class TestType
		{
			public const string VariableNameOne = "one";
			[StaticKey]
			public const string StaticVariable = "some static value";
			[EnvironmentKey]
			public const string EnvironmentVariable = "differs per environment";
		}

		private static void CheckEnvironmentEntry(string result, string expectedVariableName)
		{
			Assert.IsNotNull(result);
			Assert.AreEqual(expectedVariableName, result);
		}

		[SetUp]
		public void SetUp()
		{
			ConfigurationKeyData = new ConfigurationKeyData(typeof(TestType));
		}

		[Test]
		public void ContainsEntry()
		{
			string result = ConfigurationKeyData.LookupConfigurationKey(TestType.EnvironmentVariable);
			CheckEnvironmentEntry(result, TestType.EnvironmentVariable);
		}

		[Test]
		public void DoesNotContainEntry()
		{
			string result = ConfigurationKeyData.LookupConfigurationKey("foo");

			Assert.IsNotNull(result);
			Assert.AreEqual(string.Empty, result);
		}

		[Test]
		public void DefaultsToEnvironmentKey()
		{
			string result = ConfigurationKeyData.LookupConfigurationKey(TestType.VariableNameOne);
			CheckEnvironmentEntry(result, TestType.VariableNameOne);
		}

		[Test]
		public void StaticKey()
		{
			string result = ConfigurationKeyData.LookupConfigurationKey(TestType.StaticVariable);
			Assert.AreEqual(TestType.StaticVariable, result);
		}
	}
}
