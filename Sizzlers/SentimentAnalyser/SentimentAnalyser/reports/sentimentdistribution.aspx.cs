using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;


    public partial class reports_sentimentdistribution : System.Web.UI.Page
    {
    protected string sentiments = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet dsSentiments = new DataSet();

                SentimentData objBL = new SentimentData();
                StringBuilder strBuild = new StringBuilder();
                dsSentiments = objBL.getTopSentiments();
                if (dsSentiments != null && dsSentiments.Tables[0].Rows.Count > 0)
                {
                    strBuild.Append("<div class='panel panel-default' style='width:500px'><div class='panel-heading'><p>Top Postive Sentiments</p></div>");
                    strBuild.Append("<table class='table'><thead><tr>");
                    strBuild.Append("<th>Sentiments</th>");
                    strBuild.Append("</tr></thead><tbody>");

                    foreach (DataRow dr in dsSentiments.Tables[0].Rows)
                    {

                        strBuild.Append("<tr><td>" + dr["sentiment_description"].ToString() + "</td></tr>");

                    }
                    strBuild.Append("</tbody></table></div></div>");

                    if (dsSentiments.Tables[1].Rows.Count > 0)
                    {
                        strBuild.Append("<div class='panel panel-default' style='width:500px'><div class='panel-heading'><p>Top Negative Sentiments</p></div>");
                        strBuild.Append("<table class='table' ><thead><tr>");
                        strBuild.Append("<th>Sentiments</th>");
                        strBuild.Append("</tr></thead><tbody>");

                        foreach (DataRow dr in dsSentiments.Tables[1].Rows)
                        {

                            strBuild.Append("<tr><td>" + dr["sentiment_description"].ToString() + "</td></tr>");

                        }
                        strBuild.Append("</tbody></table></div></div>");
                    }
                }


            //litSentiments.Text = strBuild.ToString();
            sentiments = strBuild.ToString();

            }
        }
    }
