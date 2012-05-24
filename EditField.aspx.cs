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

public partial class EditField : PageBase
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
      string aFieldID = Request.QueryString["ID"];
      LoadRecord(int.Parse(aFieldID));
    }
  }



  private void LoadRecord(int aFieldID)
  {
    // load our database.
    trn_Field aField = BM().GetField(aFieldID);

    lID.Text = aField.FieldID.ToString();
    eDescription.Text = aField.Description;

  }



  private void SaveRecord()
  {
    trn_Field aField;

    aField = BM().GetField(int.Parse(lID.Text));
    aField.Description = eDescription.Text.Trim();

    BM().PutField(aField);
  }


  protected void btnSave_Click(object sender, EventArgs e)
  {
    SaveRecord();
    Response.Redirect(Request.UrlReferrer.ToString()); // go home.
  }


}
