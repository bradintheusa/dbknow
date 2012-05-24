using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : PageBase
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      UpdateDisplay();
    }
  }
  
  private void UpdateDisplay()
  {
    gvDatabases.DataSource = BM().GetDatabaseList().Values;
    gvDatabases.DataBind();
    MakeAccessible(gvDatabases);
  }

}
