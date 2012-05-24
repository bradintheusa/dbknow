using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/*
 Sample connection Strings
 
 * SQL Server
 * Data Source=BRADHOME\SQLEXPRESS;Initial Catalog=AdventureWorksDW;Integrated Security=True
 * Data Source=BRADHOME\SQLEXPRESS;Initial Catalog=NorthWind2;Integrated Security=True
 * 
 * My SQL
 * database=birt;server=localhost;user id=brad;pwd=password
 * 
 * 
 * Oracle
 * Data Source=127.0.0.1/XE;Uid=hr;Pwd=hr;                   
 * 
 * 
 
*/
public partial class EditDatabase : PageBase
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (!Page.IsPostBack)
    {
        string aDatabaseID = Request.QueryString["Refresh"];
        if (aDatabaseID != null)
        {
          RefreshRecord(int.Parse(aDatabaseID));
        }
        else
        {
          aDatabaseID = Request.QueryString["ID"];
          if (aDatabaseID != null)
          {
              LoadRecord(int.Parse(aDatabaseID));
          }
          else
          {
              NewRecord();
          }
        }
    }
  }
  private void NewRecord()
  {
    // clear all the fields
    lID.Text = string.Empty;
    eName.Text = string.Empty;
    eDescription.Text = string.Empty;
    eConnectionString.Text = string.Empty;
    cbActive.Checked = true;
    lCreated.Text = string.Empty;
    lModified.Text = string.Empty;
    lScanned.Text = string.Empty;
  }


  private void LoadRecord(int aDatabaseID)
  {
    // load our database.
      trn_Database aDatabase = BM().Get(aDatabaseID);

    lID.Text = aDatabase.DatabaseID.ToString();
    eName.Text = aDatabase.Name;
    eDescription.Text = aDatabase.Description;
    cbDatabaseType.SelectedValue = aDatabase.DatabaseTypeInd.ToString();
    eConnectionString.Text = aDatabase.ConnectionString.Trim();
    cbActive.Checked = aDatabase.IsActive;
    lCreated.Text = aDatabase.Created.ToString();
    lModified.Text = aDatabase.LastModified.ToString();
    lScanned.Text = aDatabase.LastScanned.ToString();
    
  }


  private void RefreshRecord(int aDatabaseID)
  {
    // load our database.
    //Database aDatabase = allDatabases().Get(aDatabaseID);

    //aDatabase.Refresh();

    BM().Refresh(aDatabaseID);
    Response.Redirect("default.aspx"); // go home.
  }

  private void SaveRecord()
  {
    trn_Database aDatabase;

    if (lID.Text == string.Empty)
    {
        aDatabase = new trn_Database();
    }
    else
    {
        aDatabase = BM().Get(int.Parse(lID.Text));
    }

    aDatabase.Name = eName.Text.Trim();
    aDatabase.Description = eDescription.Text.Trim();
    aDatabase.ConnectionString = eConnectionString.Text.Trim();
    aDatabase.IsActive = cbActive.Checked;
    aDatabase.DatabaseTypeInd = int.Parse(cbDatabaseType.SelectedValue);
    BM().Put(aDatabase);
  }


  protected void btnSave_Click(object sender, EventArgs e)
  {
    SaveRecord();
    FlashNotice = "Database " + eName.Text.Trim()  + " saved.";
    Response.Redirect("default.aspx"); // go home.
  }
  protected void eName_TextChanged(object sender, EventArgs e)
  {

  }
}
