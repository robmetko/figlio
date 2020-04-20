using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSi.DnB.Schemas.Service
{

    public class Transactiondetail
    {
        public string ApplicationTransactionID { get; set; }
        public string ServiceTransactionID { get; set; }
        public DateTime TransactionTimestamp { get; set; }
    }

    public class Transactionresult
    {
        public string SeverityText { get; set; }
        public string ResultID { get; set; }
        public string ResultText { get; set; }
    }

    public class Organizationprimaryname
    {
        public Organizationname OrganizationName { get; set; }
    }

    public class Organizationname
    {
        public string _ { get; set; }
    }

    public class Primaryaddress
    {
        public Streetaddressline[] StreetAddressLine { get; set; }
        public string PrimaryTownName { get; set; }
        public string CountryISOAlpha2Code { get; set; }
        public string PostalCode { get; set; }
        public string PostalCodeExtensionCode { get; set; }
        public string TerritoryOfficialName { get; set; }
        public string TerritoryAbbreviatedName { get; set; }
        public string CountryOfficialName { get; set; }
        public string[] MetropolitanStatisticalAreaUSCensusCode { get; set; }
    }

    public class Streetaddressline
    {
        public string LineText { get; set; }
    }

    public class Telephonenumber
    {
        public string TelecommunicationNumber { get; set; }
    }

}
