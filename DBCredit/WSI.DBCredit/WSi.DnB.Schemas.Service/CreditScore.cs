


namespace WSi.DnB.Schemas.Service.CompanyCC
{


    public class CreditScore
    {
        public Orderproductresponse OrderProductResponse { get; set; }
    }

    public class Orderproductresponse
    {
        public string ServiceVersionNumber { get; set; }
        public Transactiondetail TransactionDetail { get; set; }
        public Transactionresult TransactionResult { get; set; }
        public Orderproductresponsedetail OrderProductResponseDetail { get; set; }
    }

    public class Transactiondetail
    {
        public string ServiceTransactionID { get; set; }
        public System.DateTime TransactionTimestamp { get; set; }
    }

    public class Transactionresult
    {
        public string SeverityText { get; set; }
        public string ResultID { get; set; }
        public string ResultText { get; set; }
    }

    public class Orderproductresponsedetail
    {
        public Inquirydetail InquiryDetail { get; set; }
        public Product Product { get; set; }
    }

    public class Inquirydetail
    {
        public string DUNSNumber { get; set; }
        public string CountryISOAlpha2Code { get; set; }
    }

    public class Product
    {
        public string DNBProductID { get; set; }
        public Organization Organization { get; set; }
        public Archivedetail ArchiveDetail { get; set; }
    }

    public class Organization
    {
        public Subjectheader SubjectHeader { get; set; }
        public Telecommunication Telecommunication { get; set; }
        public Location Location { get; set; }
        public Financial Financial { get; set; }
        public Organizationname OrganizationName { get; set; }
        public Organizationdetail OrganizationDetail { get; set; }
        public Activitiesandoperations ActivitiesAndOperations { get; set; }
        public Employeefigures EmployeeFigures { get; set; }
        public Assessment Assessment { get; set; }
        public Businesstrading BusinessTrading { get; set; }
        public Principalsandmanagement PrincipalsAndManagement { get; set; }
    }

    public class Subjectheader
    {
        public string DUNSNumber { get; set; }
    }

    public class Telecommunication
    {
        public Telephonenumber[] TelephoneNumber { get; set; }
    }

    public class Telephonenumber
    {
        public string TelecommunicationNumber { get; set; }
        public string InternationalDialingCode { get; set; }
    }

    public class Location
    {
        public Primaryaddress[] PrimaryAddress { get; set; }
    }

    public class Primaryaddress
    {
        public Streetaddressline[] StreetAddressLine { get; set; }
        public string PrimaryTownName { get; set; }
        public string CountryISOAlpha2Code { get; set; }
        public string TerritoryAbbreviatedName { get; set; }
        public string PostalCode { get; set; }
        public string TerritoryOfficialName { get; set; }
    }

    public class Streetaddressline
    {
        public string LineText { get; set; }
    }

    public class Financial
    {
        public Financialstatement[] FinancialStatement { get; set; }
    }

    public class Financialstatement
    {
        public Balancesheet BalanceSheet { get; set; }
    }

    public class Balancesheet
    {
        public Liabilities Liabilities { get; set; }
    }

    public class Liabilities
    {
        public Totalequityamount TotalEquityAmount { get; set; }
    }

    public class Totalequityamount
    {
        public int DNBCodeValue { get; set; }
        public int _ { get; set; }
    }

    public class Organizationname
    {
        public Organizationprimaryname[] OrganizationPrimaryName { get; set; }
        public Donotconfuseorganizationname[] DoNotConfuseOrganizationName { get; set; }
    }

    public class Organizationprimaryname
    {
        public Organizationname1 OrganizationName { get; set; }
    }

    public class Organizationname1
    {
        public string valueHolder { get; set; }
    }

    public class Donotconfuseorganizationname
    {
        public Organizationname2 OrganizationName { get; set; }
    }

    public class Organizationname2
    {
        public string orgName2 { get; set; }
    }

    public class Organizationdetail
    {
        public Familytreememberrole[] FamilyTreeMemberRole { get; set; }
        public Controlownershipdate ControlOwnershipDate { get; set; }
        public string OrganizationStartYear { get; set; }
    }

