using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Collections;
using BusinessObjects;
using CDL = CoreDataLayer;

namespace BusinessLayer
{
    public class SentimentData
    {

        public DataSet getProperties()
        {
            DataSet dsMetadata = null;
           CDL.CoreDataLayer objCoreDataLayer = new CDL.CoreDataLayer();
            try
            {
                dsMetadata = new DataSet();
                dsMetadata = objCoreDataLayer.ExecuteDataSet("pr_getproperties", new object[] { });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsMetadata;

        }

        public DataSet getTopSentiments()
        {
            DataSet dsSentiments = null;
            CDL.CoreDataLayer objCoreDataLayer = new CDL.CoreDataLayer();
            try
            {
                dsSentiments = new DataSet();
                dsSentiments = objCoreDataLayer.ExecuteDataSet("pr_gettopsentimentstatus", new object[] { });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsSentiments;

        }

        public DataSet getPropertyById(int propertyId)
        {
            DataSet dsMetadata = null;
            CDL.CoreDataLayer objCoreDataLayer = new CDL.CoreDataLayer();
            try
            {
                dsMetadata = new DataSet();
                dsMetadata = objCoreDataLayer.ExecuteDataSet("pr_getpropertyById", new object[] {propertyId });

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsMetadata;

        }



        public bool insertProperties(Property objPropery)
        {
            CDL.CoreDataLayer objCoreDataLayer = new CDL.CoreDataLayer();

            bool blnFlg = false;
            try
            {
                
                string retValue = objCoreDataLayer.ExecuteScalar("pr_insertproperties", new object[] {objPropery.PropertyId,objPropery.PropertyName,objPropery.PropertValues,objPropery.PropertyDesc },true);
                if (retValue=="0")
                    blnFlg = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return blnFlg;
        }

        public bool insertSentiments(SentimentObj objSentiments )
        {
            CDL.CoreDataLayer objCoreDataLayer = new CDL.CoreDataLayer();

            bool blnFlg = false;
            try
            {

                string retValue = objCoreDataLayer.ExecuteScalar("pr_insertsentiments", new object[] { objSentiments.SentimentDesc,objSentiments.SentimentStatus,objSentiments.PosScore,objSentiments.NegScore,objSentiments.SentimentScore }, true);
                if (retValue == "0")
                    blnFlg = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return blnFlg;
        }




    }
}