using System;

namespace WSi.DnB.Schemas.Service.CompanyResponse
{

    public class FindCompanyResponse
    {
        public Getcleansematchresponse GetCleanseMatchResponse { get; set; }
    }

    public class Getcleansematchresponse
    {
        public Transactiondetail TransactionDetail { get; set; }
        public Transactionresult TransactionResult { get; set; }
        public Getcleansematchresponsedetail GetCleanseMatchResponseDetail { get; set; }
    }

    public class Transactiondetail
    {
        public string ServiceTransactionID { get; set; }
        public DateTime TransactionTimestamp { get; set; }
    }

    public class Transactionresult
    {
        public string SeverityText { get; set; }
        public string ResultID { get; set; }
        public string ResultText { get; set; }
    }

    public class Getcleansematchresponsedetail
    {
        public Inquirydetail InquiryDetail { get; set; }
        public Matchresponsedetail MatchResponseDetail { get; set; }
    }

    public class Inquirydetail
    {
        public string SubjectName { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string CountryISOAlpha2Code { get; set; }
        public string TerritoryName { get; set; }
    }

    public class Matchresponsedetail
    {
        public Matchdatacriteriatext MatchDataCriteriaText { get; set; }
        public int CandidateMatchedQuantity { get; set; }
        public Matchcandidate[] MatchCandidate { get; set; }
    }

    public class Matchdatacriteriatext
    {
        public string _ { get; set; }
    }

    public class Matchcandidate
    {
        public string DUNSNumber { get; set; }
        public Organizationprimaryname OrganizationPrimaryName { get; set; }
        public Primaryaddress PrimaryAddress { get; set; }
        public Mailingaddress MailingAddress { get; set; }
        public Telephonenumber TelephoneNumber { get; set; }
        public Operatingstatustext OperatingStatusText { get; set; }
        public Familytreememberrole[] FamilyTreeMemberRole { get; set; }
        public bool StandaloneOrganizationIndicator { get; set; }
        public Matchqualityinformation MatchQualityInformation { get; set; }
        public int DisplaySequence { get; set; }
        public Tradestylename TradeStyleName { get; set; }
    }

    public class Organizationprimaryname
    {
        public Organizationname OrganizationName { get; set; }
    }

    public class Organizationname
    {
        public string orgName { get; set; }
    }

    public class Primaryaddress
    {
        public Streetaddressline[] StreetAddressLine { get; set; }
        public string PrimaryTownName { get; set; }
        public string CountryISOAlpha2Code { get; set; }
        public string PostalCode { get; set; }
        public string TerritoryAbbreviatedName { get; set; }
        public bool UndeliverableIndicator { get; set; }
        public string PostalCodeExtensionCode { get; set; }
    }

    public class Streetaddressline
    {
        public string LineText { get; set; }
    }

    public class Mailingaddress
    {
        public string CountryISOAlpha2Code { get; set; }
        public bool UndeliverableIndicator { get; set; }
        public Streetaddressline1[] StreetAddressLine { get; set; }
        public string PrimaryTownName { get; set; }
        public string PostalCode { get; set; }
        public string TerritoryAbbreviatedName { get; set; }
    }

    public class Streetaddressline1
    {
        public string LineText { get; set; }
    }

    public class Telephonenumber
    {
        public string TelecommunicationNumber { get; set; }
        public bool UnreachableIndicator { get; set; }
    }

    public class Operatingstatustext
    {
        public string _ { get; set; }
    }

    public class Matchqualityinformation
    {
        public int ConfidenceCodeValue { get; set; }
        public Matchbasi[] MatchBasis { get; set; }
        public string MatchGradeText { get; set; }
        public int MatchGradeComponentCount { get; set; }
        public Matchgradecomponent[] MatchGradeComponent { get; set; }
        public string MatchDataProfileText { get; set; }
        public int MatchDataProfileComponentCount { get; set; }
        public Matchdataprofilecomponent[] MatchDataProfileComponent { get; set; }
    }

    public class Matchbasi
    {
        public bool EndIndicator { get; set; }
        public string SubjectTypeText { get; set; }
        public bool SeniorPrincipalIndicator { get; set; }
        public Matchbasistext MatchBasisText { get; set; }
    }

    public class Matchbasistext
    {
        public string _ { get; set; }
    }

    public class Matchgradecomponent
    {
        public Matchgradecomponenttypetext MatchGradeComponentTypeText { get; set; }
        public string MatchGradeComponentRating { get; set; }
        public decimal MatchGradeComponentScore { get; set; }//was int.
    }

    public class Matchgradecomponenttypetext
    {
        public string _ { get; set; }
    }

    public class Matchdataprofilecomponent
    {
        public Matchdataprofilecomponenttypetext MatchDataProfileComponentTypeText { get; set; }
        public string MatchDataProfileComponentValue { get; set; }
    }

    public class Matchdataprofilecomponenttypetext
    {
        public string _ { get; set; }
    }

    public class Tradestylename
    {
        public Organizationname1 OrganizationName { get; set; }
    }

    public class Organizationname1
    {
        public string _ { get; set; }
    }

    public class Familytreememberrole
    {
        public Familytreememberroletext FamilyTreeMemberRoleText { get; set; }
    }

    public class Familytreememberroletext
    {
        public string _ { get; set; }
    }



}
