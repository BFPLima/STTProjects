<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormReportMajorWorkedHours.aspx.cs" Inherits="STTProjects.TimeSheet.WebApp.Reports.WebFormReportMajorWorkedHours" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <link href="../Content/bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.10.2.js"></script>
    <script src="../Scripts/jquery.validate.min.js"></script>
    <script src="../Scripts/bootstrap.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1 style="text-align: center"> <span class="label label-pill label-info" id="spanLabel" runat="server"> </span></h1>
            
            <asp:GridView ID="GridView1" runat="server" Style="width: 90%; margin-right: 5%; margin-left: 5%; text-align: center" CssClass="table table-hover table-striped" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="FullName" HeaderText="Nome" HeaderStyle-CssClass="text-center" HeaderStyle-Font-Size="Large" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
