<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tables.aspx.cs" Inherits="Tables" %>

    <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="gvTables" runat="server" AutoGenerateColumns="False" CssClass="ListTable">
          <Columns>
            <asp:HyperLinkField datanavigateurlfields="TableID" HeaderText="Table" datanavigateurlformatstring="Fields.aspx?ID={0}" DataTextField="Name" />
            <asp:TemplateField  HeaderText="Description">
              <ItemTemplate>
                <%# Eval("DescriptionHTML")%>
              </ItemTemplate>
            </asp:TemplateField>            
            <asp:HyperLinkField datanavigateurlfields="TableID" HeaderText="Edit" datanavigateurlformatstring="EditTable.aspx?ID={0}" Text="Edit" />
          </Columns>
        </asp:GridView>
    
    </div>
    </form>
