<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditDatabase.aspx.cs" Inherits="EditDatabase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Strict//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
      .style1
      {
        margin-bottom: 61px;
      }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      &nbsp;
      <br />
      <br />
      Database Name:<br />
&nbsp;<asp:TextBox ID="eName" runat="server" ontextchanged="eName_TextChanged"></asp:TextBox>
      <br />
      <p class="Help">This is the name you call the database, you can chnage the name later if you 
      like. It will be shown as the primary referene for the database</p>
      
      <br />
      Description:<br />
&nbsp;<asp:TextBox ID="eDescription" runat="server" Width="600px" 
        TextMode="MultiLine" CssClass="commentbox style1" Height="300px"></asp:TextBox>
      <p class="Help">Use all the space you want to describe your databse here.</p>
      <br />
      Database:<br />
&nbsp;<asp:DropDownList ID="cbDatabaseType" runat="server"  Width="184px">
        <asp:ListItem Selected="True" Value="1">SQL Server</asp:ListItem>
        <asp:ListItem Value="2">MySQL</asp:ListItem>
        <asp:ListItem Value="3">Oracle</asp:ListItem>
      </asp:DropDownList>
      <p class="Help">
      Choose the server type for your database. Send us an email if you want another 
      database suppored (we need to write a little code)</p>
      Connection String:<br />
&nbsp;<asp:TextBox ID="eConnectionString" runat="server" Width="405px"></asp:TextBox>
      <p class="Help">
      If you know how to connect to your database using ADO.Net enter the connection 
      string here. If you you can <a href="http://www.connectionstrings.com/">look 
      online</a> for examples.</p>
      <br />
      Active:
      <asp:CheckBox ID="cbActive" runat="server" /><br />
      <br />
      <br />
      <asp:Label ID="lCreated" runat="server" Text="lCreated"></asp:Label><br />
      <asp:Label ID="lModified" runat="server" Text="lModified"></asp:Label>
      <br />
      <asp:Label ID="lScanned" runat="server" Text="lScanned"></asp:Label>
      <br />
      <br />
      <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" 
        Width="100px" />
      <asp:Label ID="lID" runat="server" Text="lID" Visible="False"></asp:Label></div>
    </form>
</body>
</html>
