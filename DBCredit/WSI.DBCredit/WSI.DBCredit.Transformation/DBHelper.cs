//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WSI.DnBCredit.Data;

//namespace WSI.DnBCredit.Transformation
//{
//    public class DBHelper
//    {
//        public DBHelper()
//        {

//        }
     


//        #region Helper

//        /// <summary>
//        /// SaveDnBCreditData
//        /// </summary>
//        /// <param name="dataFromResponse"></param>
//        /// <returns></returns>
//        //public   bool SaveDnBCreditData(Party party, List<PartyCreditScore> lstPartyScore , string connectionstring)
//        //{
//        //    bool isSaved = false;
//        //    try
//        //    {

//        //        using (DnBDataLayer d = new DnBDataLayer(connectionstring))
//        //        {                    
//        //            d.SaveDnBCCScores(party, lstPartyScore);
//        //        }
                
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + " --saving CSS FAILED:" + ex.Message);

//        //        // throw ex;
//        //    }
//        //    return isSaved;
//        //}

//        /// <summary>
//        /// GetCCSValue
//        /// </summary>
//        /// <param name="dunsNumber"></param>
//        /// <param name="connectionstring"></param>
//        /// <returns></returns>
//        //public Dictionary<string,string>  GetCCSValue(string dunsNumber, string connectionstring)
//        //{

//        //    string retValCCS = string.Empty;
//        //    Dictionary<string, string> drrrr = new Dictionary<string, string>();
//        //    try
//        //    {
//        //        using (DnBDataLayer d = new DnBDataLayer(connectionstring))
//        //        {
                  

//        //            string retCCSVal = string.Empty;                    
                   
//        //                //check if the party exists and get the Party CCS_PCT
//        //                Party partyCS = d.DnBPartyDataRepository.Get(v => v.VendorPartyId == dunsNumber, null, string.Empty).FirstOrDefault();
//        //                // _dbContext.SaveChanges();
//        //                if (partyCS != null)
//        //                {
//        //                    PartyCreditScore partyCSS = d.DnBCCSDataRepository.Get(v => v.VendorPartyId == partyCS.VendorPartyId && v.PartyKey == partyCS.PartyKey && v.CreditScoreTypeCd == "CCS_PCT", null, string.Empty).FirstOrDefault();
//        //                    if (partyCSS != null)
//        //                    {
//        //                    //Fill up the dictiniary with data from DB tobe used in the creation of response.
//        //                    drrrr.Add("COUNTRY_SUBDIVISION_CD", partyCS.CountrySubdivisionCd);
//        //                    drrrr.Add("PARTY_NAME", partyCS.PartyName);
//        //                    drrrr.Add("PARTY_ADDRESS_LINE1_TXT", partyCS.PartyAddressLine1Txt);
//        //                    drrrr.Add("PARTY_CITY_NM", partyCS.PartyCityNm);
//        //                    drrrr.Add("PARTY_POSTAL_CODE", partyCS.PartyPostalCode);
//        //                    drrrr.Add("VENDOR_PARTY_ID", partyCS.VendorPartyId);
//        //                    drrrr.Add("CREDIT_SCORE_DATE", partyCSS.CreditScoreDate.ToString());
//        //                    drrrr.Add("CREDIT_SCORE_TXT", partyCSS.CreditScoreTxt);
//        //                     System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-CCCS is loaded from DB-partyCS.PartyName:" + partyCS.PartyName);
//        //                    }
//        //                }
                   
//        //        }

                    
                 
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "  getting the CCS values failed with errror:" + ex.Message);
//        //        //..throw;
//        //        return null;
//        //    }
//        //    return drrrr;
//        //}

//        //public bool IsCCSsaved(string dunsNumber, string connectionstring)
//        //{

//        //    bool isSaved = false;

//        //    try
//        //    {
//        //        using (DnBDataLayer d = new DnBDataLayer(connectionstring))
//        //        {
//        //          Party p=  d.DnBPartyDataRepository.GetAll().Where(x => x.VendorPartyId == dunsNumber).FirstOrDefault();
//        //            if (p != null)
//        //            {
//        //                isSaved = true;
//        //            }
//        //        }



//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "  getting the CCS values failed with errror:" + ex.Message);
//        //        //..throw;
//        //        isSaved = false; ;
//        //    }
//        //    return isSaved;
//        //}
//        #endregion
//    }
//}
