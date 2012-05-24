<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fields.aspx.cs" Inherits="Fields" %>

 <form id="form1" runat="server">
    <div>
    
        <asp:GridView ID="gvFields" runat="server" AutoGenerateColumns="False" CssClass="ListTable">
          <Columns>
            <asp:BoundField DataField="Name" HeaderText="Field" />
            <asp:BoundField DataField="Nullable" HeaderText="Nulls" />
            <asp:BoundField DataField="FieldType" HeaderText="Type" />
            <asp:BoundField DataField="MaxLength" HeaderText="Size" />
            <asp:TemplateField  HeaderText="Description">
              <ItemTemplate>
                <%# Eval("DescriptionHTML")%>
              </ItemTemplate>
            </asp:TemplateField>            
            <asp:HyperLinkField datanavigateurlfields="FieldID" HeaderText="Edit" datanavigateurlformatstring="EditField.aspx?ID={0}" Text="Edit" />
          </Columns>
        </asp:GridView>
    
    </div>
    </form>
