using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using Satellite.Envrionment.Configuration;

namespace Satellite.Environment.Tests.Configuration
{
	[TestFixture]
    public class ApplicationConfigurationManagerTest
    {
		private static class KeyNames
		{
			public const string NoSuchKey = "foo";
			public const string Single = "TestSingleKey";
			public const string Multiple = "TestMultipleDefaultKey";
			public const string MultiplePipeSeparator = "TestMultipleSeparatorKey";
		}

		private static class ConnectionStrings
		{
			public const string NoSuchKey = "foo";
			public const string EmptyValue = "empty";
			public const string Valid = "SecurityContext";
		}

		private IConfigurationManager Configuration { get; set; }
		private IDictionary<string, object> ExpectedKeyValueMap { get; set; }

		private bool ContainsAllItems(IList<string> firstList, List<string> secondList)
		{
			return !secondList.Except(firstList).Any();
		}

		private bool KeyExists(string key)
		{
			return ConfigurationManager.AppSettings[key] != null;
		}

		private bool ConnectionExists(string connectionString)
		{
			return ConfigurationManager.ConnectionStrings[connectionString] != null;
		}

		[OneTimeSetUp]
		public void Initialize()
		{
			ExpectedKeyValueMap = new Dictionary<string, object>
			{
				{KeyNames.Single, "someValue"},
				{KeyNames.Multiple, new List<string>() {"a", "b", "c"}},
				{KeyNames.MultiplePipeSeparator, new List<string>() {"d", "e", "f"}}
			};
		}

		[SetUp]
		public void SetUp()
		{
			this.Configuration = new ApplicationConfigurationManager();
		}

		[Test]
		public void SingleValueNullKey()
		{
			Assert.Throws<ArgumentNullException>(() => Configuration.ReadEntry(null));
		}

		[Test]
		public void SingleNoSuchKey()
		{
			Assert.False(KeyExists(KeyNames.NoSuchKey));
			Assert.Throws<ApplicationException>(() => Configuration.ReadEntry(KeyNames.NoSuchKey));
		}

		[Test]
		public void MultipleValueNullKey()
		{
			Assert.Throws<ArgumentNullException>(() => Configuration.ReadEntries(null));
		}

		[Test]
		public void MultipleValuesNoSuchKey()
		{
			Assert.False(KeyExists(KeyNames.NoSuchKey));
			Assert.Throws<ApplicationException>(() => Configuration.ReadEntries(KeyNames.NoSuchKey));
		}

		[Test]
		public void SingleKey()
		{
			Assert.True(KeyExists(KeyNames.Single));
			string expectedValue = (string)ExpectedKeyValueMap[KeyNames.Single];

			string result = Configuration.ReadEntry(KeyNames.Single);

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void MultipleKeyDefaultSeparator()
		{
			Assert.True(KeyExists(KeyNames.Multiple));
			List<string> expected = (List<string>)ExpectedKeyValueMap[KeyNames.Multiple];

			IList<string> result = Configuration.ReadEntries(KeyNames.Multiple);

			Assert.AreEqual(expected.Count, result.Count);
			Assert.True(ContainsAllItems(result, expected));
		}

		[Test]
		public void MultipleKeyPipeSeparator()
		{
			Assert.True(KeyExists(KeyNames.MultiplePipeSeparator));
			List<string> expected = (List<string>)ExpectedKeyValueMap[KeyNames.MultiplePipeSeparator];

			IList<string> result = Configuration.ReadEntries(KeyNames.MultiplePipeSeparator, '|');

			Assert.AreEqual(expected.Count, result.Count);
			Assert.True(ContainsAllItems(result, expected));
		}
	}
}
