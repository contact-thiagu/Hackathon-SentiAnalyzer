using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessObjects;
using System.IO;

using System.Runtime.Serialization.Json;

public partial class reports_service_rstsentimentdistribution : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        BusinessLayer.ReportServiceBL oBl = new BusinessLayer.ReportServiceBL();
        List <BusinessObjects.ResportServiceObject> olst=oBl.GetSentimentReport(DateTime.Now.Subtract(TimeSpan.FromHours(24)), DateTime.Now);


        

        MemoryStream stream1 = new MemoryStream();
        DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<BusinessObjects.ResportServiceObject>));
        ser.WriteObject(stream1, olst);
        stream1.Position = 0;
        StreamReader sr = new StreamReader(stream1);
        
        Response.Write(sr.ReadToEnd());
        sr.Close();

    }
}