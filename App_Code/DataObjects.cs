using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Linq;
using System.Data.OracleClient;
using MySql.Data;
using MySql.Data.MySqlClient;



public class baseBusinessObject
{
  public const int IND_False = 0;
  public const int IND_True = 1;

  protected string TextileIt(string aString)
  {
    if (aString == null) { aString = string.Empty; }
    string rs = Textile.TextileFormatter.FormatString(aString);
    return rs;
  }
}

public partial class trn_Database : baseBusinessObject
{
  partial void OnCreated()
  {
    LastScanned = DateTime.Now;      
    LastModified = DateTime.Now;      
    Created = DateTime.Now;
    LockNum = 0; 
  }

    public bool IsActive
    {
        get
        {
            return this.Active == trn_Database.IND_True;
        }
        set
        {
            if (value) { Active = trn_Database.IND_True; }
            else { Active = trn_Database.IND_False; }
        }
    }
    public string DescriptionHTML
    {
      get
      {
        return TextileIt(Description);
      }
    }

}

public partial class trn_Table : baseBusinessObject
{
  partial void OnCreated()
  {
    LastScanned = DateTime.Now;
    LastModified = DateTime.Now;
    Created = DateTime.Now;
    LockNum = 0;
  }
  public string DescriptionHTML
  {
    get
    {
      return TextileIt(Description);
    }
  }

}

public partial class trn_Field : baseBusinessObject
{
  partial void OnCreated()
  {
    LastScanned = DateTime.Now;
    LastModified = DateTime.Now;
    Created = DateTime.Now;
    LockNum = 0;
  }
  public string DescriptionHTML
  {
    get
    {
      return TextileIt(Description);
    }
  }

}
  


public class BusinessManager
{
  private static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
  private SqlConnection sqlConnection = new SqlConnection(ConnectionString);
  private SqlCommand sqlCommand = new SqlCommand();
  private DBMap db = new DBMap();

  public BusinessManager()
  {
    sqlCommand.Connection = sqlConnection;
  }

#region utils
  public SqlDataReader ExecSelect(string aSQL)
  {
    sqlCommand.Connection.Open();
    sqlCommand.CommandText = aSQL;
    SqlDataReader r = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
    return r;
  }

  public void ExecExecute(string aSQL)
  {
    sqlCommand.Connection.Open();
    sqlCommand.CommandText = aSQL;
    sqlCommand.ExecuteNonQuery();
  }

  protected string CommaQuote(string aValue)
  {
    return "," + Quote(aValue);
  }
  protected string Quote(string aValue)
  {
    return "'" + aValue + "'";
  }

  protected string CommaQuote(DateTime aDateTime)
  {
    // new to add hours mins and seconds here.
    return "," + Quote(aDateTime);
  }

  protected string Quote(DateTime aDateTime)
  {
    // new to add hours mins and seconds here.
    return "'" + aDateTime.ToString("yyyy-MM-dd") + "'";
  }

  protected string CommaQuote(bool aValue)
  {
    return "," + Quote(aValue);
  }

  protected string Quote(bool aValue)
  {
    if (aValue) { return "1"; }
    else { return "0"; }
  }
#endregion


  public Dictionary<int, trn_Database> GetDatabaseList()
  {
      Dictionary<int, trn_Database> lDatabases = new Dictionary<int, trn_Database>();

    var allDBs = (from d in db.trn_Databases
                  where d.Active == trn_Database.IND_True
                  orderby d.Name
                  select d);


    foreach (trn_Database d in allDBs)
    {
        lDatabases.Add(d.DatabaseID, d);
    }

    return lDatabases;
  }

  public trn_Database Get(int aDatabaseID)
  {
    trn_Database aDatabase = (from d in db.trn_Databases
                              where d.DatabaseID == aDatabaseID
                              select d).SingleOrDefault();
    return aDatabase;
  }

  public void Put(trn_Database aDatabase)
  {
    if (aDatabase.DatabaseID == 0)
    {
        db.trn_Databases.InsertOnSubmit(aDatabase);
    }


    db.SubmitChanges();
  }

