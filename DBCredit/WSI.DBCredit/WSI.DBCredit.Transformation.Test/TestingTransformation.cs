using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml.Schema;
using System.Xml;
using WSI.Common.XML;
using Newtonsoft.Json;
using WSI.DnBCredit.DnBApplicationservice;
using WSi.DnB.Schemas.Service.CompanyResponse;
using WSi.DnB.Schemas.Service.Token;
using WSI.DnBCredit.Transformation;
using WSI.DnBCredit.Data;
using System.Globalization;
using System.Collections.Generic;
using WSI.DnB.Schemas.NBClients.ContractTypes;

using System.Configuration;
namespace WSI.DnBCredit.Transformation.Test
{
    [TestClass]
    public class TestingTransformation
    {
        

        [TestMethod]
        public void RunTest()
        {
            TestIncomeRequestFromNB();
          //  Test_DnBtoNB_FIND_Compnay_ResponseTransformation();
          //  Test_loadCSS_FROMDB();

            //Call_DnBtoNB_CreditScore_ResponseTransformation();

            // TestIncomeRequestFromNB();

            // Test_DnBtoNB_FIND_Compnay_ResponseTransformation();


//robmetko april 20 220
            // TestIncomeRequestFromNB();

            //find compnay response
            // Test_DnBtoNBCompnayResponseTransformation();


            //credit score response
            //Call_DnBtoNBCreditScoreResponseTransformation();

            //token response
            // Test_DnBtoNBgetTokenResponseTransformation();

            //get credit request
            //  Transform_GettCreditRequest();

            //SetEnvandEndPoint();

            //string val= TestMessageCodes();
            #region commented
            //string jsonPath = @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Json_GetToken.txt";

            //using (StreamReader reader = new StreamReader(jsonPath))
            //{
            //    string json = reader.ReadToEnd();
            //    var resultGetToken = JsonConvert.DeserializeObject<GetToken>(json);
            //   string tokenFound= resultGetToken.AuthenticationDetail.Token;
            //}
            //jsonPath = @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\json_smaple.txt";

            //using (StreamReader reader = new StreamReader(jsonPath))
            //{
            //    string json = reader.ReadToEnd();
            //    var resultGetToken = JsonConvert.DeserializeObject<FindCompanyResponse>(json);
            //    Matchcandidate[] tokenFound = resultGetToken.GetCleanseMatchResponse.MatchResponse.MatchResponseDetail.MatchCandidate;
            //}

            // MatchResponseDetail
            //"DUNSNumber": "804735132",
            //"OrganizationName": {"$": "GORMAN MANUFACTURING COMPANY, INC."
            //"StreetAddressLine": [{"LineText": "492 KOLLER ST"}],
            //     "PrimaryTownName": "SAN FRANCISCO",
            //   "CountryISOAlpha2Code": "US",
            //   "PostalCode": "94110",
            //  "TerritoryAbbreviatedName": "CA",
            //FullPostalCode
            // TelephoneNumber

            //var directory = @"C:\Development_code_Docs\Testing_Files\2018\February2018\feb21_2018_new_DC_MM_Schema\";//C:\DCTestFiles\";
            //var filename = "GMC_P00080013_NewSchemaFeb21_Request.xml"; 

            //// load XML
            //string xmlFileName = directory + filename;
            //string sourceXmlMessage = NBXmlHelper.LoadXmlFromFile(directory + filename);         
            //string xsdFile = @"C:\TFS\NBFC\Integration\NeuronServices\MidMarket\NeuronConfig\schema\NB-DocGen-v8.xsd";            
            //string xmlValidation = ValidateXmlString(sourceXmlMessage, xsdFile);          

            //if (!string.IsNullOrEmpty(xmlValidation))
            //{
            //    //errors
            //}

            //var sourceXml = NBXmlHelper.LoadXmlFromFile(directory + filename);
            //var sourceObj = NBXmlSerializer.Deserialize(sourceXml, typeof(FindCompanyRq_Type)) as FindCompanyRq_Type;
            //FindCompanyRequest targetObj = new FindCompanyRequest();

            ////using (NBtoDnBRqTransformer transformer = new NBtoDnBRqTransformer(sourceObj, targetObj))
            ////{
            ////     transformer.Transform(sourceObj, targetObj);
            ////}

            //var targetXML = NBXmlSerializer.Serialize(targetObj);
            //Assert.IsNotNull(targetXML);

            //targetXML = ExtractAndProcessXml(targetXML);
            //NBXmlHelper.StoreToXmlFile(targetXML, directory + @"MidMarket_Test_" + GetFileName(filename) + "_" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".xml");

            #endregion
        }

