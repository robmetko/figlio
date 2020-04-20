using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WSI.DnB.Schemas.NBClients.ContractTypes;
using WSI.Common.XML;

namespace WSI.DnBCreditService.ApplicationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetCompany_tets()
        {
            // create request             
            FindCompanyRq_Type getComp = new FindCompanyRq_Type();


            getComp.ReqHeader = new ReqHeader_Type
            {
                ReqID = Guid.NewGuid().ToString(),
                LogUserID = @"nbfc\nbitrcm",
                SystemName = "DuckCreek",
                Language = "EN"

            };

            getComp.Company.Address = new Address_Type
            {
               Country="US",
                Province="CA",
                StreetAddress="101 yellowood"                
            };


            string getVehicleDataRqXml = NBXmlSerializer.Serialize(getComp);

            // get vehicle data from app service
            //string dbConnectionString = ConfigurationManager.AppSettings.Get("VINRepositoryConnectionString");

            //VINApplicationService vinAppService = new VINApplicationService(dbConnectionString);

            //GetVehicleDataRs_Type getVehicleDataRs = vinAppService.GetVehicleData(getVehicleDataRq);
            //Assert.IsNotNull(getVehicleDataRs);

           // string getVehicleDataRsXml = NBXmlSerializer.Serialize(getVehicleDataRs);

        }
    }
}
