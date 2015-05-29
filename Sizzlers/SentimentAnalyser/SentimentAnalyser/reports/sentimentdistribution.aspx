<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Site1.Master" CodeFile="sentimentdistribution.aspx.cs" Inherits="reports_sentimentdistribution" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="../js/Chart.js"></script>
        <script>
        var rstData = null;
        var pieData = [
				{
				    value: 1,
				    color: "#F7464A",
				    highlight: "#FF5A5E",
				    label: "Negative"
				},
				{
				    value: 1,
				    color: "#46BFBD",
				    highlight: "#5AD3D1",
				    label: "Positive"
				},
				{
				    value: 1,
				    color: "#FDB45C",
				    highlight: "#FFC870",
				    label: "Neutral"
				}

        ];

        window.onload = function () {
            
            loadData();
            
        };


        function loadData() {
            var xmlhttp;
            if (window.XMLHttpRequest) {// code for IE7+, Firefox, Chrome, Opera, Safari
                xmlhttp = new XMLHttpRequest();
            }
            else {// code for IE6, IE5
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {

                    rstData = eval(xmlhttp.responseText);
                    var ctx = document.getElementById("chart-area").getContext("2d");
                    for (i = 0; i < rstData.length; i++) {
                        oDbData = rstData[i];
                        for (j = 0; j < pieData.length; j++) {
                            if (oDbData.SentimentStatus == pieData[j].label) {
                                
                                    pieData[j].value = oDbData.SentimentCount;
                                
                            }                            
                        }
                    }
                    window.myPie = new Chart(ctx).Pie(pieData);
                }
            }
            xmlhttp.open("GET", "./service/rstsentimentdistribution.aspx", true);
            xmlhttp.send();
        }

	</script>
    <style>.foo {   
    float: left;
    width: 20px;
    height: 20px;
    margin: 5px;
    border-width: 1px;
    border-style: solid;
    border-color: rgba(0,0,0,.2);
}
        .auto-style1 {
            width: 49px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="bodycontent">
         <div class="fixer" style="height:20px">&nbsp;</div> 
         <p class="apex_book_italic f20 snug">Customer Insight Dashboard</p>
         <div class="fixer" style="height:10px">&nbsp;</div> 
   
                            
                         <table class='table'><thead><tr>
            <th class="auto-style1"></th><th></th>
	</tr></thead><tbody>
          <tr><td class="auto-style1"><div id="divSent"><%=sentiments %></div></td> <td>  <div id="canvas-holder">
			<canvas id="chart-area" width="300" height="300"/>
		</div>           <table class='table' style="width:200px"><thead><tr>
            <th class="auto-style1"></th><th></th>
	</tr></thead><tbody>
          <tr><td class="auto-style1"><div class="foo" style="background-color:#46BFBD;"></div></td> <td>Positive</td></tr>
         <tr><td class="auto-style1"><div class="foo" style="background-color:#F7464A;"></div></td><td>Negative</td> </tr>
        <tr><td class="auto-style1"><div class="foo" style="background-color:#FDB45C;"></div></td> <td>Neutral</td></tr>
        </tbody></table></td>

          </tr>
       
        </tbody></table>
  
                          
                            
              
                      
        </div>
   

      

</asp:Content>