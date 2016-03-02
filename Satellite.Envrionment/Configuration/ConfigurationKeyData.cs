using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Satellite.Envrionment.Configuration.Attributes;

namespace Satellite.Envrionment.Configuration
{
	public class ConfigurationKeyData : IConfigurationKeyData
	{
		// Poor Man's caching; there are plug ins out there, but, keeping it simple for now
		private IDictionary<string, string> KeyCache { get; }

		private string BuildEnvironmentConfigurationKey(string variableName)
		{
			return $"{variableName}";
		}

		private string BuildStaticConfigurationKey(string variableName)
		{
			return $"{variableName}";
		}

		private List<FieldInfo> ReadConstants(Type type)
		{
			FieldInfo[] fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

			return fieldInfos.Where(fieldInfo => fieldInfo.IsLiteral && !fieldInfo.IsInitOnly).ToList();
		}

		private void PopulateKeys(List<FieldInfo> constants)
		{
			foreach (FieldInfo constant in constants)
			{
				string stringValue = constant.GetRawConstantValue() as string;
				if (stringValue != null)
				{
					string configurationKey = BuildConfigurationKey(constant, stringValue);
					KeyCache.Add(stringValue, configurationKey);
				}
			}
		}

		private string BuildConfigurationKey(FieldInfo constant, string variableName)
		{
			// Can make this polymorphic if the need arises
			if (constant.GetCustomAttribute<StaticKey>() != null)
			{
				return BuildStaticConfigurationKey(variableName);
			}

			return BuildEnvironmentConfigurationKey(variableName);
		}

		public ConfigurationKeyData(Type type)
		{
			KeyCache = new Dictionary<string, string>();

			List<FieldInfo> constants = ReadConstants(type);
			PopulateKeys(constants);
		}

		public string LookupConfigurationKey(string keyName)
		{
			if (KeyCache.ContainsKey(keyName))
			{
				return KeyCache[keyName];
			}

			return string.Empty;
		}
	}
}
