<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ManageProperty.aspx.cs" Inherits="SentimentAnalyser.ManageProperty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
          <script>
    function fnSetProperty(pId) {
        document.getElementById("ContentPlaceHolder1_hdnPropertyId").value = pId;

    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div class="bodycontent">
       <div class="fixer" style="height:20px">&nbsp;</div> 
         <p class="apex_book_italic f20 snug">Manage Properties</p>
        	<!-- Body Content Start-->
            <div class="">
                 <div class="form-group">
 <div class="fixer" style="height:10px">&nbsp;</div>                            
              <asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="btn btn-primary" OnClick="btnUpdate_Click" CausesValidation="false" />
                        </div>
                <asp:Literal ID="litProperties" runat="server"></asp:Literal>
            	<p class="section_title">Enter the following details:</p>
                <div class="formwrap">
                        
                        <div class="form-group col-md-6 col-lg-6">
                            <label for="name">Name:<span class="tred"> *</span></label>
                            
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtName" ErrorMessage="*" ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6 col-lg-6">
                            <label for="desc">Values:<span class="tred"> *</span></label>
                            <asp:TextBox ID="txtValues" runat="server" TextMode="MultiLine" Rows="5" Columns="25" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValues" ErrorMessage="*" ForeColor="#CC0000">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group col-md-6 col-lg-6">
                            <label for="desc">Desc:<span class="tred"></span></label>
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control" MaxLength="500"></asp:TextBox>
                        </div>
                        
                        <div class="fixer">&nbsp;</div>
                        <div class="form-group">
                            
                            <asp:Button ID="btnSubmit" Text="Save" runat="server" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                        </div>
                        <div class="fixer">&nbsp;</div>
                        <div id="divMsg" role="alert" runat="server" visible="false"></div>
                </div>
            </div>
            
            <!-- Body Content Start-->
            
        <input type="hidden"  id="hdnPropertyId" name="hdnPropertyId" runat="server" />
         <input type="hidden"  id="hdnAction" name="hdnAction" runat="server" />
        </div>
</asp:Content>
