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

public partial class EditTable : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
      if (!Page.IsPostBack)
      {
        string aTableID = Request.QueryString["ID"];
        LoadRecord(int.Parse(aTableID));
      }
    }



    private void LoadRecord(int aTableID)
    {
      // load our database.
      trn_Table aTable = BM().GetTable(aTableID);

      lID.Text = aTable.TableID.ToString();
      eDescription.Text = aTable.Description;

    }



    private void SaveRecord()
    {
      trn_Table aTable;

      aTable = BM().GetTable(int.Parse(lID.Text));
      aTable.Description = eDescription.Text.Trim();

      BM().PutTable(aTable);
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
      SaveRecord();
      Response.Redirect(Request.UrlReferrer.ToString()); // go home.
    }


}
