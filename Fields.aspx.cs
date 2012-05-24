using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Fields : PageBase
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      string aTableID = Request.QueryString["ID"];
      UpdateDisplay(int.Parse(aTableID));
    }
  }

  private void UpdateDisplay(int aTableID)
  {
    trn_Table aTable = BM().GetTable(aTableID);
    gvFields.DataSource = aTable.trn_Fields.Where(t => t.Active == baseBusinessObject.IND_True).OrderBy(t => t.Name);
    gvFields.DataBind();
    MakeAccessible(gvFields);
  }
}
