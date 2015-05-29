
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using BusinessObjects;
using BusinessLayer;
using System.Data;
namespace SAnalayser
{
    public class SendStatusEmail
    {
       
        public static void SendEmail(string fileName)
        {
            SentimentData data = new SentimentData();
            DataSet ds = data.getProperties();
            string email = "";
            foreach(DataRow dr in  ds.Tables[0].Rows)
            {
                if(dr["Property_Name"].ToString() == "Email")
                {
                    email = dr["Property_Value"].ToString();
                    break;
                }
            }
            
            // Command line argument must the the SMTP host.
            SmtpClient client = new SmtpClient("smtp.verizon.com");
            // Specify the e-mail sender. 
            // Create a mailing address that includes a UTF8 character 
            // in the display name.
            MailAddress from = new MailAddress("donotreply@verizon.com",
            "Sentiment Analysis");

            // Set destinations for the e-mail message.
            MailAddress to = new MailAddress(email);
            // Specify the message content.
            MailMessage message = new MailMessage(from, to);
            message.Body = "Sentiment Analysis Status " + fileName;
            // Include some non-ASCII characters in body and subject. 
            
            message.Body += " has been generated. Please click through the below mentioned link.";
            message.Body += Environment.NewLine + "http://localhost:1120/Default.aspx";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "Sentiment Analysis Status " + fileName;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            // Set the method that is called back when the send operation ends.
           
            // Clean up.
            message.Dispose();
           
        }
    }
}