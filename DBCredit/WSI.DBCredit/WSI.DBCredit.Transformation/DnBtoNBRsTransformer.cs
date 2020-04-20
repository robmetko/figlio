using System;
using System.Collections.Generic;
using Source = WSI.DnB.Schemas.NBClients.ContractTypes;
using Target = WSi.DnB.Schemas.Service.Token;
using SourceCC = WSi.DnB.Schemas.Service.CompanyCC;

using Newtonsoft.Json;
using WSI.Common.XML;
using WSi.DnB.Schemas.Service.Token;
using WSi.DnB.Schemas.Service.CompanyResponse;
using SourceCR = WSi.DnB.Schemas.Service.CompanyCC;
using WSI.DnBCredit.Data;
using System.Globalization;
using WSI.DnBCredit.Data.Entities;

namespace WSI.DnBCredit.Transformation
{
    public class DnBtoNBRsTransformer
    {
        public DnBtoNBRsTransformer()
        {

        }

        /// <summary>
        /// TransDnBGetTokenResp
        /// </summary>
        /// <param name="sourceResponse"></param>
        /// <returns></returns>
        public string TransformDnBGetTokenResponse(string sourceResponse)
        {
            string tokenFound = string.Empty;

            try
            {
                var resultGetToken = JsonConvert.DeserializeObject<GetToken>(sourceResponse);

                string respResult = resultGetToken.TransactionResult.ResultID;

                if (respResult == "CM000")
                {
                    tokenFound = resultGetToken.AuthenticationDetail.Token;
                }
                else
                {
                    string severityText = resultGetToken.TransactionResult.SeverityText;
                    string resultID = resultGetToken.TransactionResult.ResultID;
                    string messageVal = DnBResponseMeessageCodes_Dict.GetMessageBykey(respResult);

                    tokenFound = "Token not created.\n\r" + ":resultID:" + resultID + "\n\r" + "Message:" + messageVal;
                }
            }
            catch (Exception ex)
            {
                tokenFound = "Error: Method:" + System.Reflection.MethodInfo.GetCurrentMethod() + "\n\r Message:" + ex.Message;

            }
            return tokenFound;
        }