        //private void SetEnvandEndPoint()
        //{

        //    string envvar = ConfigurationManager.AppSettings.Get("env");

        //    //string uid =   "svc_d_devops_esb ";
        //    //string pwd =  "7Fa7k3*66R4FqYpa";
        //    //string mainDom = "nbfc";


        //    switch (envvar)
        //    {

        //        case "DEV":
        //            envvar = ConfigurationManager.AppSettings.Get("dnb_dev");
        //            break;

        //        case "ASM":
        //            envvar = ConfigurationManager.AppSettings.Get("dnb_asm");
        //            break;

        //        case "QA":
        //            envvar = ConfigurationManager.AppSettings.Get("dnb_qa");
        //            break;

        //        case "UAT":
        //            envvar = ConfigurationManager.AppSettings.Get("dnb_uat");
        //            break;

        //        default:
        //            break;

        //    }

        //  //  LoadTest();
        //}

        //private void LoadTest()
        //{
        //    string req = string.Empty;

        //    //https://stackoverflow.com/questions/859224/calling-a-webservice-from-behind-a-proxy-server

        //    using (VINWebClientESB esbWebClient = new VINWebClientESB(esbClientUrl, clientAccountID, clientAccountPW, clientDomain))
        //    {
        //        esbWebClient.SetProxy("http://10.1.177.38:80");

        //        //soapResponse = esbWebClient.SendReplySecure(soapRequest, "test");
        //        soapResponse = esbWebClient.SendReplySecureNTLM(soapRequest);
        //        Assert.IsNotNull(soapResponse);

        //        string respmsg = esbWebClient.GetResponseMessage("GetVehicleDataRs", soapResponse);

               

        //    }
        //}

        private void Test_loadCSS_FROMDB()
        {
            AppServices a = new AppServices("Data Source=TOANSQL01.nbfc.com;Database=EnterpriseSharedData;User ID=nbfc\\svc_d_devops_esb;Password=7Fa7k3*66R4FqYpa;Integrated Security=True;");

            string reqID = "111111111111111111";
            string respID = System.Guid.NewGuid().ToString();
            string sessionID = "vxvxcv";
            string dunfrom = "111";
            string compnoreq = "sdfds";
            string result = a.BuildDnBResponseFromDB("804735132", reqID, respID, sessionID);
        }

