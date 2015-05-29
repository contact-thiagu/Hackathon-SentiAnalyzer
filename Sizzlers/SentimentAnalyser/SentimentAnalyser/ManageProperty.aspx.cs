using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObjects;
using System.Data;
using System.Text;

namespace SentimentAnalyser
{
    public partial class ManageProperty : System.Web.UI.Page
    {
        SentimentData objBL = new SentimentData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                LoadData();

            }
        }

        protected void LoadData()
        {
            DataSet dsProperties = new DataSet();

            dsProperties = objBL.getProperties();
            StringBuilder strBuild = new StringBuilder();
            if (dsProperties != null && dsProperties.Tables[0].Rows.Count > 0)
            {

                strBuild.Append("<div class='panel panel-default'><div class='panel-heading'><p>Manage Properties</p></div>");
                strBuild.Append("<table class='table'><thead><tr>");
                strBuild.Append("<th>#</th><th></th><th>Name</th><th>Values</th><th>Description</th>");
                strBuild.Append("</tr></thead><tbody>");
                int rowCount = 1;
                foreach (DataRow dr in dsProperties.Tables[0].Rows)
                {
                    string propertyId = dr["property_id"].ToString();
                    strBuild.Append("<tr><th scope='row'>" + rowCount + "</th><td><input type='radio' id='rad_" + propertyId + "' onclick=\"javascript:fnSetProperty('" + propertyId + "')\"></td><td>" + dr["property_name"].ToString() + "</td><td><input type='text' class='form-control' id='txt"+propertyId+"' disabled='disabled' value='"+ dr["property_value"].ToString() + "'  /> </td><td>" + dr["property_description"].ToString() + "</td></tr>");
                    rowCount++;
                }
                strBuild.Append("</tbody></table></div></div>");
            }

            litProperties.Text = strBuild.ToString();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Property objProperty = new Property();
            string msg = "Submitted Successfully";
            objProperty.PropertyName = txtName.Text;
            objProperty.PropertValues = txtValues.Text;
            objProperty.PropertyDesc = txtDesc.Text;

            if(hdnAction.Value!="")
            {
                msg = "Updated Successfully!";
                objProperty.PropertyId = Convert.ToInt32(hdnPropertyId.Value);
            }

            bool blnFlg = objBL.insertProperties(objProperty);

            if(blnFlg)
            {
                divMsg.Visible = true;
                divMsg.Attributes.Add("class", "alert alert-success");
                divMsg.InnerText = msg;
                clearFields();
                LoadData();
            }
            else
            {
                divMsg.Visible = true;
                divMsg.Attributes.Add("class", "alert alert-danger");
                divMsg.InnerText = "Sorry Error occured!";
            }
        }

        protected void clearFields()
        {
            txtName.Text = "";
            txtValues.Text = "";
            txtDesc.Text = "";
            hdnPropertyId.Value = "";
            hdnAction.Value = "";
            txtName.Enabled = true;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string propertyId = hdnPropertyId.Value;
            hdnAction.Value = "Update";
            int pId = 0;
            bool flg = int.TryParse(propertyId, out pId);
            if (flg)
            {
                DataSet dsProperty = new DataSet();
                dsProperty = objBL.getPropertyById(pId);
                if (dsProperty != null && dsProperty.Tables[0].Rows.Count > 0)
                {
                    txtName.Enabled = false;
                    txtName.Text = dsProperty.Tables[0].Rows[0]["property_name"].ToString();
                    txtValues.Text = dsProperty.Tables[0].Rows[0]["property_value"].ToString();
                    txtDesc.Text = dsProperty.Tables[0].Rows[0]["property_description"].ToString();
                }
            }


           
           }
                    
    }
}