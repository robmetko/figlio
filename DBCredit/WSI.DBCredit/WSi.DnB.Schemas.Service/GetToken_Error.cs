using System;
 

namespace WSi.DnB.Schemas.Service
{

    public class Rootobject
    {
        public Transactiondetail TransactionDetail { get; set; }
        public Transactionresult TransactionResult { get; set; }
    }

   

    public class Resultmessage
    {
        public string ResultDescription { get; set; }
    }

}







//https://www.c-sharpcorner.com/article/how-to-paste-json-as-classes-or-xml-as-classes-in-visual-stu/

//Token Management
//SC001 Your user credentials are invalid.Generate a new token once using the Authentication Service.If the same error occurs while generating a new token, please contact D&B Customer Support.
//SC003 Your user credentials have expired.Contact D&B Customer Support
//SC004 Your Subscriber number has expired.Contact D&B Customer Support
//SC005 You have reached maximum limit permitted as per the contract.Contact D&B Customer Support
//SC006 Transaction not processed as the permitted concurrency limit was exceeded. Subsequent requests are blocked when exceeding the set limit and time frame defined per customer contract. Please wait a moment and try again using the same token.
