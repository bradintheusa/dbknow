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

public partial class Tables : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        string aDatabaseID = Request.QueryString["ID"];
        UpdateDisplay(int.Parse(aDatabaseID));
      }
    }

    private void UpdateDisplay(int aDatabseID)
    {
      trn_Database aDatabase = BM().Get(aDatabseID);
      gvTables.DataSource = aDatabase.trn_Tables.Where(t => t.Active == baseBusinessObject.IND_True).OrderBy(t => t.Name);
      gvTables.DataBind();
      MakeAccessible(gvTables);
    }
}
