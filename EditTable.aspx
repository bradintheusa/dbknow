<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditTable.aspx.cs" Inherits="EditTable" %>


    <form id="form1" runat="server">
    <div>
    
      Description<br />
      <asp:TextBox ID="eDescription" runat="server" TextMode="MultiLine" 
        CssClass="commentbox" Height="300px" Width="600px" ></asp:TextBox>
      <br />
      <br />
      <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
        onclick="btnSave_Click" />
      <asp:Label ID="lID" runat="server" Text="lID" Visible="False"></asp:Label>
    
    </div>
    </form>
