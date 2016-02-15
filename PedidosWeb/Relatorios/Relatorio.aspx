<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Relatorio.aspx.cs" Inherits="PedidosWeb.Relatorios.Relatorio" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            
            <rsweb:reportviewer id="ReportViewer1" runat="server" width="722px"></rsweb:reportviewer>
        </div>
    </form>
</body>
</html>
