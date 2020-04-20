using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.DnBCredit.Data.Entities
{
    public class PartyCreditScore
    {
        public long PartyKey { get; set; } // PARTY_KEY (Primary key)
        public string CreditScoreVendorCd { get; set; } // CREDIT_SCORE_VENDOR_CD (Primary key) (length: 50)
        public string CreditScoreTypeCd { get; set; } // CREDIT_SCORE_TYPE_CD (Primary key) (length: 50)
        public System.DateTime CreditScoreDate { get; set; } // CREDIT_SCORE_DATE (Primary key)
        public string CreditScoreTxt { get; set; } // CREDIT_SCORE_TXT (length: 500)
        public string VendorPartyId { get; set; } // VENDOR_PARTY_ID (length: 50)
        public System.DateTime? CreateTs { get; set; } // CREATE_TS

    }
}
