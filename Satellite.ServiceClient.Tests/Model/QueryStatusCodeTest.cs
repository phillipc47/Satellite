using System;
using System.Collections.Generic;
using NUnit.Framework;
using Satellite.ServiceClient.Model;

namespace Satellite.ServiceClient.Tests.Model
{
	[TestFixture]
	public class QueryStatusCodeTest
	{
		private void CheckValue(QueryStatusCode code, int expectedValue)
		{
			Assert.AreEqual(expectedValue, (int)code);
		}

		[Test]
		public void NumberOfStatusCodes()
		{
			Assert.AreEqual(18, Enum.GetValues(typeof(QueryStatusCode)).Length);
		}

		[Test]
		public void CheckValues()
		{
			int expectedValue = 0;
			IDictionary<QueryStatusCode, int> expectedValues = new Dictionary<QueryStatusCode, int>()
			{
				{ QueryStatusCode.Unknown, expectedValue++ },
				{ QueryStatusCode.Success, expectedValue++ },
				{ QueryStatusCode.Failure, expectedValue++ },
				{ QueryStatusCode.InternalError, expectedValue++ },
				{ QueryStatusCode.APIKeyError, expectedValue++ },
				{ QueryStatusCode.APIKeyMissing, expectedValue++ },
				{ QueryStatusCode.APIKeyInvalid, expectedValue++ },
				{ QueryStatusCode.APIKeyNotActivated, expectedValue++ },
				{ QueryStatusCode.NonProfitError, expectedValue++ },
				{ QueryStatusCode.NonProfitNotConfirmed, expectedValue++ },
				{ QueryStatusCode.QueryError, expectedValue++ },
				{ QueryStatusCode.QueryParameterMissing, expectedValue++ },
				{ QueryStatusCode.QueryRestrictedCoverage, expectedValue++ },
				{ QueryStatusCode.QuotaExceededError, expectedValue++ },
				{ QueryStatusCode.QuotaExceededAnonymous, expectedValue++ },
				{ QueryStatusCode.QuotaExceededPaid, expectedValue++ },
				{ QueryStatusCode.VersionInvalid, expectedValue++ },
				{ QueryStatusCode.VersionMissing, expectedValue }
			};

			var queryStatusCodes = Enum.GetValues(typeof(QueryStatusCode));
			foreach (QueryStatusCode queryStatusCode in queryStatusCodes)
			{
				Assert.IsTrue(expectedValues.ContainsKey(queryStatusCode));
				CheckValue(queryStatusCode, expectedValues[queryStatusCode]);				
			}
		}
	}
}
