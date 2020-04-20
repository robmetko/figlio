using System;

using Source = WSI.DnB.Schemas.NBClients.ContractTypes;
using System.Text;


namespace WSI.DnBCredit.Transformation
{
    /// <summary>
    /// NBtoDnBRqTransformer is used to transform the request from NB to DnB
    /// </summary>
    public class NBtoDnBRqTransformer
    {

        public NBtoDnBRqTransformer()
            
        {

        }


        /// <summary>
        /// TransformNBRequestToRequestParam
        /// </summary>
        /// <param name="sourceRequest"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool TransformNBReqToDnBRequestParam(Source.FindCompanyRq_Type sourceRequest,ref string error, ref string restParam)
        {
            //base.Transform(sourceRequest, targetRequest);
            if (sourceRequest == null)
            {
                throw new ArgumentException("Move this on Process side: Invalid Transfromation request, Source   objects is null");
            }
            bool transformResult = false;
            try
            {
                StringBuilder paramlist = new StringBuilder();                
                paramlist.Append("?");
                #region building Params
               // CountryISOAlpha2Code = US & SubjectName = Gorman % 20Manufacturing & TerritoryName = CA & cleansematch = true
               

                if (sourceRequest.Company.CompanyName.Length>0)
                {
                    paramlist.Append("SubjectName=" + sourceRequest.Company.CompanyName);
                }

                if (sourceRequest.Company.Address != null)
                {
                    #region address
                    #region get street name
                    StringBuilder streetaddress = new StringBuilder();

                    if (sourceRequest.Company.Address.StreetNumber != null)
                    {
                        streetaddress.Append(sourceRequest.Company.Address.StreetNumber + " ");
                    }
                    if (sourceRequest.Company.Address.StreetName != null)
                    {
                        streetaddress.Append(sourceRequest.Company.Address.StreetName + " ");
                    }
                    if (sourceRequest.Company.Address.UnitNumber != null)
                    {
                        streetaddress.Append(sourceRequest.Company.Address.UnitNumber + " ");
                    }

                   
                    #endregion
                    if (sourceRequest.Company.Address.StreetAddress!=null)
                    {
                        paramlist.Append("&StreetAddressLine=" + sourceRequest.Company.Address.StreetAddress.ToString());
                    }
                    if (sourceRequest.Company.Address.Country != null)
                    {
                        paramlist.Append("&PrimaryTownName=" + sourceRequest.Company.Address.Municipality);
                    }
                    if (sourceRequest.Company.Address.CountryCode != null)
                    {
                        paramlist.Append("&CountryISOAlpha2Code=" + sourceRequest.Company.Address.CountryCode);
                    }
                    if (sourceRequest.Company.Address.Province != null)
                    {
                        paramlist.Append("&TerritoryName=" + sourceRequest.Company.Address.Province);
                    }
                    if (sourceRequest.Company.Address.ProvinceCode != null)
                    {
                        paramlist.Append("&TerritoryAbbreviatedName=" + sourceRequest.Company.Address.ProvinceCode);
                    }
                    if (sourceRequest.Company.Address.PostalCode != null)
                    {
                        paramlist.Append("&FullPostalCode=" + sourceRequest.Company.Address.PostalCode);
                    }
                    if (sourceRequest.Company.TelephoneNumber != null)
                    {
                        paramlist.Append("&TelephoneNumber=" + sourceRequest.Company.TelephoneNumber);
                    }
                
                    #endregion
                    
                }
               #endregion
                restParam = paramlist.ToString()+ "&cleansematch=true";
                transformResult = true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                transformResult = false;
            }
            return transformResult;
        }

       

        /// <summary>
        /// TransformNBReqtoDnBGetCreditScoreReq
        /// </summary>
        /// <param name="sourceRequest"></param>
        /// <param name="targetRequest"></param>
        /// <returns></returns>
        public bool TransformNBReqtoDnBGetCreditScoreReq(string dunsNumber,string countryCode, ref string errors, ref string restParam)//Source.GetCreditScoreRq_Type sourceNB
        {         
            
            bool transformResult = false;
            try
            {
               
                transformResult = true;                
               StringBuilder paramlist = new StringBuilder();
               string companyBaseFilter = string.Empty;
               
                if (countryCode.Trim().ToUpper() == "US")
                {

                    companyBaseFilter = "PPR_CCS_V9";
                }
                else
                {

                    companyBaseFilter = "PBPR_STD";
                }

                paramlist.Append(dunsNumber.Trim()+ "/products/" + companyBaseFilter); // US based companies  PPR_CCS_V9             
                restParam = paramlist.ToString() ;
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-countryCode:"+ countryCode.ToUpper() + "-restParam:" + restParam);
                transformResult = true;
            }
            catch (Exception ex)
            {
                errors = ex.Message;
                transformResult = false;
            }
            return transformResult;
        }



    }
}