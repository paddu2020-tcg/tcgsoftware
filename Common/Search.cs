using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using YamahaApp.YEC.Data;
using YamahaApp.YCA.Data;
using YamahaApp.Data;
namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for Search.
	/// </summary>
	public class Search
	{
		
		#region Class Variables

		protected string _contentCatList;
		protected string _searchType;
		protected string _keywords;
        protected string _keywordsForEvent;
        protected string _appId;
		protected string _version;
		protected string _logic;
		protected string _menuCatList;
		protected string _contentType;
		protected string _legacy;
		protected string _query;
		protected string _contentTypeQueried;
		protected string _calledFrom;
		protected System.Int64 _relationTypeId;
		protected IDbConnection _connection;
		protected System.Int64 _contentId;
		protected System.Int64 _startrow;
		protected System.Int64 _endrow;
		protected DataLib _dataAccessManager;
		#endregion
		
		#region constructor

		public Search()
		{
			SearchType = "SEARCH";
			Version = Constants.DEFAULT_VERSION_NAME;
			Logic = "OR";
			Legacy = "N";
		}

		#endregion
		
		#region Class Properties
		public string ContentCatList
		{
			get
			{
				return _contentCatList;
			}
			set
			{
				_contentCatList = value;
			}
		}
		public string SearchType
		{
			get
			{
				return _searchType;
			}
			set
			{
				_searchType = value;
			}
		}
		
		public string Keywords
		{
			get
			{
				return _keywords;
			}
			set
			{
				_keywords= value;
			}
		}

        public string KeywordsForEvent
        {
            get
            {
                return _keywordsForEvent;
            }
            set
            {
                _keywordsForEvent = value;
            }
        }

         public string AppId
        {
            get
            {
                return _appId;
            }
            set
            {
                _appId = value;
            }
        }

        

		public string Version
		{
			get
			{
				return _version;
			}
			set
			{
				_version = value;
			}
		}
		public string Logic
		{
			get
			{
				return _logic;
			}
			set
			{
				_logic = value;
			}
		}
		public string MenuCatList
		{
			get
			{
				return _menuCatList;
			}
			set
			{
				_menuCatList = value;
			}
		}
		public string ContentType
		{
			get
			{
				return _contentType;
			}
			set
			{
				_contentType = value;
			}
		}
		public string Legacy
		{
			get
			{
				return _legacy;
			}
			set
			{
				_legacy = value;
			}
		}
		public string Query
		{
			get
			{
				return _query;
			}
			set
			{
				_query = value;
			}
		}
		public string ContentTypeQueried
		{
			get
			{
				return _contentTypeQueried;
			}
			set
			{
				_contentTypeQueried = value;
			}
		}
		public string CalledFrom
		{
			get
			{
				return _calledFrom;
			}
			set
			{
				_calledFrom = value;
			}
		}

		public System.Int64 RelationTypeId
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

		public System.Int64 ContentId
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
		public System.Int64 StartRow
		{
			get
			{
				return _startrow;
			}
			set
			{
				_startrow = value;
			}
		}
		public System.Int64 EndRow
		{
			get
			{
				return _endrow;
			}
			set
			{
				_endrow = value;
			}
		}
		public IDbConnection Connection
		{
			get
			{
				return _connection;
			}
			set
			{
				_connection = value;
			}
		}

		public virtual DataLib DataAccessManager
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

		#region SearchResult
		public DataTable SearchResult(IDbConnection disposableConnection, string SearchType)
		{
			DataTable searchDataTable = new DataTable();
			if(SearchType == "YECSEARCH")
			{
				searchDataTable = ((YECDataLib)DataAccessManager).GetSearchResult(disposableConnection,SearchType,Keywords,Version,Legacy,ContentType,StartRow,EndRow,CalledFrom,null,null ,ContentTypeQueried ) ;
				//searchDataTable = ((YECDataLib)DataAccessManager).GetSearchResult(disposableConnection,SearchType,Keywords,Version,Legacy,ContentType,CalledFrom,null,null ,ContentTypeQueried ) ;
			}
			else if(SearchType == "YCASEARCH")
			{
				searchDataTable = ((YCADataLib)DataAccessManager).GetSearchResult(disposableConnection,SearchType,Keywords,Version,Legacy,ContentType ,string.Empty,MenuCatList,null ,ContentTypeQueried ) ;
				
			}
			else if(SearchType == "YCASEARCH_V3")
			{				
				searchDataTable = ((YCADataLib)DataAccessManager).GetSearchResult_V3(disposableConnection,SearchType,Keywords,Version,Legacy,ContentType,StartRow,EndRow ,string.Empty,MenuCatList,null ,ContentTypeQueried ,Constants.CDA_CONTENT_TYPE_EXCLUDED_LIST ,string.Empty) ;
			}
            else if (SearchType == "YASISEARCH")
            {
                searchDataTable = ((YCADataLib)DataAccessManager).GetSearchResult(disposableConnection, "YASISEARCH", Keywords, KeywordsForEvent, Version, Legacy, ContentType, StartRow, EndRow, CalledFrom, null, null, ContentTypeQueried, AppId);

            }

			return searchDataTable;
		}
		#endregion
	}


}
