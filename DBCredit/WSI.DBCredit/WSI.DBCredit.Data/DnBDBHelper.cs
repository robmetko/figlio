using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSI.DnBCredit.Data.Entities;

namespace WSI.DnBCredit.Data
{
    public class DnBDBHelper
    {
        public Dictionary<string, string> LoadCCSValueFromDB(string dunsNumber, string connectionstring)
        {


            Dictionary<string, string> dictVal = new Dictionary<string, string>();
            try
            {
                //check if the party exists and get the Party CCS_PCT
                Party partyCS = GetParty(dunsNumber, connectionstring);
                if (partyCS != null)
                {
                    dictVal = GetPCCS(partyCS, "CCS_PCT",   connectionstring);
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "  getting the CCS values failed with errror:" + ex.Message);

                dictVal = null;
            }
            return dictVal;
        }

        /// <summary>
        /// we check if the CCS is in DB and if it is not older then 90 days
        /// </summary>
        /// <param name="dunsNumber"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        public bool IsCCSSavedAndValid(string dunsNumber, string connString)
        {
            bool isValidInDb = false;
            try
            {
                Party p = GetParty(dunsNumber, connString);
                isValidInDb = p != null;
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "  --party Exist:"+ isValidInDb.ToString());

                //this change was made to avoid checking when the party is null
                if (p == null)
                {
                    isValidInDb = false;
                }
                else
                {
                    //we use this as filter: CCS_PCT,  because this is the value we pass to the user.
                    Dictionary<string, string> d = GetPCCS(p, "CCS_PCT", connString);
                   
                    if ((isValidInDb == true && d.Count == 0) || (isValidInDb == false && d.Count == 0))
                    {
                        isValidInDb = false;
                    }
                    else
                    {
                        isValidInDb = true;
                    }
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "  --party Exist and Value of CSS is VAlid in DB:" + isValidInDb.ToString());
                }
            }
            catch (SqlException sqlex)
            {
                System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, " sqlex error: " + sqlex.Message.ToString());
                //throw sqlex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, " error: " + ex.Message.ToString());

