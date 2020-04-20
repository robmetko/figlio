//using System;
//using System.Collections.Generic;
//using System.Data.Entity.Validation;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WSI.Common.DataAccess.UnitOfWork.EntityFramework;

//namespace WSI.DnBCredit.Data
//{
//    public class DnBDataLayer : UnitOfWorkBase, IDisposable
//    {
//      protected Repository<PartyCreditScore> _partyCreditScoreRepository;
//        protected Repository<Party> _partyRepository;
       


//        public DnBDataLayer() : base()
//        {
//            _dbContext = new DnBRepositoryContext();

//        }

//        public DnBDataLayer(string dbConnectionstring)
//            : base()
//        {
//            _dbContext = new DnBRepositoryContext(dbConnectionstring);
            

//        }


//        public Repository<PartyCreditScore> DnBCCSDataRepository
//        {
//            get
//            {
//                if (_partyCreditScoreRepository == null)
//                {
//                    _partyCreditScoreRepository = new Repository<PartyCreditScore>(_dbContext);
//                }

//                return _partyCreditScoreRepository;
//            }
//        }

//        public Repository<Party> DnBPartyDataRepository
//        {
//            get
//            {
//                if (_partyRepository == null)
//                {
//                    _partyRepository = new Repository<Party>(_dbContext);
//                }

//                return _partyRepository;
//            }
//        }



//        /// <summary>
//        /// IsCCSsaved
//        /// </summary>
//        /// <param name="dunsNumber"></param>
//        /// <returns></returns>
//        public bool IsCCSsaved(string dunsNumber)
//        {
//            bool isSaved = false;
//            try
//            {
               
//                Party partyCS =   DnBPartyDataRepository.Get(v => v.VendorPartyId == dunsNumber, null, string.Empty).FirstOrDefault();                   

//                if (partyCS != null)
//                {
//                    isSaved= true;
//                 }
//                else
//                {
//                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--Party is null:");//
//                }
//            }
//            catch (SqlException ex)
//            { 
//              System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--error4:" + ex.Message);
  
//                isSaved = false;
               
//            }
            

//            return isSaved;
//        }

//        /// <summary>
//        /// GetCCS_value
//        /// </summary>
//        /// <param name="dunsNumber"></param>
//        /// <returns></returns>
//        public Dictionary<string,string>  GetCCSValue(string dunsNumber)
//        {
//            string retCCSVal = string.Empty;
//            Dictionary<string, string> dictList = new Dictionary<string, string>();
//            try
//            {
//                //check if the party exists and get the Party CCS_PCT
//                Party partyCS = DnBPartyDataRepository.Get(v => v.VendorPartyId == dunsNumber,null,string.Empty).FirstOrDefault();
              
//                if (partyCS != null)
//                {                     

//                  PartyCreditScore partyCSS = DnBCCSDataRepository.Get(v => v.VendorPartyId ==  partyCS.VendorPartyId && v.PartyKey== partyCS.PartyKey && v.CreditScoreTypeCd == "CCS_PCT", null, string.Empty).FirstOrDefault();
  
//                    if (partyCSS != null)
//                    {
//                        //Fill up the dictiniary with data from DB tobe used in the creation of response.
//                        dictList.Add("COUNTRY_SUBDIVISION_CD", partyCS.CountrySubdivisionCd);
//                        dictList.Add("PARTY_NAME", partyCS.PartyName);
//                        dictList.Add("PARTY_ADDRESS_LINE1_TXT", partyCS.PartyAddressLine1Txt);
//                        dictList.Add("PARTY_CITY_NM", partyCS.PartyCityNm);
//                        dictList.Add("PARTY_POSTAL_CODE", partyCS.PartyPostalCode);
//                        dictList.Add("VENDOR_PARTY_ID", partyCS.VendorPartyId);
//                        dictList.Add("CREDIT_SCORE_DATE", partyCSS.CreditScoreDate.ToString());
//                        dictList.Add("CREDIT_SCORE_TXT", partyCSS.CreditScoreTxt);

//                     }
//                 }

 
//            }
//            catch (SqlException ex)
//            {
//                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.Message);
//               // throw ex;
//            }
//            //we get the
//            return dictList;
//        }


