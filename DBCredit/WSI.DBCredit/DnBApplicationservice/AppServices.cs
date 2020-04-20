using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Source = WSI.DnB.Schemas.NBClients.ContractTypes;
//using Target = WSi.DnB.Schemas.Service.FoundCompany;

using WSI.DnBCredit.Data;
using WSI.DnBCredit.Transformation;
using WSI.Common.XML;
using System.Data.SqlClient;

namespace WSI.DnBCredit.DnBApplicationservice
{
    public class AppServices
    {
        private string _dbConnectionString = string.Empty;

        public AppServices()
        {

        }


        public AppServices(string dbConnectionString)
        {
            _dbConnectionString = dbConnectionString;
        }


        /// <summary>
        /// TransDnBGetTokenResp
        /// </summary>
        /// <param name="responseTokenRetreive"></param>
        /// <returns></returns>
        public string TransformDnBGetTokenResponse(string responseTokenRetreive)
        {
            string tokenValue = string.Empty;
            try
            {
                DnBtoNBRsTransformer caller = new DnBtoNBRsTransformer();
                tokenValue = caller.TransformDnBGetTokenResponse(responseTokenRetreive);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + ex.Message);
               // throw ex;
            }
            return tokenValue;


        }

        #region Company request/response todo
        /// <summary>
        /// This will be called from the Process on incoming data transformation.
        /// </summary>        
        /// <returns></returns>
        public bool TransformNBReqToDnBRequestParam(string sourceND, ref string errors, ref string restParam)
        {
            restParam = string.Empty;
            errors = string.Empty;

            if (string.IsNullOrEmpty(sourceND))
            {
                return false;
            }        
            bool isTransformed = false;
            try
            {
                Source.FindCompanyRq_Type sourceSaveDocRq = NBXmlSerializer.Deserialize(sourceND, typeof(Source.FindCompanyRq_Type)) as Source.FindCompanyRq_Type;

                NBtoDnBRqTransformer caller = new NBtoDnBRqTransformer();
                isTransformed = caller.TransformNBReqToDnBRequestParam(sourceSaveDocRq, ref errors, ref restParam);
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + " --restParam:" + restParam + "--errors:" + errors);
            }
            catch (Exception ex)
            {
                isTransformed = false;
                errors += ex.Message;
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name +"/"+ System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + errors);
            }

            return isTransformed;
        }


        /// <summary>
        /// DnBtoNBCompnayResponseTransformation
        /// </summary>
        /// <param name="sourceResponse"></param>
        /// <param name="targetNB"></param>
        /// <returns></returns>
        public string TransformDnBtoNBCompnayResponse(string sourceResponse, string reqID, string respID, string sessionID, ref string errors)
        {
            string retVal = string.Empty;
            try
            {
                DnBtoNBRsTransformer caller = new DnBtoNBRsTransformer();
                retVal = caller.TransformDnBFindCompanyResponse(sourceResponse, reqID, respID, sessionID, ref errors);
            }
            catch (Exception ex)
            {
                errors += ex.Message.ToString();
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + errors);
                throw ex;
            }

            return retVal;
        }
        #endregion


 
        #region credit score request response: todo
        public bool TransformNBtoDnBCreditScoreRequest(string dunsNumber, string countryCode, ref string errors, ref string restParam)
        {
            restParam = string.Empty;
            errors = string.Empty;
            bool isTransformed = false;   
            try
            {  
                    NBtoDnBRqTransformer caller = new NBtoDnBRqTransformer();
                    isTransformed = caller.TransformNBReqtoDnBGetCreditScoreReq(dunsNumber, countryCode, ref errors, ref restParam);
                
            }
            catch (Exception ex)
            {
                isTransformed = false;
                errors += ex.Message;
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + errors);
            }
            return isTransformed;
        }

       

        /// <summary>
        /// IsCCSInDB
        /// </summary>
        /// <param name="dunsNumber"></param>
        /// <returns></returns>
        public bool IsCCSInDB(string dunsNumber)
        {
            bool isTrue = false;
            try
            {               
                DnBDBHelper dnbHelper = new DnBDBHelper();
                isTrue = dnbHelper.IsCCSSavedAndValid(dunsNumber, _dbConnectionString);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + ex.Message);
                isTrue = false;
            }
            return isTrue;
        }


        //private  bool IsDnBSaved(string duns)
        //{
        //    bool retvalue = false;
        //    try
        //    {
        //        string queryString =string.Format("SELECT [PARTY_KEY],[COUNTRY_SUBDIVISION_CD] ,[PARTY_NAME],[PARTY_ADDRESS_LINE1_TXT] ,[PARTY_CITY_NM],[PARTY_POSTAL_CODE],[VENDOR_PARTY_ID] FROM dbo.PARTY where VENDOR_PARTY_ID={0}",duns);
        //        using (SqlConnection connection = new SqlConnection(_dbConnectionString))
        //        {
        //            SqlCommand command = new SqlCommand(queryString, connection);
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.HasRows)
        //                {
        //                    retvalue = true;
        //                }
        //                else
        //                {
        //                    retvalue = false;
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name +"/"+ System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + ex.Message);
        //        retvalue = false;
        //    }

        //    return retvalue;

        //}
        /// <summary>
        /// BuildDnBResponseFromDB
        /// </summary>
        /// <param name="sourceResponse"></param>       
        /// <returns></returns>
        public string BuildDnBResponseFromDB(string dunsNumber, string reqID, string respID, string sessionID)
        {           

            string tranformedVal = string.Empty;
            try
            {                
                DnBDBHelper dnbHelper = new DnBDBHelper();
                Dictionary<string, string> cssValuefromDB = dnbHelper.LoadCCSValueFromDB(dunsNumber, _dbConnectionString);               
                if (cssValuefromDB != null)
                {
                    DnBtoNBRsTransformer caller = new DnBtoNBRsTransformer();
                    tranformedVal = caller.TransformDnBCCSFromDB(cssValuefromDB, reqID, respID, sessionID);
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-tranformedVal:" + tranformedVal);
                }
                else
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--GetCCSValue: returned NO VALUE");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + ex.Message);
                //throw ex;
            }


            return tranformedVal;

        }

        /// <summary>
        /// DnBtoNBCreditScoreResponseTransformation
        /// </summary>
        /// <param name="sourceResponse"></param>
        /// <param name="targetNB"></param>
        /// <returns></returns>
        public string TransformDnBtoNBCreditScoreResponse(string dunsNumberONrequest, string companyNameONrequest, string sourceResponse, string reqID, string respID, string sessionID, ref string errors)
        {
            string tranformedVal = string.Empty;
            try
            {
                DnBtoNBRsTransformer caller = new DnBtoNBRsTransformer();
                tranformedVal = caller.TransformDnBCCSResponse(dunsNumberONrequest,   companyNameONrequest, sourceResponse, reqID, respID, sessionID, _dbConnectionString, ref errors);
            }
            catch (Exception ex)
            {
                errors += ex.Message.ToString();
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Error:" + errors);

                throw ex;
            }
            return tranformedVal;
        }

        #endregion



        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                // any clean up
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
