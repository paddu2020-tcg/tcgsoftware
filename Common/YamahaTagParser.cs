using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Web;
using System.Xml.XPath;
using System.Text.RegularExpressions;
using YamahaApp.Data;

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for YamahaTagParserClass1.
	/// </summary>
	public class YamahaTagParser
	{

		#region Class Variables

		protected bool _parseForDislay;
		protected Int64 _categoryIdOfPage;
		protected string _legacy;
		protected string _active;
		protected string _versionName;
		protected string _returnText;
		protected IDbConnection _connection;
		private ListDictionary _cacheTagDictionary;
		protected TemplateContext _templateContext;
		protected DataLib _dataAccessManager;
		#endregion
	
		#region Constants

		private const string TAG_START = "<yamahatag>";
		private const string TAG_END = "</yamahatag>";

		private const string XML_STRING = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
		private const int DEFAULT_GLOSSARY_WIN_HEIGHT = 0;
		private const int DEFAULT_GLOSSARY_WIN_WIDTH = 0;
		private const int DEFAULT_WIN_WIDTH = 0;
		private const int DEFAULT_WIN_HEIGHT = 0;

		#endregion

		#region Constructor

		public YamahaTagParser()
		{
			_cacheTagDictionary = new ListDictionary();
		}

		#endregion
		
		#region Class Properties

		public TemplateContext TemplateContext
		{
			set
			{
				_templateContext = value;
			}
			get
			{
				return _templateContext;
			}
		}

		public bool ParseForDisplay
		{
			set
			{
				_parseForDislay = value;
			}
			get
			{
				return _parseForDislay;
			}
		}


		public Int64 CategoryIdOfPage
		{
			set
			{
				_categoryIdOfPage = value;
			}
			get
			{
				return _categoryIdOfPage;
			}
		}


		public string VersionName
		{
			set
			{
				_versionName = value;
			}
			get
			{
				return _versionName;
			}
		}


		public string Legacy
		{
			set
			{
				_legacy = value;
			}
			get
			{
				return _legacy;
			}
		}


		public string Active
		{
			set
			{
				_active = value;
			}
			get
			{
				return _active;
			}
		}


		public string ReplacedHtml
		{
			get 
			{
				return _returnText;
			}
		}		
		

		public IDbConnection Connection
		{
			set
			{
				_connection = value;
			}
			get
			{
				return _connection;
			}
		}

		public ListDictionary CacheTagDictionary
		{
			get
			{
				return _cacheTagDictionary;
			}
		}
        
		public DataLib DataAccessManager
		{
			get
			{
				return _dataAccessManager;
			}
			set
			{
				_dataAccessManager = value;
			}
		}

		#endregion

		#region Class Methods

		public ArrayList parse(string mainText)
		{
			XmlDocument xmlDoc = new XmlDocument();
			ArrayList listOfDictionaries = new ArrayList();
			ListDictionary controlDictionary = null;
			string tagEmbedText = String.Empty;
			string tagText,tagTextWithTags,tagTextWithTagsForCache;
			string htmlText = String.Empty;
			int posStartTag,posEndTag,posEmbedStart,posEmbedTagInLength,tagLength,posStartText,embedTextLength;
			int posEndText;
			bool parseLoop,embeddedTagFound;

			parseLoop = true;

			tagLength = TAG_START.Length;


			while(parseLoop)
			{
				posStartTag = mainText.ToLower().IndexOf(TAG_START,0);
				
				if(-1 == posStartTag)
				{
					break;
				}
				posEndTag = mainText.ToLower().IndexOf(TAG_END,0);
				
				//starting the search from one place after the 1st <yamahatag>
				string temp = mainText.ToLower().Substring(0,posEndTag);
				posEmbedStart = temp.LastIndexOf(TAG_START,temp.Length);
				if(-1 != posEmbedStart && posEmbedStart != posStartTag)//embedded start tag is indeed found
				{
					embeddedTagFound = true;
					posEmbedTagInLength = posEndTag - posEmbedStart + tagLength + 1; //TAG_END is greater than TAG_START in length by 1
				
					if(0 < posEmbedTagInLength)
					{
						tagEmbedText = mainText.Substring(posEmbedStart,posEmbedTagInLength);
					}
					posStartTag = posEmbedStart;
				}
				else
				{
					embeddedTagFound = false;
				}

				posStartText = posStartTag + tagLength;
				posEndText = posEndTag;
				embedTextLength = posEndText - posStartText;
				if(0 < embedTextLength)
				{
					tagText = mainText.Substring(posStartText,embedTextLength);
					tagTextWithTagsForCache = tagText;
					if(!embeddedTagFound)
					{
						tagEmbedText = TAG_START + tagText + TAG_END;
						tagTextWithTagsForCache = tagEmbedText;
					}
					tagTextWithTags = XML_STRING + tagEmbedText;
					try
					{
						xmlDoc.LoadXml(tagTextWithTags);
						controlDictionary = GetControlDictionary(xmlDoc);
					}
					catch(XmlException e)
					{
						HttpContext.Current.Response.Write (e.Message + "<br>");
						HttpContext.Current.Response.Write(posStartTag.ToString()+"<br>");
						HttpContext.Current.Response.Write(posEndTag.ToString()+"<br>");
						HttpContext.Current.Response.Write(posEmbedStart.ToString()+"<br>");
						HttpContext.Current.Response.Write (HttpUtility.HtmlEncode(tagTextWithTags));
						HttpContext.Current.Response.Write("<br>" + "Error in loading XML...");
						HttpContext.Current.Response.End();
					}
					listOfDictionaries.Add(controlDictionary);
					if(ParseForDisplay)
					{
						htmlText = GetHtml(controlDictionary,tagTextWithTagsForCache);
						if(embeddedTagFound)
						{
							htmlText = "<![CDATA[" + htmlText + "]]>";
						}
						mainText = GetReplacedText( mainText, htmlText, posStartTag, posEndTag+tagLength+1);
					}
					else
					{
						mainText = GetReplacedText( mainText, "", posStartTag, posEndTag+tagLength+1);
					}
					htmlText = String.Empty;
				}
			}
			_returnText = mainText;
			return listOfDictionaries;
		}


		private string GetReplacedText(string mainText,string htmlText,int startPosition,int endPosition)
		{
			string firstCut,secondCut,replacedText;
			firstCut = mainText.Substring(0,startPosition);
			secondCut = mainText.Substring(endPosition);
			replacedText = firstCut + htmlText + secondCut;
			return replacedText;
		}


		private ListDictionary GetControlDictionary(XmlDocument xmlDoc)
		{
			ListDictionary controlDict = new ListDictionary();
			XmlNode nodeType;
			XmlElement root = xmlDoc.DocumentElement;
			
			string tagType = null,tagId = null,textVal = null;

			nodeType = root.SelectSingleNode("//type");

			if(null == nodeType)
			{
				nodeType = root.SelectSingleNode("//TYPE");
			}
			if(null != nodeType)
			{
				tagType = nodeType.InnerText.ToString();
				if(null != tagType)
				{
					if(String.Empty != tagType.Trim())
					{
						if(ValidateTagType(tagType))
						{
							controlDict.Add("TYPE",tagType);
						}
					}
				}
				else
				{
					HttpContext.Current.Response.Write("TYPE: content is missing.");
				}
			}
			else
			{
				HttpContext.Current.Response.Write("TYPE: tag not found");
			}

			nodeType = root.SelectSingleNode("//id");
			if(null == nodeType)
			{
				nodeType = root.SelectSingleNode("//ID");
			}

			if(null != nodeType)
			{
				tagId = nodeType.InnerText.ToString();
				if(null != tagId)
				{
					controlDict.Add("ID",tagId);
				}
				else
				{
					HttpContext.Current.Response.Write("ID: content is missing.");
				}
			}
			else
			{
				HttpContext.Current.Response.Write("ID: tag not found");
			}
			if("LNK" == tagType || "PCT" == tagType || "PMG" == tagType || "CAT" == tagType)
			{
				nodeType = root.SelectSingleNode("//text");
				if(null == nodeType)
				{
					nodeType = root.SelectSingleNode("//TEXT");
				}
				if(null != nodeType)
				{
					textVal = nodeType.InnerText.ToString();
					if(null != textVal)
					{
						if(String.Empty != textVal.Trim())
						{
							controlDict.Add("TEXT",textVal);
						}
					}
					else
					{
						HttpContext.Current.Response.Write("TEXT missing.");
					}
				}
				else
				{
					HttpContext.Current.Response.Write("TEXT: tag not found.");
				}
				nodeType = root.SelectSingleNode("//anchor");
				if(null == nodeType)
				{
					nodeType = root.SelectSingleNode("//ANCHOR");
				}
				if(null != nodeType)
				{
					textVal = nodeType.InnerText.ToString();
					if(null != textVal)
					{
						if(String.Empty != textVal.Trim())
						{
							controlDict.Add("ANCHOR",textVal);
						}
					}
					else
					{
						HttpContext.Current.Response.Write("ANCHOR missing.");
					}
				}
			}
			// RK 20070418 ' Added PMG for params tag
			if("IMG" == tagType || "EMB" == tagType || "LNK" == tagType || "PCT" == tagType || "CAT" == tagType || "PMG" == tagType)
			{
				nodeType = root.SelectSingleNode("//params");
				if(null == nodeType)
				{
					nodeType = root.SelectSingleNode("//PARAMS");
				}
				if(null != nodeType)
				{
					textVal = nodeType.InnerText.ToString();
					if(null != textVal)
					{
						if(String.Empty != textVal.Trim())
						{
							controlDict.Add("PARAMS",textVal);
						}
					}
					else
					{
						HttpContext.Current.Response.Write("PARAMS missing");
					}
				}
			}
			return controlDict;
		}


		private bool ValidateTagType(string tagType)
		{
			bool valid = false;
			if("IMG" == tagType || "PMG" == tagType || "LNK" == tagType || "PCT" == tagType || "TXT" == tagType || "PTI" == tagType || "PTF" == tagType || "PTO" == tagType || "EMB" == tagType || "CAT" == tagType || "CPTO" == tagType)
			{
				valid = true;
			}
			return valid;
		}
				

		private string PrepareHtml(ListDictionary controlDict)
		{
			string html = String.Empty;
			string tagType = String.Empty;
			string paramVal = String.Empty;
			string text = String.Empty;
			string anchorVal = String.Empty;

			Int64 categoryId = 0;
			Int64 contentId = 0;
			Int64 catIdOfPage = 0;

			if(controlDict.Contains("TYPE"))
			{
				tagType = controlDict["TYPE"].ToString().ToUpper();
			}

			if("CPTO" == tagType)
			{
				categoryId = Convert.ToInt64(controlDict["ID"]);
			}
			else
			{
				contentId = Convert.ToInt64(controlDict["ID"]);
			}

			if(null != controlDict["TEXT"])
			{
				text = controlDict["TEXT"].ToString();
			}
			if(null != controlDict["ANCHOR"])
			{
				anchorVal = controlDict["ANCHOR"].ToString();
			}
			if(null != controlDict["PARAMS"])
			{
				paramVal = controlDict["PARAMS"].ToString();
			}			
			
			html = String.Empty;

			if(0 != CategoryIdOfPage)
			{
				if(DataAccessManager.CheckIfContentExistInCategory(Connection,contentId, CategoryIdOfPage))
				{
					catIdOfPage = CategoryIdOfPage;
				}
			}
			
			switch(tagType)
			{
				case "CAT" :
					html = GetTagHrefForCAT(contentId, text, paramVal, VersionName, Legacy, Active, anchorVal);
					if(null != TemplateContext && null != TemplateContext.CacheRegistrar)
					{
						TemplateContext.CacheRegistrar.RegisterCategory(contentId);
					}
					break;
				case "CPTO":
					html = GetTagHrefForCPTO(categoryId, VersionName, Active);
					if(null != TemplateContext && null != TemplateContext.CacheRegistrar)
					{
						TemplateContext.CacheRegistrar.RegisterCategory(categoryId);
					}
					break;
				case "LNK":
				case "PCT":
				case "EMB":
				case "PTF":
				case "PTO":
					html = GetTagHrefForLNKandPCTandEMBandPTFandPTO(contentId, catIdOfPage, text, tagType, paramVal, VersionName, Legacy,Active, anchorVal);
					if(null != TemplateContext && null != TemplateContext.CacheRegistrar)
					{
						TemplateContext.CacheRegistrar.RegisterContent(contentId);
					}
					break;
				case "IMG":
				case "PMG":
				case "PTI":
					html = GetTagHrefForIMGandPMGandPTI(contentId,text, tagType, paramVal, VersionName, Legacy, Active, anchorVal);
					if(null != TemplateContext && null != TemplateContext.CacheRegistrar)
					{
						TemplateContext.CacheRegistrar.RegisterContent(contentId);
					}
					break;
				case "TXT":
					html = GetTagHrefForTXT(contentId, text, tagType, paramVal, VersionName, Legacy, Active);
					if(null != TemplateContext && null != TemplateContext.CacheRegistrar)
					{
						TemplateContext.CacheRegistrar.RegisterContent(contentId);
					}
					break;
				default :
					;
					break;
			}
			return html;
		}
			

		public string GetTagHrefForLNKandPCTandEMBandPTFandPTO(Int64 contentId,Int64 pCatId,string text,string tagType,string paramVal,string version,string legacy,string active,string anchorVal)
		{
			string html = "#";
			string contentType,subContentType,queryString,OIDForContent,urlType,url;
			string docFileLocation,sDocFileName,windowProperties;
			bool fileExists;
			Int64 winHeight,winWidth;

			if(0 == contentId)
			{
				return html;
			}
			//### RK 20061220
			//DataTable contentInfo =DataAccessManager.GetPartialContentDetails(Connection,contentId, version, legacy, active);
			DataTable contentInfo =DataAccessManager.GetPartialContentDetails(Connection,contentId, version, "A", active);
			//### RK 20061220
			if (0 == contentInfo.Rows.Count)
			{
				html = "#";
			}
			else if (1 == contentInfo.Rows.Count)
			{
				DataRow[] allRows = contentInfo.Select();
				foreach(DataRow memberRow in allRows)
				{
					contentType = memberRow["CONTENT_TYPE"].ToString();
					subContentType = memberRow["SUB_CONTENT_TYPE"].ToString();
					queryString = String.Empty;
					OIDForContent = String.Empty;
					urlType = String.Empty;
					fileExists = false;

					//### RK 20060830
					url = memberRow["template_url"].ToString();
					if( (null==url) || (string.Empty == url))
					{
						if (Constants.FILE_SUB_CONTENT_TYPE == subContentType)
						{
							docFileLocation = memberRow["LOCATION"].ToString();
							sDocFileName = memberRow["FILE_NAME"].ToString();
							url = docFileLocation +  sDocFileName;
							urlType = Constants.DISPLAY_TYPE_EXTERNAL;
							fileExists = true;
						}
						else if(Constants.URL_LINKS_CONTENT_TYPE == contentType)
						{
							urlType = memberRow["URL_TYPE_VALUE_STRING"].ToString();
							url = memberRow["URL"].ToString();
							queryString = memberRow["QUERY_STRING"].ToString();
						}
					}
					else
					{
						OIDForContent = "CNTID=" + contentId + "&CTID=" + pCatId + "&VNM=" + version + "&AFLG=" + active;
						fileExists = false;
						urlType = Constants.DISPLAY_TYPE_INTERNAL;
					}

					/*if (Constants.FILE_SUB_CONTENT_TYPE == subContentType)
					{
						docFileLocation = memberRow["LOCATION"].ToString();
						sDocFileName = memberRow["FILE_NAME"].ToString();
						url = docFileLocation +  sDocFileName;
						urlType = Constants.DISPLAY_TYPE_EXTERNAL;
						fileExists = true;
					}
					else if(Constants.URL_LINKS_CONTENT_TYPE == contentType)
					{
						urlType = memberRow["URL_TYPE_VALUE_STRING"].ToString();
						url = memberRow["URL"].ToString();
						queryString = memberRow["QUERY_STRING"].ToString();
					}
					else
					{
						url = memberRow["template_url"].ToString();
						OIDForContent = "CNTID=" + contentId + "&CTID=" + pCatId + "&VNM=" + version + "&AFLG=" + active;
						fileExists = false;
						urlType = Constants.DISPLAY_TYPE_INTERNAL;
					}*/
					
					winWidth = Convert.ToInt64(Utility.IsNull(memberRow["WINDOW_WIDTH"].ToString(),DEFAULT_WIN_WIDTH.ToString()));
                    winHeight= Convert.ToInt64(Utility.IsNull(memberRow["WINDOW_HEIGHT"].ToString(),DEFAULT_WIN_HEIGHT.ToString()));
					
					windowProperties = memberRow["window_properties"].ToString();

					html = String.Empty;

					if ( tagType.Equals("EMB") )
					{
						html = GetHTMLForEMB(url,paramVal);
					}
					else if( tagType.Equals("PTF") )
					{
						html = url;
					}
					else if( tagType.Equals("PTO") )
					{
						html = Utility.GenerateUrl(memberRow["template_application_type"].ToString(), urlType,url,OIDForContent,queryString,null,null,null,false,null,1,null,VersionName);
					}
					else if(0 != contentType.Length)
					{
						switch(contentType)
						{
							case Constants.URL_LINKS_CONTENT_TYPE	:
								html = GetHTMLForURL_LINK(memberRow["template_application_type"].ToString(),contentId, tagType, urlType, url, queryString, text, winHeight, winWidth,paramVal,anchorVal);
								break;
							case Constants.GLOSSARY_CONTENT_TYPE :
								html = GetHTMLForGLOSSARY(urlType, url, OIDForContent, queryString, text, DEFAULT_GLOSSARY_WIN_HEIGHT, DEFAULT_GLOSSARY_WIN_WIDTH,paramVal,anchorVal);
								break;
							case Constants.POPTEXT_CONTENT_TYPE :
								html = GetHTMLForPOPTEXT(memberRow["template_application_type"].ToString(),urlType, url, OIDForContent, queryString, text, winHeight, winWidth,paramVal,anchorVal);
								break;
							default										:
								html = GetHTMLForOTHERS(memberRow["template_application_type"].ToString(),contentId, tagType, urlType, url, OIDForContent, queryString, text, winHeight, winWidth,windowProperties, fileExists,paramVal,anchorVal);
								break;
						}
					}
				}		

			}
			else
			{
				html = String.Empty;
			}
			return html;
		}
				

		private string GetTagHrefForCPTO(Int64 categoryId,string version,string active)
		{
			string categoryOID,categoryUrl;
			ListDictionary dictionary,dictionaryReturned;
			dictionary =  new ListDictionary();
			if (null == categoryId.ToString())
			{
				return "#";
			}
			categoryOID = "CTID=" + categoryId + "&VNM=" + version + "&AFLG=" + active;
			dictionary.Add(categoryId,categoryOID);
			dictionaryReturned = DataAccessManager.GetURLOfCategories(Connection,dictionary,0,"","",false,false);
			categoryUrl = (string) dictionaryReturned[categoryId];			
			return categoryUrl;
		}
				

		private string GetTagHrefForIMGandPMGandPTI(Int64 contentId,string text,string tagType,string paramVal,string version,string legacy,string active,string anchorVal)
		{
			string html = String.Empty;
			string fileName,fileLocation,alt;
			string url = String.Empty;
			string templateUrl,OID,imageName,windowProperties;
			Int64 winHeight,winWidth;
			
			//### RK 20061220
			//DataTable contentInfo =DataAccessManager.GetPartialContentDetails(Connection,contentId, version, legacy, active);
			DataTable contentInfo =DataAccessManager.GetPartialContentDetails(Connection,contentId, version, "A", active);
			//### RK 200612	20
			if (0 == contentId.ToString().Length)
			{
				return "#";
			}
			if (0 == contentInfo.Rows.Count)
			{
				html = "#";
			}
			else if(1 == contentInfo.Rows.Count)
			{
				DataRow[] allRows = contentInfo.Select();
				foreach(DataRow memberRow in allRows)
				{
					imageName = memberRow["NAME"].ToString();
					fileName = memberRow["FILE_NAME"].ToString();
					fileLocation = memberRow["LOCATION"].ToString();	
					alt = memberRow["alternate"].ToString();
					
					if("IMG" == tagType)
					{
						url = fileLocation.Trim() + fileName.Trim();
						html = "<img " + paramVal + " src=\"" + url + "\" border=\"0\"";
						if(null != alt)
						{
							if(String.Empty != alt.Trim())
							{
								html = html + " alt=\"" + alt +"\"/>";
							}
						}
					}
					else if("PTI" == tagType)
					{
						html = fileLocation.Trim() + fileName.Trim();
					}
					else
					{
						//Get the window size
						winWidth = Convert.ToInt64(Utility.IsNull(memberRow["WINDOW_WIDTH"].ToString(),DEFAULT_WIN_WIDTH.ToString()));
						winHeight= Convert.ToInt64(Utility.IsNull(memberRow["WINDOW_HEIGHT"].ToString(),DEFAULT_WIN_HEIGHT.ToString()));
						//Get the window properties
						windowProperties = memberRow["window_properties"].ToString();
						windowProperties = " height=" + winHeight + ", width=" + winWidth + "," + windowProperties;
						templateUrl = memberRow["template_url"].ToString();
						OID = "CNTID=" + contentId + "&VNM=" + version + "&AFLG=" + active;
						//# RK 20070207
						//url = Utility.GenerateUrl(memberRow["template_application_type"].ToString(),memberRow["url_type"].ToString(),templateUrl,OID,null,"","","",false,"",0,"",VersionName);
						url = Utility.GenerateUrl(memberRow["template_application_type"].ToString(), Constants.DISPLAY_TYPE_INTERNAL ,templateUrl,OID,null,"","","",false,"",0,"",VersionName);
						if(null != url)
						{
							if(String.Empty != url.Trim())
							{
								html =  "<a " + paramVal + " href=\"#" + anchorVal  + "\" onclick=" + "\"javascript:openEnlargedImageWnd('" + url + "'); return false;\"" + ">" + text + "</a>";
							}
						}
						else
						{
							html = text;
						}
					}
				}
			}
			else
			{
				html = String.Empty;
			}
			return html;
		}
				

		private string GetHTMLForURL_LINK(string templateApplicationType, Int64 contentId,string tagType,string urlType,string url,string queryString,string text,Int64 winHeight,Int64 winWidth,string paramVal,string anchorVal)
		{

			string html = String.Empty;
			string windowProperties;
			
			if (null != urlType)
			{
				if(String.Empty != urlType.Trim())
				{
					if (Constants.DISPLAY_TYPE_INTERNAL == urlType)
					{
						if ("LNK" == tagType.ToUpper())
						{
							html = Utility.GenerateUrl(templateApplicationType,urlType, url, String.Empty, queryString, String.Empty, text, String.Empty, true,paramVal,0,anchorVal,_versionName);
						}
						else if ("PCT" == tagType.ToUpper())
						{
							if (0 != contentId)
							{
								windowProperties = String.Empty;

								url = Utility.GenerateUrl(templateApplicationType,urlType, url, String.Empty, queryString, String.Empty, text, String.Empty, false,paramVal,0,String.Empty,_versionName);

								html =  "<a " +  paramVal + "href=\"#" + anchorVal + "\" onclick=" + "\"javascript:window.open('" + url + "','new','" + windowProperties + "'); return false;\"" + "> " + text + "</a>";
							}
						}
					}
					else
					{
						html = Utility.GenerateUrl(templateApplicationType,urlType, url, String.Empty, queryString, String.Empty, text, "_blank", true,paramVal,0,anchorVal,_versionName);
					}
				}				
			}
			return html;
		}
		

		private string GetHTMLForGLOSSARY(string urlType,string url,string OIDForContent,string queryString,string text,Int64 winHeight,Int64 winWidth,string paramVal,string anchorVal)
		{
			string html = String.Empty;
			string windowProperties = String.Empty;
			
			if(null == winWidth.ToString() || null == winHeight.ToString())
			{
				windowProperties = String.Empty;
			}
			else
			{
				windowProperties = " height=" + winHeight + ", width=" + winWidth;
			}
			//	Get the href with the querystring
			url = Utility.GenerateUrl(Constants.TEMPLATE_TYPE_VIGNETTE,urlType, url, OIDForContent, queryString, String.Empty, text, String.Empty, false, paramVal, 0, String.Empty,VersionName);
			//	Return the link and open the URL in new window
			html =  "<a " + paramVal + " href=\"#" + anchorVal + "\" onclick=" + "\"javascript:window.open('" + url + "','new','" + windowProperties + "');return false;\"" + "> " + text + "</a>";			
			return html;
		}
		

		private string GetHTMLForPOPTEXT(string templateApplicationType,string urlType,string url,string OIDForContent,string queryString,string text,Int64 winHeight,Int64 winWidth,string paramVal,string anchorVal)
		{
			string html = String.Empty;
			string windowProperties = String.Empty;
			//Get the href with the querystring
			url = Utility.GenerateUrl(templateApplicationType,urlType, url, OIDForContent, queryString, String.Empty, text, String.Empty, false,paramVal,0,String.Empty,VersionName);
			//Return the link and open the URL in new window
			html =  "<a " + paramVal + " href=\"#" + anchorVal  + "\" onclick=" + "\"javascript:window.open('" + url + "','new','" + windowProperties + "');return false;\"" + "> " + text + "</a>";
			return html;
		}		


		private string GetHTMLForEMB(string url,string paramVal)
		{
			string html = String.Empty;
			if (null != url)
			{
				if(String.Empty != url.Trim())
				{
					html = "<embed src=\"" + url + "\"" + paramVal + "></embed>";
				}
			}
			return html;
		}


		private string GetTagHrefForTXT(Int64 contentId,string text,string tagType,string paramVal,string version,string legacy,string active)
		{
			string html = String.Empty;
			string commonText;

			if(null == contentId.ToString())
			{
				return "#";
			}
			
			DataTable contentInfo =DataAccessManager.GetContentDetails(Connection,contentId,version,legacy,active);
			DataRow[] allRows = contentInfo.Select();

			foreach(DataRow memberRow in allRows)
			{
				commonText = memberRow["BODY"].ToString();
				html = html + commonText;
			}			
			contentInfo.Clear();
			contentInfo.Dispose();
			return html;
		}
				

		private string GetHTMLForOTHERS(string templateApplicationType,Int64 contentId,string tagType,string urlType,string url,string OIDForContent,string queryString,string text,Int64 winHeight,Int64 winWidth,string windowProperties,bool fileExists,string paramVal,string anchorVal)
		{
			string html = String.Empty;
			string finalWindowProperties = String.Empty;

			if (fileExists && "PCT" != tagType.ToUpper())
			{
				html = "<a  " + paramVal + " href=\"" + url;
				string localString = anchorVal.Trim();
				if ( String.Empty != localString)
				{
					html = html + "\"#" + anchorVal;
				}
				html = html + "\">" + text + "</a>";
			}
			else
			{
				if("PCT" == tagType.ToUpper())
				{
					if(0 != contentId.ToString().Length)
					{
						if(null == winHeight.ToString() || null == winWidth.ToString())
						{
							finalWindowProperties = String.Empty + windowProperties;
						}
						else
						{
							if(0 == winHeight || 0 == winWidth)
							{
								finalWindowProperties = String.Empty + windowProperties;
							}
							else
							{
								finalWindowProperties = " height=" + winHeight + ", width=" + winWidth + "," + windowProperties;
							}
						}
						if( ! fileExists )
						{
							url = Utility.GenerateUrl(templateApplicationType,urlType, url, OIDForContent, queryString, String.Empty, text, String.Empty, false , paramVal, 0, String.Empty,VersionName);
						}
						if(null != anchorVal)
						{
							url = url + "#" + anchorVal;
						}
						//Return the link and open the URL in new window
						html =  "<a " + paramVal + " href=\"#" + "\" onclick=" + "\"javascript:window.open('" + url + "','new','" + finalWindowProperties + "');return false;\"" + ">" + text + "</a>";
					}
				}
				else
				{
					//Open the content in the same window
					html = Utility.GenerateUrl(templateApplicationType,urlType, url, OIDForContent, queryString, String.Empty, text, String.Empty, true,paramVal,0,anchorVal,VersionName);
				}
			}
			return html;
		}
				

		private string GetTagHrefForCAT(Int64 catId,string text,string paramVal,string version,string legacy,string active,string anchorVal)
		{
			ListDictionary dictionary,dictionaryReturned;

			dictionary = new ListDictionary();

			string categoryOID,html;

			if(0 == catId)
			{
				return "#";
			}
			categoryOID = "CTID=" + catId + "&VNM=" + version + "&AFLG=" + active;
			dictionary.Add(catId,categoryOID);
			dictionaryReturned = DataAccessManager.GetURLOfCategories(Connection,dictionary, 0, "", "", false, false);			
			if(dictionaryReturned.Count > 0)
			{
				html = dictionaryReturned[catId].ToString();
				html = "<a href=\"" + html + "\" " + paramVal + ">" + text + "</a>";
			}
			else
			{
				html = "<a href=\"" + "#" + "\" " + paramVal + ">" + text + "</a>";
			}
			return html;
		}

		
		private string GetHtml(ListDictionary controlDictionary ,string tagTextWithYamahaTag)
		{
			string htmlText;
			//Get cached HTML, in case Tag is cached
			htmlText = GetCachedHtml(tagTextWithYamahaTag);
			if(null == htmlText|| String.Empty == htmlText)
			{
				htmlText = PrepareHtml(controlDictionary);
				CacheTag(tagTextWithYamahaTag,htmlText);
			}
			return htmlText;
		}

		
		private ListDictionary CacheTag(string tagTextWithYamahaTag,string htmlText)
		{
			string trimmedTag;
			trimmedTag = TrimTags(tagTextWithYamahaTag);
			if(_cacheTagDictionary != null)
			{
				if(!_cacheTagDictionary.Contains(tagTextWithYamahaTag))
				{
					_cacheTagDictionary.Add(trimmedTag,htmlText);
				}
			}
			return _cacheTagDictionary;
		}

		
		private string GetCachedHtml(string tagTextWithYamahaTag)
		{
			string tagTextHtml = null;
			string trimmedTag;
			trimmedTag = TrimTags(tagTextWithYamahaTag);
			if(null != _cacheTagDictionary)
			{
				if(_cacheTagDictionary.Contains(trimmedTag))
				{
					if (_cacheTagDictionary[trimmedTag] != null)
					{
						tagTextHtml = (_cacheTagDictionary[trimmedTag]).ToString();
					}
				}
			}
			return tagTextHtml;
		}

		
		private string TrimTags(string tagtextWithYamahaTag)
		{
			string pattern,replaceWith;
			pattern = ">( *)<";
			replaceWith = "><";
			tagtextWithYamahaTag = Regex.Replace(tagtextWithYamahaTag,pattern,replaceWith);
			return tagtextWithYamahaTag;
		}

		
		#endregion
	}
}