//        /// <summary>
//        /// SaveDnBScores
//        /// </summary>
//        /// <param name="party"></param>
//        /// <param name="lstPartyScore"></param>
//        public bool SaveDnBCCScores(Party party, List<PartyCreditScore> lstPartyScore)
//        {
//            bool isSaved = false;
//            try
//            {
//                Party partyInDB= DnBPartyDataRepository.Get(v => v.VendorPartyId == party.VendorPartyId).FirstOrDefault();

//                //We add only if the is there already
//                if (partyInDB==null)
//                {
//                    DnBPartyDataRepository.Insert(party);
//                    _dbContext.SaveChanges();

//                    var id = party.PartyKey;

//                    foreach (PartyCreditScore partyCCS in lstPartyScore)
//                    {
//                        partyCCS.PartyKey = id;
//                        DnBCCSDataRepository.Insert(partyCCS);
//                    }

//                    _dbContext.SaveChanges();

//                    isSaved = true;

//                }
//                else
//                {
//                  // we updated//but we will ask 


//                }
               
//            }
//            catch (SqlException ex)
//            {
//                if (ex.InnerException != null)
//                {
//                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.InnerException.Message);

//                }
//                else
//                {
//                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.Message);
//                   }


//                isSaved = false;
//                //throw ex;
//            }
//            return isSaved;


//        }

//        public Dictionary<string, string> GetCCSValue(string dunsNumber, string connectionstring)
//        {

//            string retValCCS = string.Empty;
//            Dictionary<string, string> drrrr = new Dictionary<string, string>();
//            try
//            {
//                using (DnBDataLayer d = new DnBDataLayer(connectionstring))
//                {


//                    string retCCSVal = string.Empty;

//                    //check if the party exists and get the Party CCS_PCT
//                    Party partyCS = d.DnBPartyDataRepository.Get(v => v.VendorPartyId == dunsNumber, null, string.Empty).FirstOrDefault();
//                    // _dbContext.SaveChanges();
//                    if (partyCS != null)
//                    {
//                        PartyCreditScore partyCSS = d.DnBCCSDataRepository.Get(v => v.VendorPartyId == partyCS.VendorPartyId && v.PartyKey == partyCS.PartyKey && v.CreditScoreTypeCd == "CCS_PCT", null, string.Empty).FirstOrDefault();
//                        if (partyCSS != null)
//                        {
//                            //Fill up the dictiniary with data from DB tobe used in the creation of response.
//                            drrrr.Add("COUNTRY_SUBDIVISION_CD", partyCS.CountrySubdivisionCd);
//                            drrrr.Add("PARTY_NAME", partyCS.PartyName);
//                            drrrr.Add("PARTY_ADDRESS_LINE1_TXT", partyCS.PartyAddressLine1Txt);
//                            drrrr.Add("PARTY_CITY_NM", partyCS.PartyCityNm);
//                            drrrr.Add("PARTY_POSTAL_CODE", partyCS.PartyPostalCode);
//                            drrrr.Add("VENDOR_PARTY_ID", partyCS.VendorPartyId);
//                            drrrr.Add("CREDIT_SCORE_DATE", partyCSS.CreditScoreDate.ToString());
//                            drrrr.Add("CREDIT_SCORE_TXT", partyCSS.CreditScoreTxt);
//                            System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-CCCS is loaded from DB-partyCS.PartyName:" + partyCS.PartyName);
//                        }
//                    }

//                }



//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "  getting the CCS values failed with errror:" + ex.Message);
//                //..throw;
//                return null;
//            }
//            return drrrr;
//        }


//        public Dictionary<string, string> GetCCSValue(string dunsNumber, string connectionstring)
//        {

//            string retValCCS = string.Empty;
//            Dictionary<string, string> drrrr = new Dictionary<string, string>();
//            try
//            {
//                using (DnBDataLayer d = new DnBDataLayer(connectionstring))
//                {


//                    string retCCSVal = string.Empty;

