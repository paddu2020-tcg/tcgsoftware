using System;
using System.Web;
using System.Web.UI;
using System.Data;
using URLRewritingApp;
using YamahaApp.Data;
using System.Configuration; 

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for TemplateContext.
	/// </summary>
	public class TemplateContext
	{

		protected System.Int64 _contentId;
		protected System.Int64 _categoryId;
		protected string _versionName;
		protected string _activeFlag;
		protected string _legacyFlag;
		protected string _historyFlag;
		protected System.Int64 _attributeId;
		protected System.Int64 _relationTypeId;
		protected string _languageCode;
		protected string _contentType;
		protected string _detailType;
		protected string _modelSeriesFlag;
		protected Template _templateInfo;
		protected CacheRegistrar _cacheRegistrar;
		protected DataTable _contentDetail;
		protected DataTable _categoryDetail;
		protected string _browserPath;
		protected DataLib _dataAccessManager;

		private IDbConnection localConnection;
		public TemplateContext(HttpRequest objRequest,IDbConnection connection, DataLib dataAccessManager,string cdstype , bool? IsSecured)
		{
			localConnection = connection;
			_dataAccessManager = dataAccessManager;
			string url = String.Empty;

			string contentId = objRequest.QueryString["CNTID"];

			if (null != contentId && String.Empty != contentId)
			{
				_contentId = Convert.ToInt64(contentId);
			}
			
			string categoryId = objRequest.QueryString["CTID"];
			
			if (null != categoryId && String.Empty != categoryId)
			{
				_categoryId = Convert.ToInt64(categoryId);
			}
			else
			{
				_categoryId = DataAccessManager.GetOwnerCatIdForContent(connection,_contentId);
			}

			string versionName = objRequest.QueryString["VNM"];

			if (null != versionName && String.Empty != versionName)
			{
				if(cdstype == Constants.CDS_TYPE_LIVE)
				{
					_versionName = Constants.LIVE_VERSION_NAME;
				}
				else
				{
					_versionName = versionName;
				}
			}
			else
			{
				_versionName = Constants.DEFAULT_VERSION_NAME;
			}

			string langCode = objRequest.QueryString["lang_code"];

			if (null != langCode && String.Empty != langCode)
			{
				_languageCode = langCode;
			}
			else
			{
				_languageCode = Constants.DEFAULT_LANGUAGE_CODE;
			}

			string activeFlag = objRequest.QueryString["AFLG"];

			if (null != activeFlag && String.Empty != activeFlag)
			{
				_activeFlag = activeFlag;
			}
			else
			{
				_activeFlag = Constants.DEFAULT_ACTIVE_FLAG;
			}

			string legacyFlag = objRequest.QueryString["LGFL"];

			if (null != legacyFlag && String.Empty != legacyFlag)
			{
				_legacyFlag = legacyFlag;
			}
			else
			{
				_legacyFlag = Constants.DEFAULT_LEGACY_FLAG;
			}

			string historyFlag = objRequest.QueryString["HST"];

			if (null != historyFlag && String.Empty != historyFlag)
			{
				_historyFlag = historyFlag;
			}
			else
			{
				_historyFlag= Constants.DEFAULT_HISTORY_FLAG;
			}

			string attributeId = objRequest.QueryString["ATRID"];

			if (null != attributeId && String.Empty != attributeId)
			{
				_attributeId = Convert.ToInt64(attributeId);
			}

			string rltnId = objRequest.QueryString["RLTID"];

			if (null != rltnId && String.Empty != rltnId)
			{
				_relationTypeId  = Convert.ToInt64(rltnId);
			}

			string contentType = objRequest.QueryString["CNTYP"];

			if (null != contentType && String.Empty != contentType)
			{
				_contentType = contentType;
			}
			else
			{
				_contentType = Constants.PRODUCT_CONTENT_TYPE;
			}

			string detailType = objRequest.QueryString["DETYP"];

			if (null != detailType && String.Empty != detailType)
			{
				_detailType = detailType;
			}

			string modelSeriesFlag = objRequest.QueryString["DTYP"];

			if (null != modelSeriesFlag && String.Empty != modelSeriesFlag)
			{
				_modelSeriesFlag = modelSeriesFlag;
			}

			if(null != HttpContext.Current.Items[Constants.BROWSER_PATH])
			{
				_browserPath = HttpContext.Current.Items[Constants.BROWSER_PATH].ToString();
			}
			else
			{
				_browserPath = HttpContext.Current.Request.Url.PathAndQuery;
			}

			_templateInfo = HandlerUtils.GetFriendlyURLTemplate(HttpContext.Current.Request.Url.AbsolutePath,VersionName);
			if(null == _templateInfo)
			{
				if(null != HttpContext.Current.Items[Constants.ORIGINAL_URL])
				{
					_templateInfo = HandlerUtils.GetTemplate(HttpContext.Current.Items[Constants.ORIGINAL_URL].ToString());
					url = HttpContext.Current.Items[Constants.ORIGINAL_URL].ToString() + "?" + HttpContext.Current.Request.QueryString;
				}
				else
				{
					_templateInfo = HandlerUtils.GetTemplate(HttpContext.Current.Request.Url.AbsolutePath);
					url = HttpContext.Current.Request.Url.AbsolutePath + "?" + HttpContext.Current.Request.QueryString;
				}
			}
			

			_cacheRegistrar = new CacheRegistrar();

			if (0 != ContentId)
			{
				//_contentDetail =DataAccessManager.GetPartialContentDetails(connection,ContentId,VersionName,LegacyFlag,ActiveFlag);
				_contentDetail = DataAccessManager.GetPartialContentDetails_v3(connection,ContentId,VersionName,LegacyFlag,ActiveFlag);
				_contentDetail = Utility.ReplaceYamahaTag(localConnection,CategoryId,_contentDetail,VersionName,LegacyFlag,ActiveFlag,null,DataAccessManager);
			}
			if (0 != CategoryId)
			{
				_categoryDetail =DataAccessManager.GetCurrentCategoryDetails(connection,CategoryId);
				_categoryDetail = Utility.ReplaceYamahaTag(localConnection,CategoryId,_categoryDetail,VersionName,LegacyFlag,ActiveFlag,null,DataAccessManager);
			}
			//### PN 20060321
			if(IsSecured==true && !HttpContext.Current.Request.IsSecureConnection)
			{
				ReloadInHttps(url);
			}
			else
			{
				if(null != TemplateInfo && "YES" == System.Configuration.ConfigurationManager.AppSettings.Get("USE_SSL").ToString())
				{
					if (  (TemplateInfo.IsSecured)==true && !HttpContext.Current.Request.IsSecureConnection)
					{
						ReloadInHttps(url);
					}
                    if ((TemplateInfo.IsSecured) == false && HttpContext.Current.Request.IsSecureConnection)
                    {
                        ReloadInHttp(url);
                    }
                    //else
                    //{
                    //    //HttpContext.Current.Response.Redirect(url);
                    //}
				}
			}
			//### PN 20060321
		}
		
		public void ReloadInHttps (string url)
		{
            //HttpContext.Current.Response.Redirect(Constants.HTTPS_STRING + HttpContext.Current.Request.Url.Host + url);
			HttpContext.Current.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Get("HTTPS_STRING").ToString() + HttpContext.Current.Request.Url.Host + url);
		}

		public void ReloadInHttp (string url)
		{
			//HttpContext.Current.Response.Redirect(Constants.HTTP_STRING + HttpContext.Current.Request.Url.Host + url);
			HttpContext.Current.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings.Get("HTTP_STRING").ToString()+ HttpContext.Current.Request.Url.Host + url);
			
		}

		public Int64 ContentId
		{
			get
			{
				return _contentId;
			}
			set
			{
				_contentId = value;
			}
		}

		public Int64 CategoryId
		{
			get
			{
				return _categoryId;
			}
			set
			{
				_categoryId = value;
			}
		}

		public string ContentType
		{
			get
			{
				return _contentType;
			}
		}

		public string VersionName
		{
			get
			{
				return _versionName;
			}
		}

		public string ActiveFlag
		{
			get
			{
				return _activeFlag;
			}
		}

		public string LegacyFlag
		{
			get
			{
				return _legacyFlag;
			}
		}

		public string HistoryFlag
		{
			get
			{
				return _historyFlag;
			}
		}

		public Int64 AttributeId
		{
			get
			{
				return _attributeId;
			}
		}

		public Int64 RelationTypeId
		{
			get
			{
				return _relationTypeId;
			}
			set
			{
                _relationTypeId = value;
			}
		}

		public string LanguageCode
		{
			get
			{
				return _languageCode;
			}
		}

		public Template TemplateInfo
		{
			get
			{
				return _templateInfo;
			}
            set
            {
                _templateInfo = value;
            }
		}

		public CacheRegistrar CacheRegistrar
		{
			get
			{
				return _cacheRegistrar;
			}
			set
			{
				_cacheRegistrar = value;
			}
		}

		public DataTable ContentDetail
		{
			get
			{

				if (0 != ContentId && _contentDetail == null)
				{
					_contentDetail =DataAccessManager.GetPartialContentDetails(localConnection,ContentId,VersionName,LegacyFlag,ActiveFlag);
					_contentDetail = Utility.ReplaceYamahaTag(localConnection,CategoryId,_contentDetail,VersionName,LegacyFlag,ActiveFlag,null,DataAccessManager);
				}
				return _contentDetail;
			}
		}
        
		public DataTable CategoryDetail
		{
			get
			{
				if (0 != CategoryId && _categoryDetail == null)
				{
					_categoryDetail =DataAccessManager.GetCurrentCategoryDetails(localConnection,CategoryId);
					_categoryDetail = Utility.ReplaceYamahaTag(localConnection,CategoryId,_categoryDetail,VersionName,LegacyFlag,ActiveFlag,null,DataAccessManager);
				}
				return _categoryDetail;
			}
		}


		public string BrowserPath
		{
			get
			{
				return _browserPath;
			}
		}

		public string DetailType
		{
			get
			{
				return _detailType;
			}
		}

		public string ModelSeriesFlag
		{
			get
			{
				return _modelSeriesFlag;
			}
		}

		public string PrinterFriendlyURL
		{
			get
			{
				string printURL;
				printURL = _browserPath;
			
				if (printURL.IndexOf("?") != -1)
				{
					if(printURL.IndexOf("&PF=") == -1)
						printURL = printURL + "&PF=1";
				}
				else
				{
						printURL = printURL + "?PF=1";
				}
				return printURL;
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
	}
}