    public class Controlownershipdate
    {
        public string valueHolder { get; set; }
    }

    public class Familytreememberrole
    {
        public Familytreememberroletext FamilyTreeMemberRoleText { get; set; }
    }

    public class Familytreememberroletext
    {
        public int DNBCodeValue { get; set; }
        public string valueHolder { get; set; }
    }

    public class Activitiesandoperations
    {
        public Lineofbusinessdetail[] LineOfBusinessDetails { get; set; }
    }

    public class Lineofbusinessdetail
    {
        public Lineofbusinessdescription LineOfBusinessDescription { get; set; }
    }

    public class Lineofbusinessdescription
    {
        public int LanguageCode { get; set; }
        public string valueHolder { get; set; }
    }

    public class Employeefigures
    {
        public Headquarterslocationemployeedetails HeadquartersLocationEmployeeDetails { get; set; }
        public Consolidatedemployeedetails ConsolidatedEmployeeDetails { get; set; }
    }

    public class Headquarterslocationemployeedetails
    {
        public int TotalEmployeeQuantity { get; set; }
    }

    public class Consolidatedemployeedetails
    {
        public int TotalEmployeeQuantity { get; set; }
    }

    public class Assessment
    {
        public Commercialcreditscore[] CommercialCreditScore { get; set; }
    }

    public class Commercialcreditscore
    {
        public int RawScore { get; set; }
        public string ClassScore { get; set; }
        public string ClassScoreDescription { get; set; }
        public int NationalPercentile { get; set; }
        public Scoredate scoreDate { get; set; }
    }

    public class Scoredate
    {
        public string valueHolder { get; set; }
    }

    public class Businesstrading
    {
        public Purchaser Purchaser { get; set; }
    }

    public class Purchaser
    {
        public Purchaserderiveddata[] PurchaserDerivedData { get; set; }
    }

    public class Purchaserderiveddata
    {
        public Twentyfourmonthsdatacoveragepayments TwentyFourMonthsDataCoveragePayments { get; set; }
    }

    public class Twentyfourmonthsdatacoveragepayments
    {
        public Paymentperiodsummary[] PaymentPeriodSummary { get; set; }
    }

    public class Paymentperiodsummary
    {
        public string SummaryPeriod { get; set; }
        public Paymentswithhighcredit PaymentsWithHighCredit { get; set; }
    }

    public class Paymentswithhighcredit
    {
        public Averagehighcreditamount AverageHighCreditAmount { get; set; }
    }

    public class Averagehighcreditamount
    {
        public int valueHolder { get; set; }
    }

    public class Principalsandmanagement
    {
        public Mostseniorprincipal MostSeniorPrincipal { get; set; }
    }

    public class Mostseniorprincipal
    {
        public Principalname PrincipalName { get; set; }
        public Position[] Position { get; set; }
    }

    public class Principalname
    {
        public string type { get; set; }
        public string FullName { get; set; }
    }

    public class Position
    {
        public Positiontext PositionText { get; set; }
    }

    public class Positiontext
    {
        public string valueHolder { get; set; }
    }

    public class Archivedetail
    {
        public int PortfolioAssetID { get; set; }
    }

}


