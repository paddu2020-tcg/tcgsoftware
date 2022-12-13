using System;
using System.Text.RegularExpressions;  
using System.Text; 
using System.Web;  
using System.Collections; 
using System.Collections.Specialized ; 
using System.Data; 
using YamahaApp.Common;  
namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for SQLGenerator.
	/// </summary>
	public class SQLGenerator
	{
		public SQLGenerator()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		// Assumptions :
		// 1 )	Alias name column names should be used in capital letters.
		// 2 )	Alias should be used betwen <~ & ~> tags.
		// 3 )	Column names should be used between <% and %> tags.

		#region GetReplacedSQL
		public static string GetReplacedSQL(string sqlString , IDbConnection connection ) 
		{
			string formattedSQL = string.Empty; 
			string returnString = string.Empty; 
			ListDictionary contentColumnDefination = new ListDictionary(); 
			formattedSQL = sqlString.ToUpper(); 

			// I used to replace these tag becuase i was not able to use ~ in my replace pattern

			formattedSQL  = sqlString.Replace("<~","<TABLENAME>");
			formattedSQL  = formattedSQL.Replace("~>","</TABLENAME>");

			formattedSQL  = formattedSQL.Replace("<%","<COLUMN>");
			formattedSQL  = formattedSQL.Replace("%>","</COLUMN>");

			string tableNamePattern = @"<TABLENAME>(?><TABLENAME>(?<DEPTH>)|</TABLENAME>(?<-DEPTH>)|.?)*(?(DEPTH)(?!))</TABLENAME>";
			string columnNamePattern = @"<COLUMN>(?><COLUMN>(?<DEPTH>)|</COLUMN>(?<-DEPTH>)|.?)*(?(DEPTH)(?!))</COLUMN>";

			// List all column names
			MatchCollection columnNameInSQLTreatedAsContentType = Regex.Matches(formattedSQL,columnNamePattern);  
			ListDictionary columnDictionary = new ListDictionary(); 
			foreach(Match match in columnNameInSQLTreatedAsContentType)
			{
				string columnNameWithTableName = match.ToString().Replace("<COLUMN>","") ;
				columnNameWithTableName = columnNameWithTableName.Replace("</COLUMN>","") ;
				try
				{
					columnDictionary.Add(columnNameWithTableName,columnNameWithTableName); 
				}
				catch
				{
					// In Case when the key is already added. We can ignore this case.
				}
			}

			// List all Content Types
			MatchCollection tableNameInSQLTreatedAsContentType = Regex.Matches(formattedSQL,tableNamePattern);
			// This loop is only for getting columnInformation in advance so that we dont have to 
			// execute sql query if the same content type is used more than once
			string contentType =  string.Empty; 
			DataTable columnInfo = new DataTable();
			DataColumn contentTypeColumn = new DataColumn("CONTENT_TYPE") ;
			DataColumn contentNameColumn = new DataColumn("COLUMN_NAME") ;
			DataColumn contentActualNameColumn = new DataColumn("COLUMN_ACTUAL_NAME") ;
			columnInfo.Columns.Add(contentTypeColumn);  
			columnInfo.Columns.Add(contentNameColumn);
			columnInfo.Columns.Add(contentActualNameColumn);

			foreach(Match match in tableNameInSQLTreatedAsContentType)
			{
				contentType =  match.ToString();
				contentType =  contentType.Replace("<TABLENAME>","");
				contentType =  contentType.Replace("</TABLENAME>","");
				contentType = contentType.Substring(0,contentType.LastIndexOf("_")).ToUpper(); 
				
				DataView dv  = columnInfo.DefaultView; 
				dv.RowFilter= "CONTENT_TYPE='"+ contentType.ToUpper() + "'";  
				if(dv.Count == 0 )
				{
					Data.DataLib dataAccessManager;
					dataAccessManager = DataAccessManagerFactory.GetDataAccessManager("YCA"); 
					DataTable columnInfoDataTable =  dataAccessManager.GetContentColumns(connection , contentType); 
					 
					foreach(DataRow dr in columnInfoDataTable.Select())
					{	
						DataRow columnInfoRow =  columnInfo.NewRow(); 
						columnInfoRow["CONTENT_TYPE"] = contentType;
						columnInfoRow["COLUMN_NAME"] = dr["COLUMN_NAME"];
						columnInfoRow["COLUMN_ACTUAL_NAME"] = dr["COLUMN_ACTUAL_NAME"];
						columnInfo.Rows.Add(columnInfoRow);
					}
				}
			}
			// End 
			ListDictionary contentTypeDictionary = new ListDictionary(); 
			foreach(Match match in tableNameInSQLTreatedAsContentType)
			{
                contentType =  match.ToString();
				contentType =  contentType.Replace("<TABLENAME>","");
				contentType =  contentType.Replace("</TABLENAME>","");
				string aliasName = contentType;
				// Now Get the Exact Content Type : 
				contentType = contentType.Substring(0,contentType.LastIndexOf("_")).ToUpper(); 

				try
				{
					contentTypeDictionary.Add(aliasName,contentType);
				}
				catch
				{
					// In Case when the key is already added. We can ignore this case.
				}

				DataView colunInfoView; 
				foreach ( DictionaryEntry de in  columnDictionary )
				{
					string columnName =  de.Value.ToString().Substring(de.Value.ToString().LastIndexOf(".")+1);
					string matchString = aliasName + "." +  columnName;
					if(de.Value.ToString().ToUpper()  == matchString.ToUpper())
					{
						try
						{
							colunInfoView = columnInfo.DefaultView; 
							colunInfoView.RowFilter = "CONTENT_TYPE='" + contentType + "' AND COLUMN_NAME='" + columnName.ToUpper() + "'";
							contentColumnDefination.Add(matchString,aliasName + "." + colunInfoView[0]["COLUMN_ACTUAL_NAME"].ToString());
						}
						catch(Exception ex)
						{
							// Ignore if added once
							HttpContext.Current.Response.Write(ex.Message);     
						}
					}
				}
			}
			
			returnString = sqlString; 
			
			// We can remove contentTypeDictionary object and move replace function inside the above loop

			foreach ( DictionaryEntry de in  contentTypeDictionary )
			{
				HttpContext.Current.Response.Write (de.Key + "-" + de.Value + "<br>" );
				returnString = returnString.Replace("<~" + de.Key.ToString() +"~>",de.Key.ToString());   
			}
			foreach ( DictionaryEntry de in  contentColumnDefination )
			{
				HttpContext.Current.Response.Write (de.Key + "-" + de.Value  + "<br>" );
				returnString = returnString.Replace("<%" + de.Key.ToString()+ "%>",de.Value.ToString());   
			}
			
			return returnString;
		}
		#endregion
		

	}
}