        /// <summary>
        /// TransDnBFindCompanyResp
        /// </summary>
        /// <param name="sourceResponse"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public string TransformDnBFindCompanyResponse(string sourceResponse, string reqID, string respID, string sessionID, ref string errors)
        {
            string finalVal = string.Empty;

          
            //transformation happened here
            Source.FindCompanyRs_Type findcompanyType = new Source.FindCompanyRs_Type();
            string severityText = string.Empty;
            string resultID = string.Empty;
            string messageVal = string.Empty;
            string respResult = string.Empty;
            try
            {
                if (string.IsNullOrEmpty(sourceResponse))
                {
                    throw new ArgumentException(" Invalid Transfromation Response, Source can not be null");
                }
                // this is a way around in case of unusual field name.
                sourceResponse = sourceResponse.Replace("\"$\": \"", "\"orgName\": \"");
                sourceResponse = sourceResponse.Replace("\"$\":\"", "\"orgName\":\"");                 
                var resultFindCompany = JsonConvert.DeserializeObject<FindCompanyResponse>(sourceResponse);
                  respResult = resultFindCompany.GetCleanseMatchResponse.TransactionResult.ResultID; 
                // Code result on success
                if (respResult == "CM000")
                {

                    WSi.DnB.Schemas.Service.CompanyResponse.Matchcandidate[] companiesFound = resultFindCompany.GetCleanseMatchResponse.GetCleanseMatchResponseDetail.MatchResponseDetail.MatchCandidate;
                    if (companiesFound.Length > 0)
                    {
                        #region filling up compnay type
                       
                        Source.CompanyInfo_Type companyType = null;
                        List<Source.CompanyInfo_Type> companyInfo = new List<Source.CompanyInfo_Type>();
                         

                        foreach (WSi.DnB.Schemas.Service.CompanyResponse.Matchcandidate item in companiesFound)
                        {
                            if (item != null)
                            { 
                            companyType = GetCompanyType( item);
                            //we need duns to continue
                            if (companyType != null && (!string.IsNullOrEmpty(companyType.DUNSNumber)))
                            {
                                companyInfo.Add(companyType);
                            }

                             }                        
                        }
                        //Add the status and the resp header
                        findcompanyType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
                        {
                            ReqID = reqID,
                            RespID = respID,
                            SessionID = sessionID
                        };//                    
                        findcompanyType.Status = new Source.Status_Type { StatusCode = "1" };
                        findcompanyType.Companies = companyInfo.ToArray();
                        finalVal = NBXmlSerializer.Serialize(findcompanyType);
                        #endregion
                        System.Diagnostics.Trace.Write("DnBService---:" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-finalVal:" + finalVal );
                    }
                    else
                    {
                        //no company found
                        severityText = resultFindCompany.GetCleanseMatchResponse.TransactionResult.SeverityText;
                        resultID = resultFindCompany.GetCleanseMatchResponse.TransactionResult.ResultID;
                        //we get the message from the list of messages provided by DnB
                        messageVal = DnBResponseMeessageCodes_Dict.GetMessageBykey(respResult.Trim());
                        System.Diagnostics.Trace.Write("DnBService-NO COMPANY FOUND:" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-resultID:" + resultID + "-messageVal:: " + messageVal);
                       
                        #region  
                        findcompanyType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
                        {
                            ReqID = reqID,
                            RespID = respID,
                            SessionID = sessionID
                        };//                      
                        findcompanyType.Status = new Source.Status_Type { StatusCode = "0" };

                        findcompanyType.Fault = new DnB.Schemas.NBClients.ContractTypes.Fault_Type
                        {
                            FaultCode = "43",
                            FaultDescription = messageVal

                        };
                        finalVal = NBXmlSerializer.Serialize(findcompanyType);

                        System.Diagnostics.Trace.Write("DnBService-CompanyName2:" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-resultID:" + resultID + "-messageVal:: " + messageVal);

                        #endregion
                    }
                }
                else
                {
                    severityText = resultFindCompany.GetCleanseMatchResponse.TransactionResult.SeverityText;
                    resultID = resultFindCompany.GetCleanseMatchResponse.TransactionResult.ResultID;
                    //we get the message from the list of messages provided by DnB
                    messageVal = DnBResponseMeessageCodes_Dict.GetMessageBykey(respResult.Trim());
                    System.Diagnostics.Trace.Write("DnBService-CompanyName2:" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-resultID:" + resultID + "-messageVal:: " + messageVal);
                     

                    #region  

                    findcompanyType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
                    {
                        ReqID = reqID,
                        RespID = respID,
                        SessionID = sessionID
                    };//                      
                    findcompanyType.Status = new Source.Status_Type { StatusCode = "0" };

                    findcompanyType.Fault = new DnB.Schemas.NBClients.ContractTypes.Fault_Type
                    {
                        FaultCode = "43",
                        FaultDescription = messageVal
                    };
                    finalVal = NBXmlSerializer.Serialize(findcompanyType);
                    System.Diagnostics.Trace.Write("DnBService-CompanyName2:" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-resultID:" + resultID + "-messageVal:: " + messageVal);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write("DnBService-CompanyName2:" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-resultID:" + resultID + "-messageVal:: " + messageVal, ex.Message);
                // throw new Exception("resultID:" + resultID + "--severityText:" + severityText + "--messageVal:" + messageVal);
                finalVal= ExceptionOnTransDnBFCResponse(respResult.Trim(), ex.Message, reqID, respID, sessionID, findcompanyType);
            }

            return finalVal;
        }



        /// <summary>
        /// ExceptionOnTransDnBFCResponse
        /// </summary>
        /// <param name="reqID"></param>
        /// <param name="respID"></param>
        /// <param name="sessionID"></param>
        /// <param name="findcompanyType"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string ExceptionOnTransDnBFCResponse(string resultID, string messageVal, string reqID, string respID, string sessionID, Source.FindCompanyRs_Type findcompanyType)
        {
            string finalVal;
            findcompanyType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
            {
                ReqID = reqID,
                RespID = respID,
                SessionID = sessionID
            };//                    
            findcompanyType.Status = new Source.Status_Type { StatusCode = "0" };

            findcompanyType.Fault = new DnB.Schemas.NBClients.ContractTypes.Fault_Type
            {
                FaultCode = "43",
                FaultDescription = messageVal

            };
            finalVal = NBXmlSerializer.Serialize(findcompanyType);
            return finalVal;
        }


        /// <summary>
        /// TransformDnBCCSResponse--In progress
        /// </summary>
        /// <param name="allValuesFromDB"></param>
        /// <returns></returns>
        public string TransformDnBCCSFromDB(Dictionary<string,string> allValuesFromDB, string reqID, string respID, string sessionID)
        {
            Source.GetCreditScoreRs_Type creditScoreType = new Source.GetCreditScoreRs_Type();
            string finalVal = string.Empty;
            try
            {          
            #region declaration
            string severityText = string.Empty;
            string resultID = string.Empty;
            string messageVal = string.Empty;
            string partyAddressLine1Txt = string.Empty;
            string partyCityNm = string.Empty;
            string partyPostalCd = string.Empty;
            string partyNm = string.Empty;
            string creditScoreDate = string.Empty;
            string creditScoreTxt = string.Empty;
            string vendorPartyId_Duns = string.Empty;
            string countrySubdivisionCd = string.Empty;
            #endregion
 
            #region filling up the scredit score type
            Source.CompanyInfo_Type companyType = new DnB.Schemas.NBClients.ContractTypes.CompanyInfo_Type();
            Source.Address_Type addressType = new DnB.Schemas.NBClients.ContractTypes.Address_Type();


            //if commercial number is empty or null we do not show that company in the search.
            addressType.Municipality = allValuesFromDB["PARTY_CITY_NM"];                
            addressType.Province = allValuesFromDB["COUNTRY_SUBDIVISION_CD"];
            addressType.PostalCode = allValuesFromDB["PARTY_POSTAL_CODE"]; 
            addressType.StreetAddress = allValuesFromDB["PARTY_ADDRESS_LINE1_TXT"];

          
            companyType.Address = addressType;
            companyType.DUNSNumber = allValuesFromDB["VendorPartyId"];
            companyType.CompanyName = allValuesFromDB["PARTY_NAME"];
            //reset of addreess comes here
            Source.CCS_Type ccstype = new DnB.Schemas.NBClients.ContractTypes.CCS_Type();
            //normally we filter thin on insert.// but if data gets corrupted this is check
            string nationalPercentile = allValuesFromDB["CreditScoreTxt"];

              if (string.IsNullOrEmpty(nationalPercentile))
                {
                throw new Exception("NationalPercentile is empty.");

                }
            ccstype.NationalPercentile = nationalPercentile;// allValuesFromDB["CREDIT_SCORE_TXT"];  
            ccstype.ScoreDate = allValuesFromDB["CreditScoreDate"];          
            creditScoreType.CommercialCreditScore = ccstype;
            #endregion

            creditScoreType.Company = companyType;
            creditScoreType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
            { ReqID = reqID, RespID = respID, SessionID = sessionID };//

            creditScoreType.Status = new Source.Status_Type { StatusCode = "1" };

           finalVal = creditScoreType!=null? NBXmlSerializer.Serialize(creditScoreType):"No value from DB at this time.";

            }
            catch (Exception ex)
            {

                #region error
                creditScoreType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
                {
                    ReqID = reqID,
                    RespID = respID,
                    SessionID = sessionID
                };//                    
                creditScoreType.Status = new Source.Status_Type { StatusCode = "0" };
                creditScoreType.Fault = new DnB.Schemas.NBClients.ContractTypes.Fault_Type
                {
                    FaultCode = "44",
                    FaultDescription = ex.Message.ToString()

                };
                finalVal = NBXmlSerializer.Serialize(creditScoreType);
                #endregion
            }
            return   finalVal;
        }

        /// <summary>
        /// TransDnBGetCreditScoreResp
        /// </summary>
        /// <param name="sourceResponse"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public string TransformDnBCCSResponse(string dunsNumberONrequest, string companyNameONrequest, string sourceResponse, string reqID, string respID, string sessionID, string _dbConnectionString, ref string errors)
        {
            string finalVal = string.Empty;

            
            #region declaration
            string severityText = string.Empty;
            string resultID = string.Empty;
            string messageVal = string.Empty;


            string partyAddressLine1Txt = string.Empty;
            string partyCityNm = string.Empty;
            string partyPostalCd = string.Empty;
            string partyNm = string.Empty;

            string creditScoreDate = string.Empty;
            string classCreditScoreTxt = string.Empty;
            string nationalpecscore = string.Empty;
            string rawScore = string.Empty;
            string vendorPartyId_Duns = string.Empty;
            string countrySubdivisionCd = string.Empty;
            #endregion

            Source.GetCreditScoreRs_Type creditScoreType = new Source.GetCreditScoreRs_Type(); 
            try
            {
                #region This handles an issue from JSON
                sourceResponse = sourceResponse.Replace("\"$\": \"", "\"valueHolder\": \"");
                sourceResponse = sourceResponse.Replace("\"$\":\"", "\"valueHolder\":\"");
                #endregion
                var resultGetCredScore = JsonConvert.DeserializeObject<SourceCC.CreditScore>(sourceResponse);//  
                
                string respResult = resultGetCredScore.OrderProductResponse.TransactionResult.ResultID;
               

                if (respResult.ToUpper() == "CM000")
                {
                    
                     SourceCC.Organization organization = resultGetCredScore.OrderProductResponse.OrderProductResponseDetail.Product.Organization;

                    if (organization != null)
                    {
                        #region filling up the scredit score type
                        Source.CompanyInfo_Type companyType = new DnB.Schemas.NBClients.ContractTypes.CompanyInfo_Type();
                        Source.Address_Type addressType = new DnB.Schemas.NBClients.ContractTypes.Address_Type();

                        SourceCC.Location location = organization.Location;

                        if (location.PrimaryAddress == null)
                        {
                            throw new Exception("Address is notavailable.");
                        }

                         
                        addressType.Municipality = GetValue(location.PrimaryAddress[0].PrimaryTownName);
                        partyCityNm = GetValue(location.PrimaryAddress[0].PrimaryTownName);
                        addressType.Province = GetValue(location.PrimaryAddress[0].TerritoryOfficialName);
                        addressType.PostalCode = GetValue(location.PrimaryAddress[0].PostalCode);
                        partyPostalCd = GetValue(location.PrimaryAddress[0].PostalCode);
                        addressType.Province = GetValue(location.PrimaryAddress[0].TerritoryOfficialName);
                        countrySubdivisionCd = GetValue(addressType.Province);
                        addressType.StreetAddress = GetValue(location.PrimaryAddress[0].StreetAddressLine[0].LineText);
                        partyAddressLine1Txt = GetValue(location.PrimaryAddress[0].StreetAddressLine[0].LineText);
                        addressType.CountryCode = GetValue(location.PrimaryAddress[0].CountryISOAlpha2Code);
                        companyType.Address = addressType;
                        SourceCC.Organization org = resultGetCredScore.OrderProductResponse.OrderProductResponseDetail.Product.Organization;                         
                       // BS does not want that--may012018
                       // companyType.TelephoneNumber = "00000000";// GetValue(org.Telecommunication.TelephoneNumber[0].TelecommunicationNumber);
                        string dunsNumber = GetValue(resultGetCredScore.OrderProductResponse.OrderProductResponseDetail.InquiryDetail.DUNSNumber);
                        if (string.IsNullOrEmpty(dunsNumber))
                        {
                            throw new Exception(" DUNSNumber is not available.");
                        }
                        companyType.DUNSNumber = dunsNumber;
                        vendorPartyId_Duns = dunsNumber;
                        string compName = GetValue(org.OrganizationName.OrganizationPrimaryName[0].OrganizationName.valueHolder);

                        if (string.IsNullOrEmpty(compName))
                        {
                            throw new Exception(" Company Name is not available.");
                        }
                        companyType.CompanyName = compName;
                        partyNm = compName;
                        creditScoreType.Company = companyType;
                        //rest of addreess comes here
                        Source.CCS_Type ccstype = new DnB.Schemas.NBClients.ContractTypes.CCS_Type();
                        SourceCC.Assessment assesment = organization.Assessment;

                        if (assesment.CommercialCreditScore == null)
                        {
                            throw new Exception("  Commercial Credit Score is not available.");
                        }


                        ccstype.ClassScore = GetValue(assesment.CommercialCreditScore[0].ClassScore);

                        classCreditScoreTxt = GetValue(assesment.CommercialCreditScore[0].ClassScore);                       

                        ccstype.ClassScoreDescription = GetValue(assesment.CommercialCreditScore[0].ClassScoreDescription);

                        nationalpecscore= GetValue(assesment.CommercialCreditScore[0].NationalPercentile.ToString());
                        ccstype.NationalPercentile = nationalpecscore;

                         rawScore= GetValue(assesment.CommercialCreditScore[0].RawScore.ToString());
                        ccstype.RawScore = rawScore;
                          
                        string scoreDt = GetValue(assesment.CommercialCreditScore[0].scoreDate.valueHolder);

                        if (string.IsNullOrEmpty(scoreDt))
                        {
                            throw new Exception("Score Date is not available.");
                        }
                        ccstype.ScoreDate = scoreDt;
                        creditScoreDate = ccstype.ScoreDate;
                        creditScoreType.CommercialCreditScore = ccstype;
                        #endregion

                    }

                    creditScoreType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
                    { ReqID = reqID, RespID = respID, SessionID = sessionID };//
                    creditScoreType.Status = new Source.Status_Type { StatusCode = "1" };
                    finalVal = NBXmlSerializer.Serialize(creditScoreType);
                    #region DBPart
                    bool isSaved = SaveCCS(_dbConnectionString, partyAddressLine1Txt, partyCityNm, partyPostalCd, 
                        partyNm, creditScoreDate, classCreditScoreTxt, nationalpecscore, rawScore, vendorPartyId_Duns, countrySubdivisionCd, sourceResponse);
                    #endregion
                    System.Diagnostics.Trace.Write("DnBService- SaveDnBCreditData:" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + ":" + isSaved.ToString());

                }
                else
                {
                    #region NOT SUCCESS
                     

                  severityText = resultGetCredScore.OrderProductResponse.TransactionResult.SeverityText;
                    resultID = resultGetCredScore.OrderProductResponse.TransactionResult.ResultID;
                    System.Diagnostics.Trace.Write("DnBService-NOT SUCCESS resultID:" + resultID);
                   
                    //we get the message from the list of messages provided by DnB
                   messageVal = DnBResponseMeessageCodes_Dict.GetMessageBykey(respResult.Trim());

                   

                    #region commneted
                    creditScoreType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
                    {

                        ReqID = reqID,
                        RespID = respID,
                        SessionID = sessionID
                    };//                    
                    creditScoreType.Status = new Source.Status_Type { StatusCode = "0" };

                    creditScoreType.Fault = new DnB.Schemas.NBClients.ContractTypes.Fault_Type
                    {
                        FaultCode = "44",// ToDo: bug 353383 respResult.Trim().ToUpper(),
                        FaultDescription = messageVal

                    };

                    Source.CompanyInfo_Type companyType = new DnB.Schemas.NBClients.ContractTypes.CompanyInfo_Type();
                    companyType.DUNSNumber = dunsNumberONrequest;
                    companyType.CompanyName = companyNameONrequest;
                    creditScoreType.Company = companyType;


                    finalVal = NBXmlSerializer.Serialize(creditScoreType);
                    #endregion
                    #endregion
                }

            }
            catch (Exception ex)
            {
              //  throw ex;
                #region 
                creditScoreType.RespHeader = new DnB.Schemas.NBClients.ContractTypes.RespHeader_Type
                {
                    ReqID = reqID,
                    RespID = respID,
                    SessionID = sessionID
                };//                    
                creditScoreType.Status = new Source.Status_Type { StatusCode = "0" };
                creditScoreType.Fault = new DnB.Schemas.NBClients.ContractTypes.Fault_Type
                {
                    FaultCode = "44",
                    FaultDescription = ex.Message.ToString()

                };

                System.Diagnostics.Trace.Write("DnBService-EXCEPTION resultID:" + ex.Message.ToString());
                finalVal = NBXmlSerializer.Serialize(creditScoreType);
                #endregion
            }

            return finalVal;
        }

        private string GetValue(string varValue)
        {
           if (!string.IsNullOrEmpty(varValue))
            {
                return varValue;
            }
            else
            {
                return string.Empty;
            }
        }

        #region Private
        /// <summary>
        /// SaveCCS
        /// </summary>       
        /// <returns></returns>
        private static bool SaveCCS(string _dbConnectionString, string partyAddressLine1Txt, string partyCityNm,
            string partyPostalCd, string partyNm, string creditScoreDate, string classCeditScoreTxt, string nationalPervScore, string rawScore,
            string vendorPartyId_Duns, string countrySubdivisionCd, string allData)
        {
            bool isSaved = false;
            try
            {
                #region Load Party object
                Party party = new Party();
                party.CountrySubdivisionCd = countrySubdivisionCd;
                party.PartyAddressLine1Txt = partyAddressLine1Txt;
                party.PartyCityNm = partyCityNm;
                party.PartyPostalCode = partyPostalCd;
                party.PartyName = partyNm;
                party.VendorPartyId = vendorPartyId_Duns;
                #endregion

                #region Load coll PartyCreditScore  
                DateTime dateScore = DateTime.ParseExact(creditScoreDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                List<PartyCreditScore> lstPartyScore = new List<PartyCreditScore> {

                            new PartyCreditScore
                            {
                                CreditScoreDate = dateScore,
                                CreditScoreTxt = classCeditScoreTxt,
                                VendorPartyId =  vendorPartyId_Duns,
                                CreditScoreTypeCd = "CCS_CLASS", //ClassScore    
                                CreditScoreVendorCd = "DNB"
                            },
                            new PartyCreditScore
                            {
                                CreditScoreDate = dateScore,
                                CreditScoreTxt = nationalPervScore,
                                VendorPartyId =  vendorPartyId_Duns,
                                CreditScoreTypeCd = "CCS_PCT", //NationalPercentile    
                                CreditScoreVendorCd = "DNB"
                            }, new PartyCreditScore
                            {
                                CreditScoreDate = dateScore,
                                CreditScoreTxt = rawScore,
                                VendorPartyId =  vendorPartyId_Duns,
                                CreditScoreTypeCd = "CCS_RAW", //RawScore    
                                CreditScoreVendorCd = "DNB"
                            }

                     };
                #endregion

               
                DnBDBHelper d = new DnBDBHelper();
                isSaved = d.SaveDnBCCScores(party, lstPartyScore, _dbConnectionString, allData);
            }
            catch (Exception ex)
            {
                isSaved = false;
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + ex.Message);
            }

            return isSaved;
        }

      
        /// <summary>
        /// CompanyType_Add
        /// </summary>      
        /// <returns>CompanyInfo_Type</returns>
        private static Source.CompanyInfo_Type GetCompanyType(  Matchcandidate item)
        {
            Source.CompanyInfo_Type companyType = new Source.CompanyInfo_Type();
            try
            {         
            companyType.CompanyName = item.OrganizationPrimaryName.OrganizationName.orgName;
            companyType.DUNSNumber = item.DUNSNumber;

            if (item.TelephoneNumber!=null)
            {
                companyType.TelephoneNumber = item.TelephoneNumber.TelecommunicationNumber;
            }

            if (item.MatchQualityInformation != null)
            {

                companyType.ConfidenceCodeValue = item.MatchQualityInformation.ConfidenceCodeValue.ToString();
                System.Diagnostics.Trace.Write("DnBService-ConfidenceCodeValue:" + item.MatchQualityInformation.ConfidenceCodeValue.ToString());
           }
            else
            {
                companyType.ConfidenceCodeValue = "0";//thi is in cane no confidence value//might be.

            }

            if (item.PrimaryAddress != null)
            {
                Source.Address_Type addressType = new DnB.Schemas.NBClients.ContractTypes.Address_Type();
                addressType = new DnB.Schemas.NBClients.ContractTypes.Address_Type();
                addressType.Municipality = item.PrimaryAddress.PrimaryTownName;
                addressType.PostalCode = item.PrimaryAddress.PostalCode;
                addressType.CountryCode = item.PrimaryAddress.CountryISOAlpha2Code;
                addressType.StreetAddress = item.PrimaryAddress.StreetAddressLine[0].LineText;
                addressType.ProvinceCode = item.PrimaryAddress.TerritoryAbbreviatedName;
                
                companyType.Address = addressType;
            }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + ex.Message);
            }
            return companyType;
        }


        #endregion
    }
}