        private void TestIncomeRequestFromNB()
        {

            AppServices app = new AppServices("Data Source=TOANSQL01.nbfc.com;Database=EnterpriseSharedData;User ID=nbfc\\svc_d_devops_esb;Password=7Fa7k3*66R4FqYpa;Integrated Security=True;");
            //  Data Source = TOANSQL01.nbfc.com; Database = EnterpriseSharedData; User ID = nbfc\\svc_d_devops_esb; Password = 7Fa7k3*66R4FqYpa; Integrated Security = True;
            string s = @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Sample_TXT_Files\findcompany_empty_result_sample.xml";// sample_FindCompany_Test.xml";

            //TransformNBReqToDnBRequestParam

           // var sourceXml = NBXmlHelper.LoadXmlFromFile(s);
            string errors = string.Empty;
            string restParam = string.Empty;
            string dun = "";



            var sourceXml = NBXmlHelper.LoadXmlFromFile(s);
            bool isTransformed = app.TransformNBReqToDnBRequestParam(sourceXml, ref errors, ref restParam);

          
          var sourceObj = NBXmlSerializer.Deserialize(sourceXml, typeof(FindCompanyRq_Type)) as FindCompanyRq_Type;

            NBXmlHelper.StoreToXmlFile(sourceXml.ToString(), @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Sample_TXT_Files\findcompany_empty_result_sample_Response.xml");
        }

        private string TestMessageCodes()
        {
            string val= DnBResponseMeessageCodes_Dict.GetMessageBykey("SC001");
            return val;
        }




        //public List<mdm_VwVinlink> GetVehicleData(mdm_VwVinlink reqVehicleData)
        //{


        //    List<mdm_VwVinlink> vehicleDataItems = null;

        //    if (reqVehicleData != null)
        //    {
        //        // lookup by vehicle code first
        //        if (reqVehicleData.VehicleCd != null)
        //        {
        //            vehicleDataItems = VehicleDataRepository.Get(veh => veh.VehicleCd == reqVehicleData.VehicleCd, null, string.Empty).ToList();
        //        }

        //        // lookup by extended vehicle code first
        //        if (reqVehicleData.ExtendedVehicleCd != null)
        //        {
        //            vehicleDataItems = VehicleDataRepository.Get(veh => veh.ExtendedVehicleCd == reqVehicleData.ExtendedVehicleCd, null, string.Empty).ToList();
        //        }

        //private void Testit()
        //{
        //    AppServices app = new AppServices("Data Source=TOANSQL01.nbfc.com;Database=EnterpriseSharedData;User ID=nbfc\\svc_d_devops_esb;Password=7Fa7k3*66R4FqYpa;Integrated Security=True;");

        //   long t= app.GetCCSValue(long.Parse("804735132"));
        //}
        //private void Test_DBSave_CCS()
        //{

        //    Party p = new Party();
        //    //p.PartyKey = "idnentity" // leave itempty
        //    p.CountrySubdivisionCd = "sadas";//----------is this fro  ODS too
        //    p.PartyAddressLine1Txt = "101 yellowood cir";
        //    p.PartyCityNm = "chocago";
        //    p.PartyPostalCd = "1234537";
        //    p.PartyNm = "dsadas";
           
             
        //    DateTime date = DateTime.ParseExact("2014-02-15", "yyyy-MM-dd", CultureInfo.InvariantCulture);

        //    PartyCreditScore pccs = new PartyCreditScore();
        //    pccs.CreditScoreDate = date;
        //    pccs.CreditScoreTxt = "0.08";
        //    //pccs.PartyKey = "idnentity" // leave itempty
        //    pccs.VendorPartyId = long.Parse("804735132");//DUNS NUmber

        //    pccs.CreditScoreTypeCd = "we";    //[CREDIT_SCORE_TYPE_CD], [CREDIT_SCORE_TYPE_CD]     From NBMasterDataServices.MDM._VW_CREDIT_SCORE_VENDOR      
        //    pccs.CreditScoreVendorCd = "sdasd";//[CREDIT_SCORE_VENDOR_CD] ,  [CREDIT_SCORE_VENDOR_CD]  From NBMasterDataServices.MDM._VW_CREDIT_SCORE_VENDOR

        //    string dbcon = "Data Source=TOANSQL01.nbfc.com;Database=EnterpriseSharedData;User ID=nbfc\\svc_d_devops_esb;Password=7Fa7k3*66R4FqYpa;Integrated Security=True;";
        //    AppServices app = new AppServices(dbcon);

        //    app.SaveDnBCreditData(p, pccs, dbcon);
        //}
         
        private void Call_DnBtoNB_CreditScore_ResponseTransformation()
        {
            try
            {


                string jsonPath = @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Sample_TXT_Files\dun_april30.txt";// Test_DoubInsert_Party.txt";// error_DUNS_NO_DATA_march29.txt";// PPR_CCS_V9_SAMPLE_March17.txt"; -- PPR_CCS_V9_SAMPLE_March17.txt--GET_CCS_sample2.txt";// json_FindCompany.txt";
            AppServices a = new AppServices("Data Source=TOANSQL01.nbfc.com;Database=EnterpriseSharedData;User ID=nbfc\\svc_d_devops_esb;Password=7Fa7k3*66R4FqYpa;Integrated Security=True;");
            string json = string.Empty;
            using (StreamReader reader = new StreamReader(jsonPath))
            {
                json = reader.ReadToEnd();

            }
            string targetNB = string.Empty;

            string reqID = "111111111111111111";
            string respID =System.Guid.NewGuid().ToString();
            string sessionID = "vxvxcv";
            string dunfrom = "111";
            string compnoreq = "sdfds";
            string result = a.TransformDnBtoNBCreditScoreResponse(dunfrom, compnoreq, json, reqID, respID, sessionID, ref targetNB);

            NBXmlHelper.StoreToXmlFile(result, @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Sample_TXT_Files\GET_CCS_sample_Transformed2.xml");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void Test_DnBtoNBgetTokenResponseTransformation()
        {
            string jsonPath = @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Json_GetToken.txt";// json_FindCompany.txt";
            AppServices a = new AppServices("sdfsf");
            string json = string.Empty;
            using (StreamReader reader = new StreamReader(jsonPath))
            {
                json = reader.ReadToEnd();
               
            }
            string reqID = "111111111111111111";
            string respID = System.Guid.NewGuid().ToString();
            string sessionID = "vxvxcv";
            string targetNB = string.Empty;
            string  dunfrom   = "111";
            string compnoreq = "sdfds";
            string result = a.TransformDnBtoNBCreditScoreResponse(dunfrom, compnoreq, json, reqID, respID, sessionID, ref targetNB);

            NBXmlHelper.StoreToXmlFile(result, @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Unittesting_DnBService\dnbToNBRespCreditTScore.xml");
        }
        private void Test_DnBtoNB_FIND_Compnay_ResponseTransformation()
        {
            string jsonPath = @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Sample_TXT_Files\sept09_erro_investidation.txt";// findcompany_empty_result_sample_Response.txt";// Menakshi_test_March28.txt";// GetCleansematchresponse.txt";// json_FindCompany.txt";
            AppServices a = new AppServices("sdfsf");

            string jsonresp = string.Empty;//
            using (StreamReader reader = new StreamReader(jsonPath))
            {
                jsonresp = reader.ReadToEnd();

              //  var resultFindCompany = JsonConvert.DeserializeObject<FindCompanyResponse>(jsonresp);


                //Findcandidate[] companiesFound = resultFindCompany.FindCompanyResponse.FindCompanyResponseDetail.FindCandidate;

                //if (companiesFound.Length > 0)
                //{
                //    //great
                //}
            }
            string targetNB = string.Empty;
            string reqID = "111111111111111111";
            string respID = System.Guid.NewGuid().ToString();
            string sessionID = "vxvxcv";
            string result = a.TransformDnBtoNBCompnayResponse(jsonresp, reqID, respID, sessionID, ref targetNB);
            NBXmlHelper.StoreToXmlFile(result, @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Sample_TXT_Files\findcompany_empty_result_sample_Response_ON_NOTFOUND.xml");
        }
        private bool Transform_GettCreditRequest()
        {
            AppServices app = new AppServices("Data Source=TOANSQL01.nbfc.com;Database=EnterpriseSharedData;User ID=nbfc\\svc_d_devops_esb;Password=7Fa7k3*66R4FqYpa;Integrated Security=True;");
                                            //  Data Source = TOANSQL01.nbfc.com; Database = EnterpriseSharedData; User ID = nbfc\\svc_d_devops_esb; Password = 7Fa7k3*66R4FqYpa; Integrated Security = True;
            string s = @"C:\Development_code_Docs\NBFC_Neuron_Solution_designs\DandB_service\Sample_TXT_Files\ESB_GetCreditRq.xml";// sample_FindCompany_Test.xml";

            var sourceXml = NBXmlHelper.LoadXmlFromFile(s);
            string errors = string.Empty;
            string restParam = string.Empty;
            string dun = "804735132";
        //   bool istrue= app.IsCCSInDB(dun);

         //  Dictionary<string,string> sdasd = app.GetCCSInDB(dun);


         

          bool isTransformed = app.TransformNBtoDnBCreditScoreRequest("250282535","US", ref errors, ref restParam);
            //var sourceXml = NBXmlHelper.LoadXmlFromFile(s);
            // var sourceObj = NBXmlSerializer.Deserialize(sourceXml, typeof(FindCompanyRq_Type)) as FindCompanyRq_Type;

            return true;
        }


       

        public string ValidateXmlString(string xmlString, string xmlSchemaPath)
        {
            string validationResult = string.Empty;
            StringReader xmlStringReader = null;

            try
            {
                xmlStringReader = new StringReader(xmlString);
                XmlTextReader xmlReader = new XmlTextReader(xmlStringReader);

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(null, xmlSchemaPath);
                settings.ValidationFlags = XmlSchemaValidationFlags.ProcessSchemaLocation;
                settings.ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings;

                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XmlReader.Create(xmlReader, settings));



            }
            catch (Exception e)
            {
                // log can be added
                //validationResult = false;
                validationResult = e.Message;
            }
            finally
            {
                xmlStringReader.Close();
            }

            return validationResult;

        }

        private string ValidateXmlFile(string xmlFileName, string xmlSchemaPath)
        {
            bool validationResult = false;
            FileStream xmlFileStream = null;
            System.Xml.XmlTextReader xmlReader = null;


            try
            {
                xmlFileStream = File.Open(xmlFileName, FileMode.Open);
                xmlReader = new System.Xml.XmlTextReader(xmlFileStream);

                System.Xml.XmlReaderSettings settings = new System.Xml.XmlReaderSettings();
                settings.ValidationType = System.Xml.ValidationType.Schema;
                settings.Schemas.Add(null, xmlSchemaPath);
                settings.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.ProcessSchemaLocation;
                settings.ValidationFlags = System.Xml.Schema.XmlSchemaValidationFlags.ReportValidationWarnings;

                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);

                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.Load(System.Xml.XmlReader.Create(xmlReader, settings));

                validationResult = true;
            }
            catch (Exception ex)
            {
                // log can be added
                validationResult = false;
            }
            finally
            {
                xmlFileStream.Close();
                xmlReader.Close();
            }

            return string.Empty;

        }
        private void ValidationCallback(object sender, System.Xml.Schema.ValidationEventArgs args)
        {



            if (args.Severity == System.Xml.Schema.XmlSeverityType.Error)
            {


                string smlError = args.Severity + " Message: " + args.Message;

                //throw new Exception("ValidationCallback - Error: " + args.Message);
            }



        }
        
        private string ExtractAndProcessXml(string targetXML)
        {
            string startTag = "<value>non-encripted data</value></attributes><document_StringBase64>";
            string endTag = "</document_StringBase64>";

            int pFrom = targetXML.IndexOf(startTag) + startTag.Length;
            int pTo = targetXML.LastIndexOf(endTag);

            targetXML = targetXML.Substring(pFrom, pTo - pFrom);

            targetXML = targetXML.Replace("&lt;", "<");
            targetXML = targetXML.Replace("&gt;", ">");

            //Remove tag
            string tag1 = "]]>";
            string tag2 = "<![CDATA[";

            targetXML = targetXML.Replace(tag1, "");
            targetXML = targetXML.Replace(tag2, "");

            return targetXML;
        }

        private string GetFileName(string fileName)
        {
            if (fileName.IndexOf(".") > 0)
            {
                return fileName.Substring(0, fileName.IndexOf("."));
            }

            return fileName;
        }

     

    }
}
