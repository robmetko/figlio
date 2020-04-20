using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSI.DnBCredit.Transformation
{
   public class DnBResponseMeessageCodes_Dict
    {
        public static string GetMessageBykey(string keyVal)
        {

            #region Msg Codes
            Dictionary<string, string> messageCode = new Dictionary<string, string>();
            messageCode.Add("CM000", "Success"); //200
            messageCode.Add("CM001", "The given request is invalid.");// 400
            messageCode.Add("CM002", "The format of this request is invalid.");// //400
            messageCode.Add("CM003", "Missing information in the request.");//// 400
            messageCode.Add("CM004", "Internal D & B system error. ");////500 * *
            messageCode.Add("CM005", "D & B System temporarily unavailable.");// //503
            messageCode.Add("CM006", "D & B System busy.");// 503
            messageCode.Add("CM007", " Your request cannot be fulfilled for the given criteria.");// //404
            messageCode.Add("CM008", "No match found for the requested Duns number.");//// 404
            messageCode.Add("CM009", " Reason Code required for Germany.");// //400
            messageCode.Add("CM010", "Partial Success. ");////200
            messageCode.Add("CM011", "Invalid Country Code provided in the request.");// //400
            messageCode.Add("CM012", "Invalid Duns number provided in the request.");//// 400
            messageCode.Add("CM013", "Invalid Subject ID provided in the request.");//// 400
            messageCode.Add("CM014", "Invalid Product Code provided in the request.");// //400
            messageCode.Add("CM016", "Invalid Territory provided in the request.");// //400
            messageCode.Add("CM017", " Duns number is missing in the request.");// //400
            messageCode.Add("CM018", "No candidates resulted for the given input criteria. ");////404
            messageCode.Add("CM022", "Only a Subset of request parameters were used for processing the request. ");// //200
            messageCode.Add("CM023", "Duplicate record cannot be created.");// 400
            messageCode.Add("CM026", "Request accepted.Processing pending.");// //200
            messageCode.Add("CP001", "No Match for the given input criteria. ");// //404
            messageCode.Add("CP002", " No candidates resulted in the Extended Search");//
             messageCode.Add("CP004", " No Match for given Organization Identifier. ");////404
            messageCode.Add("CP005", " Insufficient Information to determine Fraud Score.");//// 400
            messageCode.Add("PD001", "Requested product not available due to insufficient data.");//// 404
            messageCode.Add("PD002", "Requested subject is promised later.Preliminary report returned. ");////200
            messageCode.Add("PD003", " Requested product not available - subject is on Stop Distribution.");// //404
            messageCode.Add("PD004", "Requested product not available due to subject information is too old.");//// 404
            messageCode.Add("PD005", " Requested Product not available.");//// 404
            messageCode.Add("PD006", " Subject is foreign branch, therefore trade - up to HQ is not available.");//// 404
            messageCode.Add("PD007", "Family Tree does not exist for requested subject.");//// 404
            messageCode.Add("PD008", "Success.Returned truncated family tree, as family tree member count exceeds maximum limit for requested subject.");//// 200
            messageCode.Add("PD009", " Required scoring elements missing - a Commercial or Blended Small Business report is not available.You may proceed with a Principal only request.");///// 200
            messageCode.Add("PD010", " Requested Product not available as address is undeliverable");//.// 200
            messageCode.Add("PD011", "Requested Product not available as address is undeliverable.");// //404
            messageCode.Add("PD012", " Requested Product not available on Self request.");// //404
            messageCode.Add("PD013", "Requested Product is not available currently.Product will be delivered to the customer at a later point of time via offline delivery channels(Email / FAX / FTP etc).");// 404
            messageCode.Add("PD014", "Requested data is not available or partially available due to data integrity errors.");// 404
            messageCode.Add("PD015", "Trade - up to HQ is not available.Therefore delivering the data for branch organization.");// 200
            messageCode.Add("PD016", "Requested Product is not available since the requested DUNS is deleted or transferred to a new DUNS.");// 200
            messageCode.Add("PD019", "The requested product is not available because the product size exceeds the maximum limit.Please use the compressed product attachment option for pulling a product of large size.");//  200
            messageCode.Add("PD020 ", "No Match found during screening.");// 404
            messageCode.Add("PD021", " Potential Match found during screening; Review required.");// 200
            messageCode.Add("SC001", "Your user credentials are invalid.");// 401
            messageCode.Add("SC002", " Your user credentials are not eligible for this request.");// 401
            messageCode.Add("SC003", "Your user credentials have expired.");// 401
            messageCode.Add("SC004", "Your Subscriber number has expired. ");//401
            messageCode.Add("SC005", "You have reached maximum limit permitted as per the contract. ");//401
            messageCode.Add("SC006", " Transaction not processed as the permitted concurrency limit was exceeded.");// 401
            messageCode.Add("SC007", "Subscriber validation failed. ");//401
            messageCode.Add("SC008", "Your account has been locked out due to repeated attempts to login with an incorrect User ID / Password.  ");//401
            messageCode.Add("SC009", " Invalid Sign On Token in the request.");// 401
            messageCode.Add("SC010", "The User ID you provided doesn’t exist in the system.n / a");//
            messageCode.Add("SC011", "The User ID you provided already exist in the system n / a");//
            messageCode.Add("SC012", "Request not processed as user activation key is invalid or expired.n / a");//
            messageCode.Add("SC014", "Your user credentials are not eligible for this request since you are in trial period. ");// 401
            messageCode.Add("BC001", "Partial Success.One or more records in the input file were not processed. ");//200
            messageCode.Add("BC002", "Batch request is not processed since input file is not available in the specified location.");// 400
            messageCode.Add("BC003", "Batch request is not processed since the content of the input file is not in the expected format.");// 400
            messageCode.Add("BC004", " Batch request is not processed since the count of the records specified in the request do not match the exact count of records in the input file.");// 400
            messageCode.Add("BC005", "Intermediate Success ");//200
            messageCode.Add("BC006", " Batch request is not processed ");// 400
            messageCode.Add("BC007", "Batch Execution in progress n/ a");//
            messageCode.Add("BC008", "Batch Execution not yet started n / a");//
            messageCode.Add("BC009", "Batch not processed as the input file contains more records than the permitted limit. ");//400
            messageCode.Add("BC010", " Batch files no longer available after retention period.");// 200
            messageCode.Add("BC011", " Invalid Batch Process Id provided in the request.");// 400
            messageCode.Add("MN001", " Invalid Monitoring Profile ID provided in the request. ");// 400
            messageCode.Add("MN002", "Invalid Notification Profile ID provided in the request. ");// 400
            messageCode.Add("MN003", "Product not registered for Level 2 Monitoring ");//400
            messageCode.Add("MN004", "Cannot delete Monitoring Profile - registrations exist ");//400
            messageCode.Add("MN005", "Cannot delete Notification Profile - registrations exist.");// 400

            #endregion
            return messageCode[keyVal.Trim()].ToString().Trim();
        }
    }
}

//"SeverityText": "Fatal",
//"ResultID": "SC001",
//"ResultText