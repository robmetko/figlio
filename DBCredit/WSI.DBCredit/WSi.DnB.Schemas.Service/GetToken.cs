
using System;
namespace WSi.DnB.Schemas.Service.Token
{ 
public class GetToken
{
    public Transactiondetail TransactionDetail { get; set; }
    public Transactionresult TransactionResult { get; set; }
    public Authenticationdetail AuthenticationDetail { get; set; }
}

public class Transactiondetail
{
    public string ServiceTransactionID { get; set; }
    public DateTime TransactionTimestamp { get; set; }
    public string ApplicationTransactionID { get; set; }
}

public class Transactionresult
{
    public string SeverityText { get; set; }
    public string ResultID { get; set; }
    public string ResultText { get; set; }
    public Resultmessage ResultMessage { get; set; }
}

public class Resultmessage
{
    public string ResultDescription { get; set; }
}

public class Authenticationdetail
{
    public string Token { get; set; }
}
}