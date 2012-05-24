<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>



    <form id="form1" runat="server">
    <div>
      Database List<br />
        <asp:GridView ID="gvDatabases" runat="server" AutoGenerateColumns="False" 
        CssClass="ListTable" BorderStyle="None">
          <Columns>
            <asp:HyperLinkField datanavigateurlfields="DatabaseID" HeaderText="Database" datanavigateurlformatstring="Tables.aspx?ID={0}" DataTextField="Name" />
            <asp:TemplateField  HeaderText="Description">
              <ItemTemplate>
                <%# Eval("DescriptionHTML")%>
              </ItemTemplate>
            </asp:TemplateField>            
            <asp:HyperLinkField datanavigateurlfields="DatabaseID" HeaderText="Refresh" datanavigateurlformatstring="EditDatabase.aspx?Refresh={0}" Text="Refresh" />
            <asp:HyperLinkField datanavigateurlfields="DatabaseID" HeaderText="Edit" datanavigateurlformatstring="EditDatabase.aspx?ID={0}" Text="Edit" />
          </Columns>
        </asp:GridView>
      <br />
      <a href="EditDatabase.aspx">New Database</a>
    
    </div>
    </form>
<p>
  &nbsp;</p>
<p>
  &nbsp;</p>