/* classes above are based on sample below
{
    "OrderProductResponse": {
        "@ServiceVersionNumber": "5.0",
        "TransactionDetail": {
            "ServiceTransactionID": "Id-827df70252ff384907bb3a53-2",
            "TransactionTimestamp": "2014-02-15T04:50:02.425-05:00"
        },
        "TransactionResult": {
            "SeverityText": "Information",
            "ResultID": "CM000",
            "ResultText": "Success"
        },
        "OrderProductResponseDetail": {
            "InquiryDetail": {
                "DUNSNumber": "804735132",
                "CountryISOAlpha2Code": "US"
            },
            "Product": {
                "DNBProductID": "PPR_CCS_V9",
                "Organization": {
                    "SubjectHeader": {
                        "DUNSNumber": "804735132"
                    },
                    "Telecommunication": {
                        "TelephoneNumber": [
                            {
                                "TelecommunicationNumber": "(650) 555-0000",
                                "InternationalDialingCode": "1"
                            }
                        ]
                    },
                    "Location": {
                        "PrimaryAddress": [
                            {
                                "StreetAddressLine": [
                                    {
                                        "LineText": "492 Koller St"
                                    }
                                ],
                                "PrimaryTownName": "San Francisco",
                                "CountryISOAlpha2Code": "US",
                                "TerritoryAbbreviatedName": "CA",
                                "PostalCode": "94110",
                                "TerritoryOfficialName": "California"
                            }
                        ]
                    },
                    "Financial": {
                        "FinancialStatement": [
                            {
                                "BalanceSheet": {
                                    "Liabilities": {
                                        "TotalEquityAmount": {
                                            "@DNBCodeValue": 3047,
                                            "$": 1180200
                                        }
                                    }
                                }
                            }
                        ]
                    },
                    "OrganizationName": {
                        "OrganizationPrimaryName": [
                            {
                                "OrganizationName": {
                                    "$": "Gorman Manufacturing Company, Inc."
                                }
                            }
                        ],
                        "DoNotConfuseOrganizationName": [
                            {
                                "OrganizationName": {
                                    "$": "other Gorman companies, this is a fictitious company used by D for demonstration purposes"
                                }
                            }
                        ]
                    },
                    "OrganizationDetail": {
                        "FamilyTreeMemberRole": [
                            {
                                "FamilyTreeMemberRoleText": {
                                    "@DNBCodeValue": 12775,
                                    "$": "Global Ultimate"
                                }
                            },
                            {
                                "FamilyTreeMemberRoleText": {
                                    "@DNBCodeValue": 12773,
                                    "$": "Parent"
                                }
                            },
                            {
                                "FamilyTreeMemberRoleText": {
                                    "@DNBCodeValue": 12774,
                                    "$": "Domestic Ultimate"
                                }
                            }
                        ],
                        "ControlOwnershipDate": {
                            "$": "1985"
                        },
                        "OrganizationStartYear": "1985"
                    },
                    "ActivitiesAndOperations": {
                        "LineOfBusinessDetails": [
                            {
                                "LineOfBusinessDescription": {
                                    "@LanguageCode": 39,
                                    "$": "Lithographic commercial printing"
                                }
                            }
                        ]
                    },
                    "EmployeeFigures": {
                        "HeadquartersLocationEmployeeDetails": {
                            "TotalEmployeeQuantity": 110
                        },
                        "ConsolidatedEmployeeDetails": {
                            "TotalEmployeeQuantity": 125
                        }
                    },
                    "Assessment": {
                        "CommercialCreditScore": [
                            {
                                "RawScore": 419,
                                "ClassScore": "5",
                                "ClassScoreDescription": "HIGH RISK",
                                "NationalPercentile": 8,
                                "ScoreDate": {
                                    "$": "2014-02-15"
                                }
                            }
                        ]
                    },
                    "BusinessTrading": {
                        "Purchaser": {
                            "PurchaserDerivedData": [
                                {
                                    "TwentyFourMonthsDataCoveragePayments": {
                                        "PaymentPeriodSummary": [
                                            {
                                                "SummaryPeriod": "P24M",
                                                "PaymentsWithHighCredit": {
                                                    "AverageHighCreditAmount": {
                                                        "$": 3440
                                                    }
                                                }
                                            }
                                        ]
                                    }
                                }
                            ]
                        }
                    },
                    "PrincipalsAndManagement": {
                        "MostSeniorPrincipal": {
                            "PrincipalName": {
                                "@type": "ass:IndividualNameType",
                                "FullName": "Leslie Smith"
                            },
                            "Position": [
                                {
                                    "PositionText": {
                                        "$": "Pres"
                                    }
                                }
                            ]
                        }
                    }
                },
                "ArchiveDetail": {
                    "PortfolioAssetID": 47648406
                }
            }
        }
    }
}
 
     */