//                    //check if the party exists and get the Party CCS_PCT
//                    Party partyCS = d.DnBPartyDataRepository.Get(v => v.VendorPartyId == dunsNumber, null, string.Empty).FirstOrDefault();
//                    _dbContext.SaveChanges();
//                    if (partyCS != null)
//                    {
//                        PartyCreditScore partyCSS = d.DnBCCSDataRepository.Get(v => v.VendorPartyId == partyCS.VendorPartyId && v.PartyKey == partyCS.PartyKey && v.CreditScoreTypeCd == "CCS_PCT", null, string.Empty).FirstOrDefault();
//                        if (partyCSS != null)
//                        {
//                            Fill up the dictiniary with data from DB tobe used in the creation of response.
//                            drrrr.Add("COUNTRY_SUBDIVISION_CD", partyCS.CountrySubdivisionCd);
//                            drrrr.Add("PARTY_NAME", partyCS.PartyName);
//                            drrrr.Add("PARTY_ADDRESS_LINE1_TXT", partyCS.PartyAddressLine1Txt);
//                            drrrr.Add("PARTY_CITY_NM", partyCS.PartyCityNm);
//                            drrrr.Add("PARTY_POSTAL_CODE", partyCS.PartyPostalCode);
//                            drrrr.Add("VENDOR_PARTY_ID", partyCS.VendorPartyId);
//                            drrrr.Add("CREDIT_SCORE_DATE", partyCSS.CreditScoreDate.ToString());
//                            drrrr.Add("CREDIT_SCORE_TXT", partyCSS.CreditScoreTxt);
//                            System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-CCCS is loaded from DB-partyCS.PartyName:" + partyCS.PartyName);
//                        }
//                    }

//                }



//            }
//            catch (Exception ex)
//            {
//                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "  getting the CCS values failed with errror:" + ex.Message);
//                ..throw;
//                return null;
//            }
//            return drrrr;
//        }

//        //#region Helper

//        ///// <summary>
//        ///// SaveDnBCreditData
//        ///// </summary>
//        ///// <param name="dataFromResponse"></param>
//        ///// <returns></returns>
//        //public bool SaveDnBCreditData(Party party, List<PartyCreditScore> lstPartyScore, string connectionstring)
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

//        ///// <summary>
//        ///// GetCCSValue
//        ///// </summary>
//        ///// <param name="dunsNumber"></param>
//        ///// <param name="connectionstring"></param>
//        ///// <returns></returns>
//        //public Dictionary<string, string> GetCCSValue(string dunsNumber, string connectionstring)
//        //{

//        //    string retValCCS = string.Empty;
//        //    Dictionary<string, string> drrrr = new Dictionary<string, string>();
//        //    try
//        //    {
//        //        using (DnBDataLayer d = new DnBDataLayer(connectionstring))
//        //        {


//        //            string retCCSVal = string.Empty;

//        //            //check if the party exists and get the Party CCS_PCT
//        //            Party partyCS = d.DnBPartyDataRepository.Get(v => v.VendorPartyId == dunsNumber, null, string.Empty).FirstOrDefault();
//        //            // _dbContext.SaveChanges();
//        //            if (partyCS != null)
//        //            {
//        //                PartyCreditScore partyCSS = d.DnBCCSDataRepository.Get(v => v.VendorPartyId == partyCS.VendorPartyId && v.PartyKey == partyCS.PartyKey && v.CreditScoreTypeCd == "CCS_PCT", null, string.Empty).FirstOrDefault();
//        //                if (partyCSS != null)
//        //                {
//        //                    //Fill up the dictiniary with data from DB tobe used in the creation of response.
//        //                    drrrr.Add("COUNTRY_SUBDIVISION_CD", partyCS.CountrySubdivisionCd);
//        //                    drrrr.Add("PARTY_NAME", partyCS.PartyName);
//        //                    drrrr.Add("PARTY_ADDRESS_LINE1_TXT", partyCS.PartyAddressLine1Txt);
//        //                    drrrr.Add("PARTY_CITY_NM", partyCS.PartyCityNm);
//        //                    drrrr.Add("PARTY_POSTAL_CODE", partyCS.PartyPostalCode);
//        //                    drrrr.Add("VENDOR_PARTY_ID", partyCS.VendorPartyId);
//        //                    drrrr.Add("CREDIT_SCORE_DATE", partyCSS.CreditScoreDate.ToString());
//        //                    drrrr.Add("CREDIT_SCORE_TXT", partyCSS.CreditScoreTxt);
//        //                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "-CCCS is loaded from DB-partyCS.PartyName:" + partyCS.PartyName);
//        //                }
//        //            }

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
//        //            Party p = d.DnBPartyDataRepository.GetAll().Where(x => x.VendorPartyId == dunsNumber).FirstOrDefault();
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
//        //#endregion


//    }



   
//}