  public trn_Table GetTable(int aTableID)
  {
    trn_Table aTable = (from t in db.trn_Tables
                        where t.TableID == aTableID
                        select t).SingleOrDefault();
    return aTable;
  }

  public void PutTable(trn_Table aTable)
  {
    db.SubmitChanges();
  }

  public trn_Field GetField(int aFieldID)
  {
    trn_Field aField = (from t in db.trn_Fields
                        where t.FieldID == aFieldID
                        select t).SingleOrDefault();
    return aField;
  }

  public void PutField(trn_Field aField)
  {
    db.SubmitChanges();
  }

  public void Refresh(int aDatabaseID)
  {
    trn_Database aDatabase = (from d in db.trn_Databases
                              where d.DatabaseID == aDatabaseID
                              select d).SingleOrDefault();

    foreach (trn_Table t2 in aDatabase.trn_Tables)
    {
      t2.Active = trn_Table.IND_False;
      foreach (trn_Field f2 in t2.trn_Fields)
      {
        f2.Active = trn_Field.IND_False;
      }
    }

    IDataReader r = null;
     
    if (aDatabase.DatabaseTypeInd == 1) // sql server
    {
      SqlConnection con = new SqlConnection(aDatabase.ConnectionString);
      con.Open();
      SqlCommand cmd = new SqlCommand("SELECT T.TABLE_SCHEMA + '.' + T.TABLE_NAME AS table_name, C.COLUMN_NAME AS column_name,	" +
                                      " C.IS_NULLABLE as nullable, C.DATA_TYPE AS field_type, CHARACTER_MAXIMUM_LENGTH as max_length " +
                                      " FROM INFORMATION_SCHEMA.TABLES T JOIN INFORMATION_SCHEMA.COLUMNS C	" +
                                      " ON T.TABLE_NAME = C.TABLE_NAME	WHERE T.TABLE_NAME NOT LIKE 'sys%'" +
                                      " AND T.TABLE_NAME <> 'dtproperties'	AND T.TABLE_SCHEMA <> 'INFORMATION_SCHEMA' " +
                                      " and T.Table_Type = 'BASE TABLE'	ORDER BY T.TABLE_NAME, C.ORDINAL_POSITION", con);
      r = cmd.ExecuteReader();
    }
    else if (aDatabase.DatabaseTypeInd == 2) // Mysql server
    {
      MySqlConnection con = new MySqlConnection(aDatabase.ConnectionString);
      con.Open();
      MySqlCommand cmd = new MySqlCommand("SELECT t.table_name, c.COLUMN_NAME as column_name, c.DATA_TYPE as field_type, c.IS_NULLABLE as nullable, c.CHARACTER_MAXIMUM_LENGTH as max_length FROM INFORMATION_SCHEMA.COLUMNS c, INFORMATION_SCHEMA.TABLES t WHERE c.table_name = t.table_name  AND t.table_schema = '" + con.Database + "'", con);
      r = cmd.ExecuteReader();
    }
    else if (aDatabase.DatabaseTypeInd == 3) // The Oracle
    {
      OracleConnection con = new OracleConnection(aDatabase.ConnectionString);
      con.Open();
      OracleCommand cmd = new OracleCommand("select table_name as table_name, COLUMN_NAME AS column_name, NULLABLE as nullable, DATA_TYPE AS field_type, DATA_LENGTH as max_length from  USER_TAB_COLUMNS", con);
      r = cmd.ExecuteReader();
    }

    while (r.Read())
    {
      trn_Table t = (from dbt in aDatabase.trn_Tables
                     where dbt.Name.ToUpper().Trim() == r["TABLE_NAME"].ToString().ToUpper().Trim()
                     select dbt).SingleOrDefault();

      if (t == null)
      {
        t = new trn_Table();
        t.Name = r["TABLE_NAME"].ToString();
        aDatabase.trn_Tables.Add(t);
      }

      t.Active = trn_Table.IND_True;
      t.LastScanned = DateTime.Now;

      trn_Field f = (from dbf in t.trn_Fields
                     where dbf.Name.ToUpper().Trim() == r["column_name"].ToString().ToUpper().Trim()
                     select dbf).SingleOrDefault();
          
       
      if (f == null)
      {
        f = new trn_Field();
        f.Name = r["column_name"].ToString();
        t.trn_Fields.Add(f);
      }
      f.Active = trn_Field.IND_True;
      if (r["nullable"].ToString().ToUpper().Substring(0, 1) == "Y")
      {
        f.Nullable = trn_Field.IND_True;
      }
      else
      {
        f.Nullable = trn_Field.IND_False;
      }

      f.FieldType = r["field_type"].ToString(); ;
      try
      {
        f.MaxLength = int.Parse(r["max_length"].ToString());
      }
      catch
      {
        f.MaxLength = 0;
      }
      f.Sequence = 0;
      f.LastScanned = DateTime.Now;
    }
    db.SubmitChanges();
    //con.Close();

      // now SAve;

      /*
        if adptorname == 'mysql'
            sqltext = "SELECT t.table_name, c.COLUMN_NAME as column_name, c.DATA_TYPE as field_type, c.IS_NULLABLE as nullable, c.CHARACTER_MAXIMUM_LENGTH as max_length FROM INFORMATION_SCHEMA.COLUMNS c, INFORMATION_SCHEMA.TABLES t WHERE c.table_name = t.table_name  AND t.table_schema = '"+schemaname+"'"
            return connection.select_all(sqltext)
        end
        if adptorname == 'sqlserver'
            sqltext = " SELECT T.TABLE_NAME AS table_name, C.COLUMN_NAME AS column_name,	C.IS_NULLABLE as nullable, C.DATA_TYPE AS field_type, CHARACTER_MAXIMUM_LENGTH as max_length	FROM INFORMATION_SCHEMA.TABLES T JOIN INFORMATION_SCHEMA.COLUMNS C	ON T.TABLE_NAME = C.TABLE_NAME	WHERE T.TABLE_NAME NOT LIKE 'sys%'	AND T.TABLE_NAME <> 'dtproperties'	AND T.TABLE_SCHEMA <> 'INFORMATION_SCHEMA'	and T.Table_Type = 'BASE TABLE'	ORDER BY T.TABLE_NAME, C.ORDINAL_POSITION"
            return connection.select_all(sqltext)
        end
        if adptorname == 'oci'
            sqltext = "select table_name as table_name, COLUMN_NAME AS column_name, NULLABLE as nullable, DATA_TYPE AS field_type, DATA_LENGTH as max_length from  USER_TAB_COLUMNS"
            return connection.select_all(sqltext)
        end
       * 
       * 
       * def refreshschema
        @connectionresult = 'Schema refreshed.'
        begin
            if params[:db_name]
                db = Db.find_by_db_name(params[:db_name])
            else
                db_id = params[:id] || session[:id] 
                session[:id] = db_id
                db = Db.find(session[:db_id])
            end
			
            # Set existing stuff hidden.
            db.tbls.find_all.each do |row|
              row.active_ind = 0
                row.save
                row.fields.each do |rowf|
                    rowf.active_ind = 0
                    rowf.save
                end
            end
			
            schemainfo = Schemamysql.getschemainfo(db.connectionstring).each do |row|
			
                tbl = db.tbls.find_by_table_name(row['table_name']) || Tbl.new
                tbl.table_name = row['table_name']
                tbl.db_id = db.id
                tbl.active_ind = 1
				
                field = tbl.fields.find_by_field_name(row['column_name']) || Field.new
                field.active_ind = 1
                field.field_name = row['column_name']
                #field.tbl_id = 1
					
                if row['nullable'] == 'YES'||row['nullable'] == 'Y' # Oracle just has Y/N the rest are YES/NO
                    field.nullable = 1
                else 
                    field.nullable = 0
                end	
                field.fieldtype = row['field_type']
                field.length = row['max_length'] || 0
                tbl.fields << field
                tbl.save
            end
        rescue
            @connectionresult = 'The database returned the following error during refresh:'	
            @flash[:note] = $!
        end		
  end	

       */
  }




}


