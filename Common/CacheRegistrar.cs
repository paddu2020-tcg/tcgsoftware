using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for CacheRegistrar.
	/// </summary>
	public class CacheRegistrar
	{
		#region Constants

		public const string CONTENT_CACHE_FILE_PREFIX = "CNT";
		public const string CATEGORY_CACHE_FILE_PREFIX= "CAT";
		public const string RELATION_TYPE_CACHE_FILE_PREFIX = "RLTN";
		public const string SPOTLIGHT_TYPE_CACHE_FILE_PREFIX = "SPOT";
		public const string SPECIAL_MENU_CACHE_FILE_PREFIX = "SPMENU";
		public const string ATTR_CACHE_FILE_PREFIX = "ATTR";
		public const string CONTENT_TYPE_CACHE_FILE_PREFIX = "CNTTYP";
		public const string SUB_CONTENT_TYPE_CACHE_FILE_PREFIX = "SUBCNTTYP";

		#endregion
		
		#region Class Variables

		protected ListDictionary _contentDictionary;		
		protected ListDictionary _categoryDictionary;		
		protected ListDictionary _contentTypeDictionary;
		protected ListDictionary _relationShipTypeDictionary;
		protected ListDictionary _sunContentTypeDictionary;
		protected ListDictionary _spotlightTypeDictionary;
		protected ListDictionary _specialMenudictionary;
		protected ListDictionary _attributeDictionary;
		protected string _cacheCurl;

		#endregion

		#region Class Constructor and Destructor

		public CacheRegistrar()
		{
			_contentDictionary = new ListDictionary();
			_categoryDictionary = new ListDictionary();
			_contentTypeDictionary = new ListDictionary();
			_relationShipTypeDictionary = new ListDictionary();
			_sunContentTypeDictionary = new ListDictionary();
			_spotlightTypeDictionary = new ListDictionary();
			_specialMenudictionary = new ListDictionary();
			_attributeDictionary = new ListDictionary();
		}		
		
		~CacheRegistrar()
		{
			_contentDictionary = null;
			_categoryDictionary = null;
			_contentTypeDictionary = null;
			_relationShipTypeDictionary = null;
			_sunContentTypeDictionary = null;
			_spotlightTypeDictionary = null;
			_specialMenudictionary = null;
			_attributeDictionary = null;
		}


		#endregion

		#region Class Properties

		public ListDictionary ContentDictionary
		{
			get
			{
				return _contentDictionary;
			}
			set 
			{
				_contentDictionary = value;
			}
		}

		public ListDictionary CategoryDictionary
		{
			get
			{
				return _categoryDictionary;
			}
			set 
			{
				_categoryDictionary = value;
			}
		}

		public ListDictionary ContentTypeDictionary
		{
			get
			{
				return _contentTypeDictionary;
			}
			set 
			{
				_contentTypeDictionary = value;
			}
		}

		public ListDictionary RelationShipTypeDictionary
		{
			get
			{
				return _relationShipTypeDictionary;
			}
			set 
			{
				_relationShipTypeDictionary = value;
			}
		}

		public ListDictionary SunContentTypeDictionary
		{
			get
			{
				return _sunContentTypeDictionary;
			}
			set 
			{
				_sunContentTypeDictionary = value;
			}
		}

		public ListDictionary SpotlightTypeDictionary
		{
			get
			{
				return _spotlightTypeDictionary;
			}
			set 
			{
				_spotlightTypeDictionary = value;
			}
		}

		public ListDictionary SpecialMenudictionary
		{
			get
			{
				return _specialMenudictionary;
			}
			set 
			{
				_specialMenudictionary = value;
			}
		}

		public ListDictionary AttributeDictionary
		{
			get
			{
				return _attributeDictionary;
			}
			set 
			{
				_attributeDictionary = value;
			}
		}

		public string CacheCurl
		{
			get
			{
				return _cacheCurl;
			}
			set 
			{
				_cacheCurl = value;
			}
		}
		

		#endregion

		#region Class Methods

		public void RegisterContentType(string contentType)
		{
			if(null != contentType &&  0 != contentType.Length)
			{			
				_contentTypeDictionary[contentType]=contentType;
			}
		}

		public void RegisterContent(Int64 contentId)
		{
			if(0 != contentId)
			{
				_contentDictionary [contentId]= contentId;
			}
		}

		public void RegisterCategory(Int64 categoryId)
		{
			if(0 != categoryId)
			{
				_categoryDictionary[categoryId]= categoryId;
			}
		}

		public void RegisterRltnType(Int64 relationId)
		{
			if(0 != relationId)
			{
				_relationShipTypeDictionary[relationId]= relationId;
			}
		}

		public void RegisterSpotlightType(Int64 spotlightTypeId)
		{
			if(0 != spotlightTypeId)
			{
				_spotlightTypeDictionary[spotlightTypeId]= spotlightTypeId;
			}
		}

		//NTC
		public void RegisterSubContentType(Int64 subContentType)
		{
			if(0 != subContentType)
			{
				_sunContentTypeDictionary[subContentType]=subContentType;
			}
		}

		public void RegisterSpecialMenu(Int64 specialMenuId)
		{
			if(0 != specialMenuId)
			{
				_specialMenudictionary[specialMenuId]= specialMenuId;
			}
		}

		public void RegisterAttribute(Int64 attributeId)
		{
			if(0 != attributeId)
			{
				_attributeDictionary[attributeId]= attributeId;
			}
		}

		public string ConvertDictToFileList(ListDictionary obDict,string filePrefix)
		{
			string result = String.Empty;
			int count = 0;
			string key;
			
			if(null != obDict)
			{
				foreach(DictionaryEntry obTemp in obDict)
				{
					key = obTemp.Key.ToString();
					if(0 == count)
					{
						result = result + filePrefix + "_" + obDict.Keys.ToString();
						count = count+1;				
					}
					else
					{
						result = result + "," + filePrefix + "_" + obDict.Keys.ToString();
					}
				}
			}
			return result;
		}

		public ArrayList UrlRegister()
		{
			ArrayList finalContentList;
			ArrayList finalCatList;
			ArrayList finalRltnTypeList;
			ArrayList finalSpotlightTypeList;
			ArrayList finalSpecialMenuList;
			ArrayList finalAttributeList;
			ArrayList finalContentTypeList;			
					
			finalContentList = ConvertDictToArrayList(ContentDictionary, CONTENT_CACHE_FILE_PREFIX, Constants.CACHE_FILE_PATH);
			finalCatList = ConvertDictToArrayList(CategoryDictionary, CATEGORY_CACHE_FILE_PREFIX, Constants.CACHE_FILE_PATH);
			finalRltnTypeList = ConvertDictToArrayList(RelationShipTypeDictionary, RELATION_TYPE_CACHE_FILE_PREFIX, Constants.CACHE_FILE_PATH);
			finalSpotlightTypeList = ConvertDictToArrayList(SpotlightTypeDictionary, SPOTLIGHT_TYPE_CACHE_FILE_PREFIX, Constants.CACHE_FILE_PATH);
			finalSpecialMenuList = ConvertDictToArrayList(SpecialMenudictionary, SPECIAL_MENU_CACHE_FILE_PREFIX, Constants.CACHE_FILE_PATH);
			finalAttributeList = ConvertDictToArrayList(AttributeDictionary, ATTR_CACHE_FILE_PREFIX, Constants.CACHE_FILE_PATH);
			finalContentTypeList = ConvertDictToArrayList(ContentTypeDictionary, CONTENT_TYPE_CACHE_FILE_PREFIX, Constants.CACHE_FILE_PATH);

			ArrayList finalString = new ArrayList();
			foreach(object item in finalContentList)
			{
				finalString.Add(item);
			}
			foreach(object item in finalCatList)
			{
				finalString.Add(item);
			}
			foreach(object item in finalRltnTypeList)
			{
				finalString.Add(item);
			}
			foreach(object item in finalSpotlightTypeList)
			{
				finalString.Add(item);
			}
			foreach(object item in finalSpecialMenuList)
			{
				finalString.Add(item);
			}
			foreach(object item in finalAttributeList)
			{
				finalString.Add(item);
			}
			foreach(object item in finalContentTypeList)
			{
				finalString.Add(item);
			}
			return finalString;
		}
		
		public string ConvertDictToList(ListDictionary obDict)
		{
			string result = String.Empty;
			int count = 0;
			string key;
			
			if(null != obDict)
			{
				foreach(DictionaryEntry obTemp in obDict)
				{
					key=obTemp.Key.ToString();

					if(0 == count)
					{
						result = result + obDict.Keys.ToString();
						count = count + 1;
					}
					else
					{
						result = result + "," + obDict.Keys.ToString();
					}
				}
			}
			return result;
		}

		public ArrayList ConvertDictToArrayList(ListDictionary obDict, string filePrefix, string cacheFilePath)
		{
			ArrayList returnList = new ArrayList();
			if(null != obDict)
			{
				foreach(DictionaryEntry obTemp in obDict)
				{
					returnList.Add(HttpContext.Current.Server.MapPath(cacheFilePath + "/" + filePrefix + "_" + obTemp.Value.ToString()));
				}
			}
			return returnList;
		}

		public string ConvertDictToStringList(ListDictionary obDict)
		{
			string result = String.Empty;
			int count = 0;
			string key;
			
			if(null != obDict)
			{
				foreach(DictionaryEntry obTemp in obDict)
				{
					key = obTemp.Key.ToString();
					if(0 == count)
					{
						result = result + "'" + obDict.Keys.ToString() + "'";
						count = count + 1;
					}
					else
					{
						result = result + "," + "'" + obDict.Keys.ToString() + "'";
					}
				}
			}
			return result;
		}


		#endregion

	}
}

