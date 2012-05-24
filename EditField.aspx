<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditField.aspx.cs" Inherits="EditField" %>


    <form id="form1" runat="server">
    <div>
    
      Description<br />
      <asp:TextBox ID="eDescription" runat="server" Width="600px" Rows="10" 
        TextMode="MultiLine" Height="300px" CssClass="commentbox" ></asp:TextBox>
      <br />
      <br />
      <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
        onclick="btnSave_Click" />
      <asp:Label ID="lID" runat="server" Text="lID" Visible="False"></asp:Label>
    
    </div>
    </form>
