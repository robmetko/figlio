using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.DnBCredit.Data.Entities
{
   public class Party
    {      
            public long PartyKey { get; set; } // PARTY_KEY (Primary key)
            public string CountrySubdivisionCd { get; set; } // COUNTRY_SUBDIVISION_CD (length: 50)
            public string PartyName { get; set; } // PARTY_NAME (length: 255)
            public string PartyAddressLine1Txt { get; set; } // PARTY_ADDRESS_LINE1_TXT (length: 500)
            public string PartyAddressLine2Txt { get; set; } // PARTY_ADDRESS_LINE2_TXT (length: 500)
            public string PartyCityNm { get; set; } // PARTY_CITY_NM (length: 255)
            public string PartyPostalCode { get; set; } // PARTY_POSTAL_CODE (length: 500)
            public string VendorPartyId { get; set; } // VENDOR_PARTY_ID (length: 50)

        }
}