                // throw ex;
            }

            return isValidInDb;

        }

        /// <summary>
        /// GetPCCS
        /// </summary>
        /// <param name="party"></param>
        /// <param name="creditScoreTypeCd"></param>
        /// <param name="connectionstring"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetPCCS(Party party, string creditScoreTypeCd,   string connectionstring)
        {

            Dictionary<string, string> dict = new Dictionary<string, string>();
            try
            {
                #region load party
                using (SqlConnection sConn = new SqlConnection(connectionstring))
                {
                    // Define the command.
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sConn;
                       
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "spDnBgetPartyScoreData";
                        command.Parameters.Add("@VENDOR_PARTY_ID", SqlDbType.NVarChar, 50).Value = party.VendorPartyId.Trim();
                        command.Parameters.Add("@CREDIT_SCORE_TYPE_CD", SqlDbType.NVarChar, 50).Value = creditScoreTypeCd;
                        command.Parameters.Add("@PARTY_KEY", SqlDbType.BigInt).Value = party.PartyKey;
                        sConn.Open();
                        SqlDataReader sReader = command.ExecuteReader();

                        while (sReader.Read())
                        {
                          
                            dict.Add("PartyKey", sReader["PARTY_KEY"].ToString());
                            dict.Add("VendorPartyId", sReader["VENDOR_PARTY_ID"].ToString());
                            dict.Add("CreditScoreVendorCd", sReader["CREDIT_SCORE_VENDOR_CD"].ToString());
                            dict.Add("CreditScoreTypeCd", sReader["CREDIT_SCORE_TYPE_CD"].ToString());
                            dict.Add("CreditScoreTxt", sReader["CREDIT_SCORE_TXT"].ToString());
                            dict.Add("CreditScoreDate", sReader["CREDIT_SCORE_DATE"].ToString());

                            dict.Add("PARTY_NAME", party.PartyName);
                            dict.Add("PARTY_CITY_NM", party.PartyCityNm);
                            dict.Add("COUNTRY_SUBDIVISION_CD", party.CountrySubdivisionCd);
                            dict.Add("PARTY_POSTAL_CODE", party.PartyPostalCode);
                            dict.Add("PARTY_ADDRESS_LINE1_TXT", party.PartyAddressLine1Txt);

                        }
                        sReader.Close();
                        sConn.Close();
                    }
                }
                #endregion


            }
            catch (SqlException sqlex)
            {
                System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, " sqlex error: " + sqlex.Message.ToString());
                //throw sqlex;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, " error: " + ex.Message.ToString());

                // throw ex;
            }
            return dict;
        }

        /// <summary>
        /// GetParty
        /// </summary>
        /// <param name="dunsNumber"></param>
        /// <param name="dbConnectionString"></param>
        /// <returns></returns>
        private static Party GetParty(string dunsNumber, string dbConnectionString)
        {

            Party p = null;
            try
            {
                #region load party
             
                using (SqlConnection sConn = new SqlConnection(dbConnectionString))
                {
                     
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sConn;
                      
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "spDnBGetPartyData";
                        command.Parameters.Add("@VENDOR_PARTY_ID", SqlDbType.NVarChar, 50).Value = dunsNumber.Trim();                    

                        // Open the connection and execute the reader.
                        sConn.Open();
                        SqlDataReader sReader = command.ExecuteReader();
                        while (sReader.Read())
                        {
                            p = new Party();

                            p.PartyKey = long.Parse(sReader["PARTY_KEY"].ToString());

                            p.VendorPartyId = sReader["VENDOR_PARTY_ID"].ToString();
                            p.PartyName = sReader["PARTY_NAME"].ToString();
                            p.PartyPostalCode = sReader["PARTY_POSTAL_CODE"].ToString();
                            p.PartyCityNm = sReader["PARTY_CITY_NM"].ToString();
                            p.PartyAddressLine1Txt = sReader["PARTY_ADDRESS_LINE1_TXT"].ToString();
                            p.CountrySubdivisionCd = sReader["COUNTRY_SUBDIVISION_CD"].ToString();

                        }
                        sReader.Close();
                        sConn.Close();
                    }
                }
                #endregion

            }
            catch (SqlException sqlex)
            {
                System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, " sqlex error: " + sqlex.Message.ToString());
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, " error: " + ex.Message.ToString());
 
            }
            return p;
        }

        /// <summary>
        /// SaveDnBCCScores
        /// </summary>
        /// <param name="party"></param>
        /// <param name="lstPartyScore"></param>
        /// <param name="dbConnectionString"></param>
        /// <param name="allData"></param>
        /// <returns></returns>
        public bool SaveDnBCCScores(Party party, List<PartyCreditScore> lstPartyScore, string dbConnectionString, string allData)
        {
            bool isSaved = false;
            try
            {
                #region save
                Party partyInDB = GetParty(party.VendorPartyId, dbConnectionString);//

                //We add only if the is there already
                if (partyInDB == null)
                {                     
                    var id = InsertParty(party, dbConnectionString); 

                    foreach (PartyCreditScore partyCCS in lstPartyScore)
                    {
                        partyCCS.PartyKey = long.Parse(id.ToString());
                        InsertCSS(partyCCS, dbConnectionString);
                    }

                    InsertPatydatainBulk(long.Parse(id.ToString()), party.VendorPartyId, allData, dbConnectionString);

                    isSaved = true;
                    System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, " This was inserted for the first time ");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, " This is an update of the party and insert new rows on ");
                    
                    // we updated//but we will ask 
                    //we check if the Party score is older then 90 days
                    // in this case we insert a new sets of data.
                    //we update the party
                    //if scores is older then 90 we insert new scores and new bullk
                  bool isPartyUpdated = UpdateParty(party, dbConnectionString);
                   System.Diagnostics.Debug.WriteLine(System.Reflection.MethodInfo.GetCurrentMethod().Name, "--Party updated:" + isPartyUpdated.ToString());

                    //this will re-use a check
                   bool isvalid= IsCCSSavedAndValid(party.VendorPartyId, dbConnectionString);

                    // we add a new sets if data older.
                    if (isvalid == false)
                    {
                        //update credit score and 
                        foreach (PartyCreditScore partyCCS in lstPartyScore)
                        {
                            partyCCS.PartyKey = 0;
                            InsertCSS(partyCCS, dbConnectionString);
                        }

                        //we insert a new link
                        InsertPatydatainBulk(0, party.VendorPartyId , allData, dbConnectionString);
                    }

                }
                #endregion

            }
            catch (SqlException ex)
            {
                #region error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.InnerException.Message);

                }
                else
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.Message);
                }
                #endregion

                isSaved = false;
                //throw ex;
            }
            return isSaved;


        }

        /// <summary>
        /// UpdateParty
        /// </summary>
        /// <param name="party"></param>
        /// <param name="dbConnectionString"></param>
        private bool UpdateParty(Party party, string connString)
        {
            bool isSaved = false;
            try
            {
                #region load party             

                using (SqlConnection sConn = new SqlConnection(connString))
                {
                    // Define the command.
                    using (SqlCommand command = new SqlCommand())
                    {                        
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "spDnBUpdateParty";
                        command.Connection = sConn;

                        command.Parameters.Add("@COUNTRY_SUBDIVISION_CD", SqlDbType.NVarChar, 50).Value = party.CountrySubdivisionCd;
                        command.Parameters.Add("@PARTY_NAME", SqlDbType.NVarChar, 255).Value = party.PartyName;
                        command.Parameters.Add("@PARTY_ADDRESS_LINE1_TXT", SqlDbType.NVarChar, 500).Value = party.PartyAddressLine1Txt;
                        command.Parameters.Add("@PARTY_CITY_NM", SqlDbType.NVarChar, 255).Value = party.PartyCityNm;
                        command.Parameters.Add("@PARTY_POSTAL_CODE", SqlDbType.NVarChar, 500).Value = party.PartyPostalCode;
                        command.Parameters.Add("@VENDOR_PARTY_ID", SqlDbType.NVarChar, 50).Value = party.VendorPartyId;
                        command.Parameters.Add("@LAST_UPDATE_TS", SqlDbType.DateTime2).Value = DateTime.UtcNow;//set the update tiem
                        sConn.Open();
                        command.ExecuteNonQuery();
                        sConn.Close();
                    }
                }
                #endregion
                System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--spDnBUpdateParty went OK");

               isSaved = true;
            }
            catch (SqlException ex)
            {
                #region error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.InnerException.Message);

                }
                else
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.Message);
                }
                #endregion
                isSaved = false;
            }
            return isSaved;
        }

        /// <summary>
        /// InsertCSS
        /// </summary>
        /// <param name="partyCCS"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        private bool InsertCSS(PartyCreditScore partyCCS, string connString)
        {
            bool isSaved = false;
            try
            {
                #region load party  

                using (SqlConnection sConn = new SqlConnection(connString))
                {
                    // Define the command.

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sConn;                      

                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "spDnBInsertPartyScoreData";
                        command.Parameters.Add("@PARTY_KEY", SqlDbType.BigInt).Value = partyCCS.PartyKey;
                        command.Parameters.Add("@CREDIT_SCORE_VENDOR_CD", SqlDbType.VarChar,50).Value = partyCCS.CreditScoreVendorCd;
                        command.Parameters.Add("@CREDIT_SCORE_TYPE_CD", SqlDbType.VarChar, 50).Value = partyCCS.CreditScoreTypeCd;
                        command.Parameters.Add("@CREDIT_SCORE_DATE", SqlDbType.DateTime2).Value = partyCCS.CreditScoreDate;
                        command.Parameters.Add("@CREDIT_SCORE_TXT", SqlDbType.VarChar,500).Value = partyCCS.CreditScoreTxt;
                        command.Parameters.Add("@VENDOR_PARTY_ID", SqlDbType.VarChar, 50).Value = partyCCS.VendorPartyId;

                        // Open the connection and execute the reader.
                        sConn.Open();
                        command.ExecuteScalar();
                        sConn.Close();
                        isSaved = true;
                    }
                }
                #endregion
            }
            catch (SqlException ex)
            {
                #region error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.InnerException.Message);
                }
                else
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.Message);
                }
                #endregion
                isSaved = false;
                //throw ex;
            }
            return isSaved;
        }

        /// <summary>
        /// InsertPatydatainBulk
        /// </summary>
        /// <param name="partyKey"></param>
        /// <param name="vendorPartyId"></param>
        /// <param name="allData"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        private bool InsertPatydatainBulk(long partyKey, string vendorPartyId,  string allData, string connString)
        {
            bool isSaved = false;
            if (string.IsNullOrEmpty(allData))
            {
                return false;
            }
            try
            {
              
                #region load party              

                using (SqlConnection sConn = new SqlConnection(connString))
                {
                    // Define the command.
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sConn;
                        byte[] bArray = Encoding.UTF8.GetBytes(allData);                       
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "spDnBInsertPatydataInBulk";
                        command.Parameters.Add("@ppartykey", SqlDbType.BigInt).Value = partyKey;
                        command.Parameters.Add("@VENDOR_PARTY_ID", SqlDbType.NVarChar, 50).Value = vendorPartyId;
                        command.Parameters.Add("@binaryData", SqlDbType.VarBinary, 8000).Value = bArray;
                        sConn.Open();
                        command.ExecuteNonQuery();
                        sConn.Close();
                        isSaved = true;
                    }
                }
                #endregion
            }
            catch (SqlException ex)
            {
                #region error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.InnerException.Message);

                }
                else
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.Message);
                }
                #endregion
                isSaved = false;
            }
            return isSaved;

            
        }

        /// <summary>
        /// InsertParty
        /// </summary>
        /// <param name="party"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        private int InsertParty(Party party, string connString)
        {
            int retValue = 0;
            try
            {
                #region load party             

                using (SqlConnection sConn = new SqlConnection(connString))
                {
                    // Define the command.
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = sConn;
                       
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = "spDnBInsertParty";
                      
                        command.Parameters.Add("@COUNTRY_SUBDIVISION_CD", SqlDbType.NVarChar,50).Value = party.CountrySubdivisionCd;
                        command.Parameters.Add("@PARTY_NAME", SqlDbType.NVarChar, 255).Value = party.PartyName;
                        command.Parameters.Add("@PARTY_ADDRESS_LINE1_TXT", SqlDbType.NVarChar, 500).Value = party.PartyAddressLine1Txt;
                        command.Parameters.Add("@PARTY_CITY_NM", SqlDbType.NVarChar, 255).Value = party.PartyCityNm;
                        command.Parameters.Add("@PARTY_POSTAL_CODE", SqlDbType.NVarChar, 500).Value = party.PartyPostalCode;
                        command.Parameters.Add("@VENDOR_PARTY_ID", SqlDbType.NVarChar, 50).Value = party.VendorPartyId;
                        sConn.Open();
                        var r = command.ExecuteScalar();  
                        if (r !=null)
                        {
                           retValue = int.Parse(r.ToString());
                        }
                        sConn.Close();
                    }
                }
                #endregion
            }
            catch (SqlException ex)
            {
                #region error
                if (ex.InnerException != null)
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.InnerException.Message);

                }
                else
                {
                    System.Diagnostics.Trace.Write("DnBService-" + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name + "/" + System.Reflection.MethodBase.GetCurrentMethod().Name + "--ERROR:" + ex.Message);
                }
                #endregion

            }
            return retValue;
        }


    }
}
 