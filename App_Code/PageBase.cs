using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for PageBase
/// </summary>
public class PageBase : System.Web.UI.Page
{
  private const string FLASHNOTICE = "FLASHNOTICE";
  private const string FLASHWARNING = "FLASHWARNING";

  protected string PageTitle = String.Empty;
  protected string FlashNotice {
    get
    {
      if (Session[FLASHNOTICE] == null) { return string.Empty; }
                                      else   { return (string) Session[FLASHNOTICE];}
    }
    set { Session[FLASHNOTICE] = value; }
  }

  protected string FlashWarning {
    get
    {
      if (Session[FLASHWARNING] == null) { return string.Empty; }
                                      else   { return (string) Session[FLASHWARNING];}
    }
    set { Session[FLASHNOTICE] = value; }
  }


	public PageBase()
	{
		//
		// TODO: Add constructor logic here
		//
	}

  protected BusinessManager BM()
  {
    BusinessManager aBM = (BusinessManager)Session["BusinessManager"];
      if (aBM == null)
      {
       aBM = new BusinessManager();
      }
      Session["BusinessManager"] = aBM;
      return aBM;
  }

  protected override void Render(HtmlTextWriter writer)
  {


    writer.Write("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.0 Strict//EN\">\n");
    writer.Write("<html>");
    writer.Write("<head>");
    writer.Write("  <title>dbKnow: " + PageTitle + "</title>\n");
    writer.Write("  <link href='css/default.css' media='screen' rel='Stylesheet' type='text/css' />	\n");
//    writer.Write("  <script src='javascripts/prototype.js' type='text/javascript'></script> \n");
    //writer.Write("  <script src='javascripts/scriptaculous.js' type='text/javascript'></script> \n");
    writer.Write("  <script src='javascripts/jquery-1.2.3.js' type='text/javascript'></script> \n");
    writer.Write("  <script src='javascripts/jquery.autogrow.js' type='text/javascript'></script> \n");
    writer.Write("  <script src='javascripts/dbknow.js' type='text/javascript'></script> \n");
    writer.Write("</head>\n");
    writer.Write("<body>");
    writer.Write("<div id='container' >");

    writer.Write("<div id='header'>");
    writer.Write("<h1>dbKnow:"+ PageTitle +"</h1>");
    writer.Write("<h2>....it's like a wiki for your database.</h2>");
    writer.Write("</div>");

    writer.Write("<div id='navigation'>");
    writer.Write("<ul>");
    writer.Write("<li><a href=default.aspx>Home</a></li>");
    writer.Write("<li></li>");
    writer.Write("<li></li>");
    writer.Write("<li><a href=http://www.dbknow.com/help/>Help</a></li>");
    writer.Write("</ul>");
    writer.Write("</div>\n");


    writer.Write("<div id='content'>");

    if (FlashNotice != String.Empty)
    {
      writer.Write("<div class='NoticePane'>");
	    writer.Write("  <h3>Notice:</h3>");
      writer.Write("	<p>"+FlashNotice+"</p>");
      writer.Write("	<img src='images/btn-delete.gif' alt='delete' class='delete' />");
      writer.Write("</div>\n");
      FlashNotice = String.Empty;
    }
    if (FlashWarning != String.Empty)
    {
      writer.Write("<div class='WarningPane'>");
	    writer.Write("  <h3>Notice:</h3>");
      writer.Write("	<p>"+FlashWarning+"</p>");
      writer.Write("	<img src='images/btn-delete.gif' alt='delete' class='delete' />");
      writer.Write("</div>\n");
      FlashWarning = String.Empty;
    }

    base.Render(writer);

    writer.Write("<br/>\n");
    writer.Write("<br/>\n");
    writer.Write("</div>");

    writer.Write("<div id='footer'>");
    writer.Write("<p>&copy; 2005-2011 <a href='http://www.dbknow.com'>dbKnow.com</a></p>");
    writer.Write("</div>");

    writer.Write("</div>");
    writer.Write("</body>");
    writer.Write("</html>");
  }

  // from http://www.webpronews.com/expertarticles/2007/01/25/aspnet-make-gridview-control-accessible
  protected void MakeAccessible(GridView grid)
  {
    if (grid.Rows.Count > 0)
    {
      //This replaces <td> with <th> and adds the scope attribute
      grid.UseAccessibleHeader = true;

      //This will add the <thead> and <tbody> elements
      grid.HeaderRow.TableSection = TableRowSection.TableHeader;

      //This adds the <tfoot> element. Remove if you don't have a footer row
      grid.FooterRow.TableSection = TableRowSection.TableFooter;
    }
  }

}
