using System;

namespace Satellite.ServiceClient.Model
{
	[Serializable()]
	[System.Xml.Serialization.XmlType(Namespace=XmlNamespaces.GeoServices)]
	public enum QueryStatusCode
	{
		Unknown,
		Success,
		Failure,
		InternalError,
		APIKeyError,
		APIKeyMissing,
		APIKeyInvalid,
		APIKeyNotActivated,
		NonProfitError,
		NonProfitNotConfirmed,
		QueryError,
		QueryParameterMissing,
		QueryRestrictedCoverage,
		QuotaExceededError,
		QuotaExceededAnonymous,
		QuotaExceededPaid,
		VersionInvalid,
		VersionMissing
	}
}
