using Satellite.ServiceClient.Model;

namespace Satellite.Data.Test
{
	public static class GeoCodeAddressModelResponseSample
	{
		public static string StringSample;
		public static GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet TypedSample;

		static GeoCodeAddressModelResponseSample()
		{
			StringSample =
				@"<WebServiceGeocodeQueryResultSet xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns =""https://geoservices.tamu.edu/"">
						<Version>4.01</Version>
						<TransactionId>5fd23bfc-88a0-4ccf-a543-28b8ad0f8dd1</TransactionId>
								  <QueryStatusCodes>Success</QueryStatusCodes>
								  <WebServiceGeocodeQueryResults>
								  <WebServiceGeocodeQueryResult>
								  <TransactionId>5fd23bfc-88a0-4ccf-a543-28b8ad0f8dd1</TransactionId>
											 <Latitude>37.4220296330465</Latitude>
											 <Longitude>-122.084331436251</Longitude>
											 <Version>4.1</Version>
											 <Quality>QUALITY_EXACT_PARCEL_CENTROID</Quality>
											 <MatchedLocationType>LOCATION_TYPE_STREET_ADDRESS</MatchedLocationType>
											 <MatchType>Relaxed</MatchType>
											 <FeatureMatchingResultCount>1</FeatureMatchingResultCount>
											 <FeatureMatchingResultTypeNotes />
											 <TieHandlingStrategyType>Unknown</TieHandlingStrategyType>
											 <FeatureMatchingSelectionMethod>FeatureClassBased</FeatureMatchingSelectionMethod>
											 <FeatureMatchingSelectionMethodNotes />
											 <QueryStatusCodes>Success</QueryStatusCodes>
											 <GeocodeQualityType>ExactParcelCentroidPoint</GeocodeQualityType>
											 <NAACCRGISCoordinateQualityCode>02</NAACCRGISCoordinateQualityCode>
											 <NAACCRGISCoordinateQualityName>Parcel</NAACCRGISCoordinateQualityName>
											 <NAACCRGISCoordinateQualityType>Parcel</NAACCRGISCoordinateQualityType>
											 <NAACCRCensusTractCertaintyCode>1</NAACCRCensusTractCertaintyCode>
											 <NAACCRCensusTractCertaintyName>ResidenceStreetAddress</NAACCRCensusTractCertaintyName>
											 <NAACCRCensusTractCertaintyType>ResidenceStreetAddress</NAACCRCensusTractCertaintyType>
											 <FeatureMatchingGeographyType>Parcel</FeatureMatchingGeographyType>
											 <FeatureMatchingResultType>Success</FeatureMatchingResultType>
											 <MatchScore>98.994082840236686</MatchScore>
											 <InterpolationType>ArealInterpolation</InterpolationType>
											 <InterpolationSubType>ArealInterpolationGeometricCentroid</InterpolationSubType>
											 <RegionSize>84126.0762965009</RegionSize>
											 <RegionSizeUnits>Meters</RegionSizeUnits>
											 <QueryStatusCodeName>Success</QueryStatusCodeName>
											 <QueryStatusCodeValue>200</QueryStatusCodeValue>
											 <ExceptionOccurred>false</ExceptionOccurred>
											 <ErrorMessage />
											 <TimeTaken>0.0180018</TimeTaken>
											 <CensusRecords />
											 <CensusYear>Unknown</CensusYear>
											 <CensusTimeTaken>0</CensusTimeTaken>
											 <CensusStateFips />
											 <CensusCountyFips />
											 <CensusTract />
											 <CensusBlockGroup />
											 <CensusBlock />
											 <CensusPlaceFips />
											 <CensusMcdFips />
											 <CensusMsaFips />
											 <CensusMetDivFips />
											 <CensusCbsaFips />
											 <CensusCbsaMicro />
											 <IIsPreParsed>false</IIsPreParsed>
											 <INonParsedStreetAddress />
											 <INumber />
											 <INumberFractional />
											 <IPreDirectional />
											 <IPreType />
											 <IPreQualifier />
											 <IPreArticle />
											 <IName />
											 <ISuffix />
											 <IPostArticle />
											 <IPostQualifier />
											 <IPostDirectional />
											 <ISuiteType />
											 <ISuiteNumber />
											 <ICity />
											 <IConsolidatedCity />
											 <IMinorCivilDivision />
											 <ICountySubRegion />
											 <ICounty />
											 <IState />
											 <IZip />
											 <IZipPlus1 />
											 <IZipPlus2 />
											 <IZipPlus3 />
											 <IZipPlus4 />
											 <IZipPlus5 />
											 <IPostOfficeBoxType />
											 <IPostOfficeBoxNumber />
											 <MNumber>1600</MNumber>
											 <MNumberFractional />
											 <MPreDirectional />
											 <MPreType />
											 <MPreQualifier />
											 <MPreArticle />
											 <MName>AMPHITHEATRE</MName>
											 <MSuffix>PKWY</MSuffix>
											 <MPostArticle />
											 <MPostQualifier />
											 <MPostDirectional />
											 <MSuiteType />
											 <MSuiteNumber />
											 <MCity>Mountain View</MCity>
												<MMinorCivilDivision />
												<MCountySubRegion />
												<MCounty />
												<MState>CA</MState>
												<MZip>94043</MZip>
												<MZipPlus1 />
												<MZipPlus2 />
												<MZipPlus3 />
												<MZipPlus4 />
												<MZipPlus5 />
												<MPostOfficeBoxType />
												<MPostOfficeBoxNumber />
												<PNumber>1600</PNumber>
												<PNumberFractional />
												<PPreDirectional />
												<PPreType />
												<PPreQualifier />
												<PPreArticle />
												<PName>AMPHITHEATRE</PName>
												<PSuffix>PKWY</PSuffix>
												<PPostArticle />
												<PPostQualifier />
												<PPostDirectional />
												<PSuiteType />
												<PSuiteNumber />
												<PCity>Mountain View</PCity>
													<PMinorCivilDivision />
													<PCountySubRegion />
													<PCounty />
													<PState>CA</PState>
													<PZip>94043</PZip>
													<PZipPlus1 />
													<PZipPlus2 />
													<PZipPlus3 />
													<PZipPlus4 />
													<PZipPlus5 />
													<PPostOfficeBoxType />
													<PPostOfficeBoxNumber />
													<FNumber>1600</FNumber>
													<FNumberFractional />
													<FPreType />
													<FPreQualifier />
													<FPreArticle />
													<FName>AMPHITHEATRE</FName>
													<FSuffix>PKWY</FSuffix>
													<FPostArticle />
													<FPostQualifier />
													<FPostDirectional />
													<FSuiteType />
													<FSuiteNumber />
													<FCity>Palo Alto</FCity>
													  <FCounty>SANTA CLARA</FCounty>
														 <FState>CA</FState>
														 <FZip>94043</FZip>
														 <FPostOfficeBoxType />
														 <FPostOfficeBoxNumber />
														 <FArea>84126.0762965009</FArea>
														 <FAreaType>Meters</FAreaType>
														 <FGeometrySRID>4269</FGeometrySRID>
														 <FGeometry />
														 <FSource>SOURCE_BOUNDARY_SOLUTIONS_PARCEL_CENTROIDS</FSource>
														 <FVintage>2012</FVintage>
														 <FPrimaryIdField>UniqueId</FPrimaryIdField>
														 <FPrimaryIdValue>6766330</FPrimaryIdValue>
														 <FSecondaryIdField>APN</FSecondaryIdField>
														 <FSecondaryIdValue>11621046</FSecondaryIdValue>
														 </WebServiceGeocodeQueryResult>
														 </WebServiceGeocodeQueryResults>
														 <QueryStatusCodeName>Success</QueryStatusCodeName>
														 <QueryStatusCodeValue>200</QueryStatusCodeValue>
														 <TimeTaken>0.0190019</TimeTaken>
														 <ExceptionOccurred>false</ExceptionOccurred>
														 <IIsPreParsed>true</IIsPreParsed>
														 <INonParsedStreetAddress>|||||||||||| Mountain View ||||| California |||</INonParsedStreetAddress>
															<INumber>1600</INumber>
															<INumberFractional />
															<IPreDirectional />
															<IPreType />
															<IPreQualifier />
															<IPreArticle />
															<IName>AMPHITHEATRE</IName>
															<ISuffix>PKWY</ISuffix>
															<IPostArticle />
															<IPostQualifier />
															<IPostDirectional />
															<ISuiteType />
															<ISuiteNumber />
															<ICity>Mountain View</ICity>
																<IMinorCivilDivision />
																<ICountySubRegion />
																<ICounty />
																<IState>CA</IState>
																<IZip>94043</IZip>
																<IZipPlus1 />
																<IZipPlus2 />
																<IZipPlus3 />
																<IZipPlus4 />
																<IZipPlus5 />
																<IPostOfficeBoxType />
																<IPostOfficeBoxNumber />
																<FeatureMatchingResultType>Success</FeatureMatchingResultType>
																<FeatureMatchingResultCount>1</FeatureMatchingResultCount>
																</WebServiceGeocodeQueryResultSet>";

			TypedSample = new GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet()
			{
				QueryStatusCode = QueryStatusCode.Success,
				ExceptionOccurred = false,
				TimeTaken = new decimal(0.12345),
				TransactionId = "Transaction Id",
				Version = new decimal(4.01),
				WebServiceGeocodeQueryResults =
					new GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet.
						WebServiceGeocodeQueryResultSetWebServiceGeocodeQueryResults()
					{
						WebServiceGeocodeQueryResult =
							new GeoCodeAddressResponseModel.WebServiceGeocodeQueryResultSet.
								WebServiceGeocodeQueryResultSetWebServiceGeocodeQueryResultsWebServiceGeocodeQueryResult()
							{

								Latitude = new decimal(37.4220296330465),
								Longitude = new decimal(-122.084331436251),
								Quality = "Quality",
								TransactionId = "Transaction Id"
							}
					}
			};
		}
	}
}