using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Collections;
using BusinessObjects;
using CDL = CoreDataLayer;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace BusinessLayer
{
   
    public class ReportServiceBL
    {

        public List<BusinessObjects.ResportServiceObject> GetSentimentReport(DateTime dtStart, DateTime dtEnd)
        {
            

            DataSet dsReport = null;
            CDL.CoreDataLayer objCoreDataLayer = new CDL.CoreDataLayer();
            List<BusinessObjects.ResportServiceObject> arrRpt = new List<ResportServiceObject> { };
            try
            {                
                dsReport = new DataSet();
                //string GetSentimentReport(DateTime dtStart,DateTime dtEnd);
                dsReport = objCoreDataLayer.ExecuteDataSet("pr_GetSentimentsByDate", new object[] { dtStart, dtEnd });
                for(int i=0;i<dsReport.Tables[0].Rows.Count;i++)
                {
                    ResportServiceObject oRpt = new ResportServiceObject();
                    oRpt.SentimentStatus = dsReport.Tables[0].Rows[i]["sentiments_status"].ToString();
                    oRpt.SentimentCount =int.Parse(dsReport.Tables[0].Rows[i]["sentiment_count"].ToString());
                    arrRpt.Add(oRpt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return arrRpt;

        }
    }
}