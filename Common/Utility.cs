using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Security.Principal;
using System.DirectoryServices;
using System.Security.Cryptography;
using YamahaApp.Data;
using System.Configuration;

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	public class Utility
	{
		//private static int DEFAULT_PASSWORD_LENGTH  = 8;

		//private static string LCASE_PASSWORD_CHARS  = "abcdefgijkmnopqrstwxyz";
		//private static string UCASE_PASSWORD_CHARS  = "ABCDEFGHJKLMNPQRSTWXYZ";
		//private static string PASSWORD_CHARS_NUMERIC= "0123456789";

		public Utility()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region GenerateUrl
		public static string GenerateUrl(string applicationType, string urlType,string url,string OID,string queryString,string className,string linkDisplay,string target,bool href,string parameterString,int defaultProtocol,string anchor,string versionName)
		{
			return GenerateUrl(applicationType,urlType,url,OID,queryString,className,linkDisplay,target,href,parameterString,defaultProtocol,anchor,versionName,string.Empty);
		}

		public static string GenerateUrl(string applicationType, string urlType,string url,string OID,string queryString,string className,string linkDisplay,string target,bool href,string parameterString,int defaultProtocol,string anchor,string versionName, string urlFlag)
		{
			string returnUrl = String.Empty;
			string classString = String.Empty;
			string targetString = String.Empty;
			string protocol = String.Empty;
			string newOID,finalUrl = String.Empty;

			if (Constants.DISPLAY_TYPE_EXTERNAL == urlType)
			{
				//protocol = Constants.HTTP_STRING;
                protocol = ConfigurationManager.AppSettings["HTTP_STRING"];
			}
			else
			{
                defaultProtocol = 2;

				if (1 == defaultProtocol)
				{
					//protocol = Constants.HTTP_STRING + HttpContext.Current.Request.Url.Host;
					protocol = ConfigurationManager.AppSettings["HTTP_STRING"] + HttpContext.Current.Request.Url.Host;
				}
				else if (2 == defaultProtocol)
				{
					//protocol = Constants.HTTPS_STRING + HttpContext.Current.Request.Url.Host;
					protocol = ConfigurationManager.AppSettings["HTTPS_STRING"] + HttpContext.Current.Request.Url.Host;
				}
				else if (0 == defaultProtocol)
				{
					protocol = String.Empty;

					try
					{

						if(HttpContext.Current.Request.IsSecureConnection)
						{
							//protocol = Constants.HTTP_STRING + HttpContext.Current.Request.Url.Host;
							protocol = ConfigurationManager.AppSettings["HTTP_STRING"] + HttpContext.Current.Request.Url.Host;
						}
					}
					catch
					{
							protocol = ConfigurationManager.AppSettings["HTTP_STRING"] + "www.yamaha.com";
					}
				}
			}

			if(null == url || String.Empty == url.Trim())
			{
				returnUrl = "#";
			}
			else
			{
				if (Constants.DISPLAY_TYPE_EXTERNAL == urlType)
				{
					returnUrl = url;
				}
				else
				{
					
					if (urlFlag != null)
					{
						if (urlFlag.Equals("CATEGORY"))
						{
							OID = "VNM=" + versionName;
						}
					}

					newOID = OID;
					if(-1 == url.IndexOf(".html"))
					{
						if(-1 == url.IndexOf(".aspx"))
						{
							finalUrl = url + ".html";
						}
						else
						{
							finalUrl = url;
						}
					}
					else
					{
						if(-1 != url.IndexOf("?"))
						{
							finalUrl = url.Substring(0,url.IndexOf("?"));
						}
						else
						{
							finalUrl = url;
						}
					}
					if(-1 != url.IndexOf("?"))
					{
						url = url.Substring(url.IndexOf("?")+1);
					}
					ListDictionary obDicCurrent = ConvertStringToDictionary(url,"&","=");
					ListDictionary obDicNew = ConvertStringToDictionary(newOID,"&","=");
					ListDictionary obDicCombined = CompareAndCombineDictionary(obDicCurrent,obDicNew);
					ListDictionary obDicQueryString = ConvertStringToDictionary(queryString,"&","=");
					obDicCombined = CompareAndCombineDictionary(obDicCombined,obDicQueryString);
					newOID = ConvertDictToString(obDicCombined);
					
					if(String.Empty != newOID)
					{
						finalUrl = finalUrl + "?" + newOID;
					}
					if(-1 != finalUrl.IndexOf("&VNM=LIVE",1))
					{
						finalUrl = finalUrl.Replace("&VNM=LIVE","");
					}
                    //if(-1 != finalUrl.IndexOf("?VNM=LIVE",1))
                    //{
                    //    finalUrl = finalUrl.Replace("?VNM=LIVE","?");
                    //}
					if(-1 != finalUrl.IndexOf("&LGFL=N",1))
					{
						finalUrl = finalUrl.Replace("&LGFL=N","");
					}
					if(-1 != finalUrl.IndexOf("&AFLG=Y",1))
					{
						finalUrl = finalUrl.Replace("&AFLG=Y", "");
					}
					if(-1 != finalUrl.IndexOf("&HST=N",1))
					{
						finalUrl = finalUrl.Replace("&HST=N", "");
					}
					if(-1 != finalUrl.IndexOf("&CNTYP=&",1))
					{
						finalUrl = finalUrl.Replace("&CNTYP=&", "&");
					}
					if(-1 != finalUrl.IndexOf("?CNTYP=&",1))
					{
						finalUrl = finalUrl.Replace("?CNTYP=&", "?");
					}
					if(-1 != finalUrl.IndexOf("?&",1))
					{
						finalUrl = finalUrl.Replace("?&", "?");
					}
					if(-1 != finalUrl.IndexOf("&CTID=0",1))
					{
						finalUrl = finalUrl.Replace("&CTID=0", "");
					}
					if(-1 != finalUrl.IndexOf("?CTID=0",1))
					{
						finalUrl = finalUrl.Replace("?CTID=0", "?");
					}
					if(-1 != finalUrl.IndexOf("&CNTID=0",1))
					{
						finalUrl = finalUrl.Replace("&CNTID=0", "");
					}
					if(-1 != finalUrl.IndexOf("?CNTID=0",1))
					{
						finalUrl = finalUrl.Replace("?CNTID=0", "?");
					}
					if (finalUrl.EndsWith("?"))
					{
						finalUrl = finalUrl.Substring(0,finalUrl.Length-1);
					}
					returnUrl = protocol + finalUrl;
				}
			}
			if (null != anchor && String.Empty != anchor.Trim())
			{
				returnUrl =  returnUrl + "#" + anchor;
			}

			if (null != className && String.Empty != className.Trim())
			{
				classString = " class=\"" + className +"\"";
			}
			else
			{
				classString = String.Empty;
			}
	
			if(null != target && String.Empty != target.Trim())
			{
				targetString = " target=\"" + target + "\"";
			}
			else
			{
				targetString = String.Empty;
			}
	
			//Return Href if parameter is true else return just makecurl url
			if (true == href)
			{
				returnUrl = "<a href=\"" + returnUrl + "\"" + classString + targetString + parameterString + ">" + linkDisplay + "</a>";
			}
			else
			{
				returnUrl = returnUrl;
			}
			return returnUrl;
		}


		public static string GenerateUrl(string applicationType, string urlType,string templateUrl,string OID,string queryString,string className,string linkDisplay,string target,bool href,string parameterString,int defaultProtocol,string anchor,string versionName, string urlFlag,string friendlyUrl,string friendlyUrlFlag)
		{
			string returnUrl = String.Empty;
			string classString = String.Empty;
			string targetString = String.Empty;
			string protocol = String.Empty;
			string newOID,finalUrl = String.Empty;
			string url = string.Empty;

			if ( friendlyUrlFlag.Trim().ToString().ToUpper() == "Y" )
			{
				url = friendlyUrl.Trim();
			}
			else
			{
				url = templateUrl.Trim();
				if(null == url || String.Empty == url.Trim())
				{
					returnUrl = "#";
				}
				else
				{
					returnUrl = GenerateUrl(applicationType,urlType,url,OID,queryString,"","","",false,"",0,"",versionName);
					return returnUrl;
				}		
			}

			if (Constants.DISPLAY_TYPE_EXTERNAL == urlType)
			{
                protocol = ConfigurationManager.AppSettings["HTTP_STRING"];
			}
			else
			{
				if (1 == defaultProtocol)
				{
					//protocol = Constants.HTTP_STRING + HttpContext.Current.Request.Url.Host;
					protocol = ConfigurationManager.AppSettings["HTTP_STRING"] + HttpContext.Current.Request.Url.Host;
				}
				else if (2 == defaultProtocol)
				{
					protocol = ConfigurationManager.AppSettings["HTTPS_STRING"] + HttpContext.Current.Request.Url.Host;
				}
				else if (0 == defaultProtocol)
				{
					protocol = String.Empty;

					if(HttpContext.Current.Request.IsSecureConnection)
					{
						protocol = ConfigurationManager.AppSettings["HTTP_STRING"] + HttpContext.Current.Request.Url.Host;
					}
				}
			}

			if(null == url || String.Empty == url.Trim())
			{
				returnUrl = "#";
			}
            else
            {
                if (versionName.Trim().ToUpper() != "LIVE")
                {
                    newOID = "?VNM=" + versionName;
                }
                else
                {
                    newOID = string.Empty;
                }

                finalUrl = url + newOID;
                returnUrl = protocol + finalUrl;
            }
            returnUrl = protocol + returnUrl;
			return returnUrl;
		}

		#endregion

		#region TCGUrl
		//		public static string TCGCurl(string psApplicationType, string psTemplatePath,string psOID,string psQueryString,string psProtocol)
		//		{
		//			if (sNewOID != null && sNewOID.Length != 0)
		//			{
		//				if(sNewOID.IndexOf("&VNM=LIVE",1) != -1) sNewOID = sNewOID.Replace("&VNM=LIVE","");
		//				if(sNewOID.IndexOf("&LGFL=N",1) != -1) 	sNewOID = sNewOID.Replace("&LGFL=N","");
		//				if(sNewOID.IndexOf("&AFLG=Y",1) != -1) 	sNewOID = sNewOID.Replace("&AFLG=Y", "");
		//				if(sNewOID.IndexOf("&HST=N",1) != -1)  sNewOID = sNewOID.Replace("&HST=N", "");
		//				if(sNewOID.IndexOf("&CNTYP=&",1) != -1) sNewOID = sNewOID.Replace("&CNTYP=&", "&");
		//			}
		//			if(psApplicationType == Constants.TEMPLATE_TYPE_VIGNETTE)
		//			{
		//				sNewOID = HttpUtility.UrlEncode(sNewOID);
		//				sUrl = psTemplatePath + "/0,," + sNewOID + ",00.html";
		//				if (psQueryString != "")
		//				{
		//					sUrl = sUrl + "?" + psQueryString;
		//				}
		//			}
		//			else if(psApplicationType == Constants.APPLICATION_TYPE_YEC)
		//			{
		//				sUrl = psTemplatePath + "/default.html";
		//				if(sNewOID != "")
		//				{
		//					sUrl = sUrl + "?" + sNewOID;
		//				}
		//				if(sNewOID == "" && psQueryString != "")
		//				{
		//					sUrl = sUrl + "?" + psQueryString;
		//				}
		//				else if (sNewOID != "" && psQueryString != "")
		//				{
		//					sUrl = sUrl + "&" + psQueryString;
		//				}
		//			}
		//			return psProtocol + sUrl;
		//		}
		#endregion

		#region MakeCurl
		//		public static string MakeCurl(string psApplicationType, string psTemplatePath,string psOID,string psQueryString)
		//		{
		//			string sUrl = "";
		//			if(psApplicationType == Constants.TEMPLATE_TYPE_VIGNETTE)
		//			{
		//				psOID = HttpUtility.UrlEncode(psOID);
		//				sUrl = psTemplatePath + "/0,," + psOID + ",00.html";
		//				if (psQueryString != "")
		//				{
		//					sUrl = sUrl + "?" + psQueryString;
		//				}
		//			}
		//			else if(psApplicationType == Constants.TEMPLATE_TYPE_NORMAL)
		//			{
		//				sUrl = psTemplatePath + "/default.html";
		//				if(psOID != "")
		//				{
		//					sUrl = sUrl + "?" + psOID;
		//				}
		//				if(psOID == "" && psQueryString != "")
		//				{
		//					sUrl = sUrl + "?" + psQueryString;
		//				}
		//				else if (psOID != "" && psQueryString != "")
		//				{
		//					sUrl = sUrl + "&" + psQueryString;
		//				}
		//			}
		//			return sUrl;
		//			
		//		}
		#endregion

        

        public static string emailPasswordResetKey(IDbConnection connection, string email, string emailTemplateName)
        {
            string message = string.Empty;
            string pwdAuthKey = string.Empty;
            string status = string.Empty;
            YamahaApp.Data.DataLib dl = new DataLib();
            try
            {
                pwdAuthKey = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString();
                DataTable userInfo = YamahaApp.Data.DataLib.GetUserUsingEmailGeneric(connection, email.Trim().ToLower());
                if (userInfo.Rows.Count != 0)
                {
                    YamahaApp.Data.DataLib.AddRenewAuthKey(connection, pwdAuthKey, userInfo.Rows[0]["user_id"].ToString());
                    DataTable dtMsgDetails = dl.GetEmailTemplateDetails(connection, emailTemplateName);
                    message = dtMsgDetails.Rows[0]["details"].ToString();
                    message = message.Replace("%reset_link%", (String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("RESETPWD")) ? "https://www.yamaha.com/paragon/myaccount/resetpassword.html?password_auth_key=" : ConfigurationManager.AppSettings.Get("RESETPWD")) + pwdAuthKey);
                    message = message.Replace("%first_name%", userInfo.Rows[0]["first_name"].ToString());
                    message = message.Replace("%accounturl%", ConfigurationManager.AppSettings.Get("ACCOUNTSETTINGS"));
                    if (GenericEmailRequest.GenericEmail.CreateEmailRequest(connection, emailTemplateName, email.Trim().ToLower(), dtMsgDetails.Rows[0]["EMAIL_FROM"].ToString(), dtMsgDetails.Rows[0]["email_subject"].ToString(), message, null))
                        status = "success";
                    else
                        status = "error";
                }
                else
                    status = "not found";
            }
            catch (Exception)
            { status = "error"; }
            return status;
        }

		public static string GetMachineName()
		{
			string machineName = string.Empty;
			machineName = System.Environment.MachineName.ToString();   
			switch(machineName.ToUpper())
			{
				case "YAMAHAWEB1":
					machineName =  "w1";
					break;
				case "YAMAHAWEB2":
					machineName =  "w2";
					break;
				case "YAMAHAWEB3":
					machineName =  "w3";
					break;
			}
			return machineName;
 		}

		public static string IsNull (string first,string second)
		{
			string returnValue = String.Empty;
			returnValue = first;
			if (String.Empty == first || null == first)
			{
				returnValue = second;
			}
			return returnValue;
		}

		public static DataTable ReplaceYamahaTag(IDbConnection connection, Int64 GlobalCategoryId,DataTable data, string versionName, string leagcyFlag, string activeFlag, TemplateContext context, DataLib dataAccessManager)
		{
			foreach (DataRow row in data.Select())
			{
				for (int i=0 ; i<data.Columns.Count; i++)
				{
					if (data.Columns[i].DataType == Type.GetType("System.String"))
					{
						YamahaTagParser tagParser = new YamahaTagParser();
						tagParser.DataAccessManager = dataAccessManager;
						tagParser.TemplateContext = context;
						tagParser.Connection = connection;
						tagParser.ParseForDisplay = true;
						tagParser.CategoryIdOfPage = GlobalCategoryId;						
						tagParser.VersionName = versionName;
						tagParser.Legacy = leagcyFlag;
						tagParser.Active = activeFlag;
						tagParser.parse(row[i].ToString());						
						row[i] = tagParser.ReplacedHtml;
					}
				}
			}
			return data;
		}

		public static ListDictionary ConvertStringToDictionary(string stringToConvert, string rowSep,string keyDataSep)
		{
			ListDictionary dictionary = new ListDictionary();
			string[] dictionaryEntries;
			if (null != stringToConvert)
			{
				if (string.Empty != stringToConvert)
				{
					dictionaryEntries = stringToConvert.Split(rowSep.ToCharArray());
					for(int i=0; i<dictionaryEntries.Length; i++)
					{
						string[] keyData;
						keyData = dictionaryEntries[i].Split(keyDataSep.ToCharArray());
						if(String.Empty != keyData[0] && 2 == keyData.Length)
						{
							dictionary.Add(keyData[0],keyData[1]);
						}
					}
				}
			}
			return dictionary;
		}

		public static ListDictionary CompareAndCombineDictionary(ListDictionary currentDictionary,ListDictionary newDictionary)
		{
			ListDictionary returnDictionary = new ListDictionary();
			bool isAdded = false;
			foreach (DictionaryEntry newDictionaryEntry in newDictionary)
			{
				isAdded = false;
				foreach(DictionaryEntry currentDictionaryEntry in currentDictionary)
				{
					if (newDictionaryEntry.Key.ToString() == currentDictionaryEntry.Key.ToString())
					{
						returnDictionary.Add(newDictionaryEntry.Key,newDictionaryEntry.Value);
						isAdded = true;
					}
				}
				if(!isAdded)
				{
					returnDictionary.Add(newDictionaryEntry.Key,newDictionaryEntry.Value);
					isAdded = false;
				}
			}			
			foreach(DictionaryEntry currentDictionaryEntry in currentDictionary)
			{
				isAdded = false;
				foreach(DictionaryEntry returnDictionaryEntry in returnDictionary)
				{
					if (currentDictionaryEntry.Key.ToString() == returnDictionaryEntry.Key.ToString())
					{
						isAdded = true;
					}
				}
				if(!isAdded)
				{
					returnDictionary.Add(currentDictionaryEntry.Key,currentDictionaryEntry.Value);
					isAdded = false;
				}
			}
			return returnDictionary;
		}

        public static string IsValidQueryStringURL(string strToCheck)
        {
            if (!Uri.IsWellFormedUriString(strToCheck, UriKind.RelativeOrAbsolute))
                return strToCheck = string.Empty;
            else
            {
                return IsValidQueryString(strToCheck);
            }
        }

        public static string IsValidQueryString(string strToCheck)
        {
            string specialCharacters = @"!%$^&*()?/>.<,:;'\|}]{[_~`+=-" + "\"";
            char[] specialCharactersArray = specialCharacters.ToCharArray();
            if (!String.IsNullOrEmpty(strToCheck))
            {
                int index = strToCheck.IndexOfAny(specialCharactersArray);
                if (index != -1)
                    return strToCheck = string.Empty;
                else
                    return strToCheck;
            }
            else
                return strToCheck;
        }
        
        // Commented by Prao 2011017
//        public static string GetIntermediaFormatedKeyword(string keywords)
//        {

//            string keywordsCriteria = String.Empty;
//            string cleanKeywords = String.Empty;
//            string  formatedKeywords = String.Empty;
//            string returnString = String.Empty;
			
//            if (String.Empty != keywords) 
//            {
//                if("\"" == keywords.Substring(0,1).ToString())
//                {
//                    if("\"" == keywords.Substring(keywords.Length -1,1 ))
//                    {	
//                        if(2 < keywords.Length) 
//                        {
//                            //### 20061220
//                            keywords = ReplaceAllSpecialCharForSearch(keywords,Constants.INTERMEDIA_SPECIAL_CHARS);
//                            //### 20061220
////							cleanKeywords = "{" + keywords + "}";
////							returnString = "(" + cleanKeywords + " WITHIN link )*5, " + cleanKeywords;
////							return returnString;
//                            cleanKeywords = keywords;
//                            keywordsCriteria = keywordsCriteria + "{" + cleanKeywords + " WITHIN productname } * 9 ";
//                            keywordsCriteria = keywordsCriteria + ",{" + cleanKeywords + " WITHIN link_display_text } * 5 ";
//                            keywordsCriteria =  keywordsCriteria  + ",{" + cleanKeywords + "} * 2 " ;
//                            if(0 < cleanKeywords.IndexOf(" ")) 
//                            {
//                                keywordsCriteria =  keywordsCriteria + "," + cleanKeywords.Replace(" ",","); 
//                            }
//                            formatedKeywords = keywordsCriteria;
//                        }
//                        else
//                        {
//                            //return a error;
//                        }
//                    }
//                }
				
//                //### 20061220
//                //cleanKeywords = ReplaceAllSpecialCharForSearch(keywords,Constants.INTERMEDIA_SPECIAL_CHARS);
//                cleanKeywords = keywords;
//                //### 20061220
//                keywordsCriteria = keywordsCriteria + "{" + cleanKeywords + " WITHIN productname } * 9 ";
//                keywordsCriteria = keywordsCriteria + ",{" + cleanKeywords + " WITHIN link_display_text } * 5 ";
//                keywordsCriteria =  keywordsCriteria  + ",{" + cleanKeywords + "} * 2 " ;
//                if(0 < cleanKeywords.IndexOf(" ")) 
//                {
//                    keywordsCriteria =  keywordsCriteria + "," + cleanKeywords.Replace(" ",","); 
//                }
//                formatedKeywords = keywordsCriteria;
//            }
//            return formatedKeywords ;
//        }

        public static string GetIntermediaFormatedKeyword(string keywords)
        {

            string keywordsCriteria = String.Empty;
            string cleanKeywords = String.Empty;
            string formatedKeywords = String.Empty;
            string returnString = String.Empty;

            if (String.Empty != keywords)
            {
                if ("\"" == keywords.Substring(0, 1).ToString())
                {
                    if ("\"" == keywords.Substring(keywords.Length - 1, 1))
                    {
                        if (2 < keywords.Length)
                        {
                            //### 20061220
                            keywords = ReplaceAllSpecialCharForSearch(keywords, YamahaApp.Common.Constants.INTERMEDIA_SPECIAL_CHARS);
                            //### 20061220
                            cleanKeywords = keywords;
                            returnString = "{" + cleanKeywords + " WITHIN link_display_text }*5, {" + cleanKeywords + "} * 1";
                            //Comment this line if the code is taking a lot of time to execute
                            //returnString = returnString + "," + cleanKeywords + "% ";
                            returnString = returnString + "," + cleanKeywords + "%";
                            return returnString;
                        }
                        else
                        {
                            //return a error;
                        }
                    }
                }

                //### 20061220
                keywords = ReplaceAllSpecialCharForSearch(keywords, YamahaApp.Common.Constants.INTERMEDIA_SPECIAL_CHARS);
                cleanKeywords = keywords;
                //### 20061220
                keywordsCriteria = keywordsCriteria + "{" + cleanKeywords + " WITHIN link_display_text } * 5 ";
                keywordsCriteria = keywordsCriteria + ",{" + cleanKeywords + "} * 1 ";
                //Comment this line if the code is taking a lot of time to execute
                keywordsCriteria = keywordsCriteria + "," + cleanKeywords + "% ";
                if (0 < cleanKeywords.IndexOf(" "))
                {
                    cleanKeywords = cleanKeywords.Replace(" and ", ",");
                    cleanKeywords = cleanKeywords.Replace(" or ", ",");
                    keywordsCriteria = keywordsCriteria + "," + cleanKeywords.Replace(" ", ",");
                }
                formatedKeywords = keywordsCriteria;
            }
            return formatedKeywords;
        }

		//### 20061220
		public static string ReplaceAllSpecialCharForSearch(string inputString,string specialCharacterString)
		{
			string updatedString = String.Empty;
			string interimUpdatedString = String.Empty;
			char[] specialCharacterArray = specialCharacterString.ToCharArray();

			if(0 != inputString.Length)
			{
				updatedString = inputString;
				for(int i = 0 ; i < specialCharacterArray.Length ; i++)
				{
					if("'" == specialCharacterArray[i].ToString())
					{
						interimUpdatedString = updatedString.Replace("'","''");
					}
					else if ("\"" == specialCharacterArray[i].ToString())
					{
						interimUpdatedString = updatedString.Replace("\"","");
					}
					else
					{
						interimUpdatedString = updatedString.Replace(specialCharacterArray[i].ToString(),"\\" + specialCharacterArray[i].ToString());
					}
					updatedString = interimUpdatedString;
					interimUpdatedString = String.Empty;
				}
			}

			return updatedString;
		}
		//### 20061220
		public static string ConvertDictToString(ListDictionary dictionary)
		{
			string result = String.Empty;
			int count = 0;

			if(null != dictionary)
			{
				foreach(DictionaryEntry entry in dictionary)
				{
					if(0 == count)
					{
						result = result + entry.Key.ToString() + "=" + entry.Value.ToString();
					}
					else
					{
						result = result + "&" + entry.Key.ToString() + "=" + entry.Value.ToString();
					}
					count = count + 1;
				}
			}
			return result;
		}

		public static string GenerateLink (Int64 relatedContentId, Int64 relatedCategoryId, DataRow dataRow, string versionName, string activeFlag, string legacyFlag)
		{	
			string returnUrl = String.Empty;
			string relatedContentType,filePath,urlType;
			
			relatedContentType = dataRow["content_type"].ToString();

			if (Constants.DOCUMENT_CONTENT_TYPE == relatedContentType || Constants.PRODUCT_MEDIA_CONTENT_TYPE == relatedContentType || Constants.MULTIMEDIA_CONTENT_TYPE == relatedContentType || Constants.SERVICE_MANUAL_CONTENT_TYPE == relatedContentType)
			{
				filePath = dataRow["location"].ToString() + dataRow["file_name"].ToString();
				if(filePath != "")
				{
					returnUrl =  " href=\"#\" onClick=\"javascript:window.open('" + filePath + "','_blank',''); return false;\"";
				}
				else
				{
					returnUrl =  " href=\"#\" ";
				}
			}
			else if (Constants.IMAGE_CONTENT_TYPE == relatedContentType)
			{
				returnUrl =  " href=\"#\" OnClick=\"javascript:openEnlargedImageWnd('" + Utility.GenerateUrl(dataRow["template_application_type"].ToString(),Constants.DISPLAY_TYPE_INTERNAL,dataRow["template_url"].ToString(),"CNTID=" + relatedContentId.ToString() + "&VNM=" + versionName + "&AFLG=" + activeFlag,"","","","",false,"",0,"",versionName) + "'); return false;\"";
			}
			else if (Constants.URL_LINKS_CONTENT_TYPE == relatedContentType)
			{
				urlType = dataRow["url_type_value_string"].ToString();
				if(Constants.DISPLAY_TYPE_EXTERNAL == urlType)
				{
					returnUrl = " href=\"" + Utility.GenerateUrl(dataRow["template_application_type"].ToString(),urlType,dataRow["url"].ToString(),"",dataRow["query_string"].ToString(),"","","",false,"",0,"",versionName) +  "\" Target=\"_new\" ";
				}
				else
				{
					returnUrl = " href=\"" + Utility.GenerateUrl(dataRow["template_application_type"].ToString(),urlType,dataRow["url"].ToString(),"",dataRow["query_string"].ToString(),"","","",false,"",0,"",versionName) + "\" ";
				}
			}
			else
			{
				if(dataRow["template_url"].ToString() != "" && dataRow["template_url"].ToString() != "#")
				{
					//RK 20060606
					if ((Constants.PRODUCT_CONTENT_TYPE == relatedContentType) || (Constants.KNOWLEDGE_CONTENT_TYPE  == relatedContentType) || (Constants.TECHNOLOGY_CONTENT_TYPE  == relatedContentType))
					{
						returnUrl =  Utility.GenerateUrl(dataRow["template_application_type"].ToString(),Constants.DISPLAY_TYPE_INTERNAL,dataRow["template_url"].ToString(),"CNTID=" + relatedContentId.ToString() + "&CTID=" + relatedCategoryId + "&VNM=" + versionName + "&AFLG=" + activeFlag,"","","","",false,"",0,"",versionName);
					}
					//### PN 20060726 commented by paresh
					//KM 20060711
					else if(Constants.SERVICE_INFO_CONTENT_TYPE == relatedContentType)
					{
						returnUrl =  " href=\"#\" onClick=\"javascript:window.open('" + Utility.GenerateUrl(dataRow["template_application_type"].ToString(),Constants.DISPLAY_TYPE_INTERNAL,dataRow["template_url"].ToString(),"CNTID=" + relatedContentId.ToString() + "&CTID=" + relatedCategoryId + "&VNM=" + versionName + "&AFLG=" + activeFlag,"","","","",false,"",0,"",versionName) + "','_blank',''); return false;\"";
					}
					else
					{
						returnUrl = " href=\"" + Utility.GenerateUrl(dataRow["template_application_type"].ToString(),Constants.DISPLAY_TYPE_INTERNAL,dataRow["template_url"].ToString(),"CNTID=" + relatedContentId.ToString() + "&CTID=" + relatedCategoryId + "&VNM=" + versionName + "&AFLG=" + activeFlag,"","","","",false,"",0,"",versionName) + "\" ";
					}
					//RK 20060606
					if(relatedContentType == Constants.WRAPPER_CONTENT_TYPE || relatedContentType == Constants.IMAGE_CONTENT_TYPE)
					{
						returnUrl = returnUrl + " target=\"_new\" ";
					}
				}
				else
				{
					returnUrl =  " href=\"#\" ";
				}
			}
			return returnUrl;
		}
	
		public static string GetTitle(TemplateContext templateContext, string pageType)
		{
			string returnString = String.Empty;

			if(Constants.CATALOG_PAGE_TYPE == pageType)
			{
				if(null != templateContext.CategoryDetail)
				{
					//## RK 20071204 To display Page Title instead of Display_Text
					returnString = templateContext.CategoryDetail.Rows[0]["page_title"].ToString();
				}
			}
			else if (Constants.DETAIL_PAGE_TYPE == pageType)
			{
				if(Constants.MULTIMEDIA_CONTENT_TYPE  == templateContext.ContentDetail.Rows[0]["CONTENT_TYPE"].ToString())
				{
					returnString = templateContext.ContentDetail.Rows[0]["name"].ToString() + " " + templateContext.ContentDetail.Rows[0]["teaser"].ToString();
				}
				else
				{
					if(templateContext.ContentDetail.Rows[0]["page_title"] != null)
					{
						returnString = templateContext.ContentDetail.Rows[0]["page_title"].ToString();
					}
					else
					{
						returnString = templateContext.ContentDetail.Rows[0]["tracking_name"].ToString();
					}
				}
			} //## RK 20082801 To display title 
			else if (Constants.DETAIL_TITLE_PAGE_TYPE == pageType)
			{
				returnString = templateContext.ContentDetail.Rows[0]["link_display_text"].ToString();
			}
			return returnString;
		}


		#region CreateLoginCookies
		//public static void CreateLoginCookies(IDbConnection connection, string userId , YEC.Data.YECDataLib dataAccessManager, string userName, string userFirstName, string zipCode , HttpResponse obResponse, bool isPersistent )
//		public static void CreateLoginCookies(IDbConnection connection, string userId , DataLib dataAccessManager, string userName, string userFirstName, string zipCode, HttpResponse obResponse, bool isPersistent )
//		{
//			// CREATE AUTH COOKIE
//			// THIS WILL BE DECIDED AS PER THE PARAMETER isPersistent
//			FormsAuthentication.SetAuthCookie(userId,isPersistent);
//			
//			// USERNAME WILL ALWAYS BE NON-PERSISTENT COOKIE
//			HttpCookie cookie = FormsAuthentication.GetAuthCookie(userName,false);
//			cookie.Name = Constants.CDA_USER_COOKIE_NAME;
//			cookie.Value = userName;
//			obResponse.Cookies.Add(cookie);
//
//			// Create "CDA_UserFirstName_COOKIE_NAME" cookie
//			// THIS WILL BE PERSISTENT COOKIE
//			HttpCookie userFirstNameCookie = new HttpCookie(Constants.CDA_USER_FIRSTNAME_COOKIE_NAME,userFirstName); 
//			userFirstNameCookie.Path = "/";
//			userFirstNameCookie.Expires = DateTime.Now.AddDays(3000); 
//			obResponse.Cookies.Add(userFirstNameCookie);
//	
//			// Create HBX - Cookie // 
//			// THIS WILL BE PERSISTENT COOKIE
//			HttpCookie hbxCookieUserId = new HttpCookie(Constants.CDA_HBX_USER_ID,userId.ToString()); 
//			hbxCookieUserId.Path = "/";
//			hbxCookieUserId.Expires = DateTime.Today.AddDays(3000); 
//			obResponse.Cookies.Add(hbxCookieUserId);
//			
//			// NOW CREATE INITIAITVE COOKIE THIS SHOULD BE NON-PERSISTENT
//			DataTable initiativesForCurrentUser = dataAccessManager.GetInitiativesForCurrentUser(connection,userId);
//			string listOfInitiatives = string.Empty;
//			if(initiativesForCurrentUser.Rows.Count > 0)
//			{
//				foreach(DataRow dr in initiativesForCurrentUser.Select())
//				{
//					if(string.Empty == listOfInitiatives)
//					{
//						listOfInitiatives = dr["INITIATIVE_ALIAS"].ToString(); 
//					}
//					else
//					{
//						listOfInitiatives = listOfInitiatives + "~" + dr["INITIATIVE_ALIAS"].ToString();
//					}
//				}
//			}
//			HttpCookie initiativeCookie = new HttpCookie(Constants.CDA_INITIATIVES_COOKIE_NAME,listOfInitiatives); 
//			initiativeCookie.Path = "/";
//			obResponse.Cookies.Add(initiativeCookie);
//			
//			// user_zip cookie
//			// THIS WILL BE PERSISTENT COOKIE
//
//			HttpCookie zipCodeCookieForUser = new HttpCookie(Constants.CDA_USER_ZIP_CODE_COOKIE,zipCode ); 
//			zipCodeCookieForUser.Path = "/";
//			zipCodeCookieForUser.Expires = DateTime.Today.AddDays(3000); 
//			obResponse.Cookies.Add(zipCodeCookieForUser);
//		}

		#endregion


//		public static void SetRootLoginCookie (string userno, string roleList, HttpResponse response, HttpRequest request)
//		{
//			
//			if(string.Empty == roleList)
//			{
//				HttpCookie initiativeCookie  = request.Cookies[Constants.CDA_INITIATIVES_COOKIE_NAME]; 
//				if(null != initiativeCookie)
//				{
//					int indexFound = initiativeCookie.Value.IndexOf(Constants.ROLE_YEC);
//					if(indexFound >= 0 )
//					{
//						roleList = Constants.ROLE_YEC;
//					}
//				}
//			}
///*			Commented by AKD because after the URL change, this was causing problem in Login
//			HttpCookie oldCookie = request.Cookies[Constants.ROOT_COOKIE_NAME];
//			if (null != oldCookie)
//			{
//				if (null != oldCookie.Value && "" != oldCookie.Value)
//				{
//					FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(oldCookie.Value);
//					if (null != oldTicket)
//					{
//						string oldRoles = oldTicket.UserData;
//						if(oldRoles.IndexOf(roleList, 0) == -1)
//						{
//							roleList = oldRoles + "," + roleList;
//						}
//					}
//				}
//			}
//*/
//			if (string.Empty != roleList)
//			{
//				FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,userno, DateTime.Now, DateTime.Now.AddMinutes(30),false, roleList, FormsAuthentication.FormsCookiePath);
//				string hash = FormsAuthentication.Encrypt(ticket);
//				HttpCookie cookie = new HttpCookie(Constants.ROOT_COOKIE_NAME, hash);
//				if(ticket.IsPersistent)
//				{
//					cookie.Expires = ticket.Expiration;
//				}
//				response.Cookies.Add(cookie);
//			}
//		}

//		public static void RemoveRootLoginCookie (string roleList, HttpResponse response, HttpRequest request)
//		{
//
///*			
//			string userno = "0";
//
//			if(string.Empty == roleList)
//			{
//				HttpCookie initiativeCookie  = request.Cookies[Constants.CDA_INITIATIVES_COOKIE_NAME]; 
//				if(null != initiativeCookie)
//				{
//					int indexFound = initiativeCookie.Value.IndexOf(Constants.ROLE_YEC);
//					if(indexFound >= 0 )
//					{
//						roleList = Constants.ROLE_YEC;
//					}
//				}
//			}
//
//			HttpCookie oldCookie = request.Cookies[Constants.ROOT_COOKIE_NAME];
//			if (null != oldCookie)
//			{
//				if (null != oldCookie.Value && "" != oldCookie.Value)
//				{
//					FormsAuthenticationTicket oldTicket = FormsAuthentication.Decrypt(oldCookie.Value);
//					if (null != oldTicket)
//					{
//						userno = oldTicket.Name;
//						string oldRoles = oldTicket.UserData;
//						if(string.Empty != oldRoles)
//						{
//							if(oldRoles.IndexOf("," + roleList, 0) != -1)
//							{
//								roleList = oldRoles.Replace("," + roleList, "");
//							}
//							else if(oldRoles.IndexOf(roleList + ",", 0) != -1)
//							{
//								roleList = oldRoles.Replace(roleList + ",", "");
//							}
//							else if(oldRoles.IndexOf(roleList, 0) != -1)
//							{
//								roleList = oldRoles.Replace(roleList, "");
//							}
//						}
//					}
//				}
//			}
//
//			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userno, DateTime.Now, DateTime.Now.AddMinutes(30),false, roleList, FormsAuthentication.FormsCookiePath);
//			string hash = FormsAuthentication.Encrypt(ticket);
//			HttpCookie cookie = new HttpCookie(Constants.ROOT_COOKIE_NAME, hash);
//			if(ticket.IsPersistent)
//			{
//				cookie.Expires = ticket.Expiration;
//			}
//			response.Cookies.Add(cookie);
//*/
//			if (null != response.Cookies[Constants.ROOT_COOKIE_NAME] )
//			{
//				response.Cookies.Remove(Constants.ROOT_COOKIE_NAME);
//			}
//		}
		public static string ReplaceSpecialCharactersForTracking(string str)
		{
			string returnString;
			returnString = null;
			if (null != str)
			{
				returnString = str.Replace(" & "," and ");
				returnString = returnString.Replace("&"," and ");
				returnString = returnString.Replace("/"," ");
				returnString = returnString.Replace(" ","+");
				returnString = returnString.Replace("\"","");
				returnString = returnString.Replace("'","");
				returnString = returnString.Replace("%","");
				returnString = returnString.Replace("?","");
			}
			return returnString;
		}

		public static string ReplaceSpecialCharactersForMenuTracking(string str)
		{
			string returnString;
			returnString = null;
			if (null != str)
			{
				returnString = str.Replace(" & "," and ");
				returnString = returnString.Replace("&"," and ");
				//returnString = returnString.Replace("/"," ");
				returnString = returnString.Replace(" ","+");
				returnString = returnString.Replace("\"","");
				returnString = returnString.Replace("'","");
				returnString = returnString.Replace("%","");
				returnString = returnString.Replace("?","");
			}
			return returnString;
		}

		
		#region CheckContentOrCategoryVisibilty
		public static void CheckContentOrCategoryVisibilty(DataTable contentDetails , DataTable categoryDetails , string versionName )
		{
			string contentVisible = string.Empty ;
			string categoryVisible = string.Empty; 
			if(YCA.Common.YcaConstants.LIVE_VERSION_NAME ==  versionName)
			{
				if(null != contentDetails)
				{
                    if (contentDetails.Rows.Count > 0)
					{
						foreach(DataRow dr in contentDetails.Select() )
						{
							contentVisible = Convert.ToString(dr["VISIBLE_ON_THE_WEB"]);
						}
						if("N" == contentVisible)
						{
							RedirectToErrorPage(1,"Information not available.","","","","" );
						}
					}
				}
			}
			
			if(null != categoryDetails)
			{
				if(categoryDetails.Rows.Count > 0)
				{
					foreach(DataRow dr in categoryDetails.Select() )
					{
						if(YCA.Common.YcaConstants.LIVE_VERSION_NAME ==  versionName )
						{
							if("Y"==Convert.ToString(dr["ready_for_live_site"]) && "Y" == Convert.ToString(dr["VISIBLE"]) )
							{
								categoryVisible = "Y";	
							}
							else
							{
								categoryVisible = "N";	
							}
						}
						else
						{
							categoryVisible = Convert.ToString(dr["VISIBLE"]);
						}
					}
					if("N"==categoryVisible)
					{
						RedirectToErrorPage(1,"Information not available.","","","","" );
					}
				}
			}
		}
		#endregion

		#region AddQueryStringToURL
		public static string AddQueryStringToURL(string urlPath , string queryStringName , string queryStringValue )
		{
			string stringToReturn = string.Empty; 
			if(urlPath.IndexOf("?")>0)
			{
				stringToReturn = urlPath + "&" + queryStringName + "=" + queryStringValue; 
			}
			else
			{
				stringToReturn = urlPath + "?" + queryStringName + "=" + queryStringValue; 
			}
			return stringToReturn;
		}
		#endregion

		#region RedirectToErrorPage
		public static void RedirectToErrorPage(Int64 errNo, string arg1, string arg2, string arg3, string arg4, string arg5)
		{	
			string siteName = string.Empty ;
			string url = string.Empty; 
			siteName = HttpContext.Current.Request.Url.PathAndQuery.ToString();
			if(null != siteName && string.Empty  != siteName)
			{
				siteName= siteName.ToUpper(); 
			
				if(siteName.IndexOf("YEC") > 0 )
				{
					url = "/yec/common/yecerrorpage.aspx";
				}
				else if(siteName.IndexOf("YSISS") > 0 )
				{
					url = "/ysiss/common/ysisserrorpage.aspx";
				}
                else if (siteName.IndexOf("YCAS") > 0)
                {
                    url = "/ycas/common/ycaserrorpage.aspx";
                }
				else
				{
					url = "/yca/common/ycaerrorpage.aspx";
				}
				
				if( 0 !=  errNo )
				{
					url = AddQueryStringToURL(url,"errNo",Convert.ToString(errNo));
				}
				if(string.Empty !=  arg1 && null != arg1 )
				{
					url = AddQueryStringToURL(url,"arg1" ,arg1);
				}

				if(string.Empty !=  arg2 && null != arg2 )
				{
					url = AddQueryStringToURL(url,"arg2" ,arg2);
				}

				if(string.Empty !=  arg3 && null != arg3 )
				{
					url = AddQueryStringToURL(url,"arg3" ,arg3);
				}

				if(string.Empty !=  arg4 && null != arg4 )
				{
					url = AddQueryStringToURL(url,"arg4" ,arg4);
				}

				if(string.Empty !=  arg5 && null != arg5 )
				{
					url = AddQueryStringToURL(url,"arg5" ,arg5);
				}

				HttpContext.Current.Response.Redirect(url);
			}
		}
#endregion

		public static string StripHref(string sourceString)
		{
			if (null!=sourceString)
			{
				return sourceString.Substring(7,sourceString.Length-9);
			}
			return "";
		}

		#region GetArchiveText
		public static string GetArchiveText(string flagLegacy)
		{
			string returnString = string.Empty;
			switch(flagLegacy)
			{
				case "Y":
					returnString = "Show+Archive";
					break;
				case "N":
					returnString = "Archive+NotSelected";
					break;
			}
			return returnString;
		}
		#endregion

		#region GetSkinNameValue
		public static string GetSkinNameValue(IDbConnection connection , string skinNameCode , TemplateContext templateContext  )
		{
			string returnString = string.Empty;
			DataTable skinNameData =  templateContext.DataAccessManager.GetSysParamValues(connection,"SKIN_NAME" ,skinNameCode);
			if(null != skinNameData)
			{
				if(skinNameData.Rows.Count > 0)
				{
					returnString = skinNameData.Rows[0]["PARAMETER_VALUE"].ToString();
				}
			}
			return returnString;
		}
		#endregion

		#region GetSkinName
		public static string GetSkinName(IDbConnection connection, TemplateContext templateContext )
		{
			string skinNameCode = string.Empty; 
			string skinNameValue = string.Empty; 
			// Check for content id and get the skin name and its value 
			if(0 != templateContext.ContentId)
			{
				skinNameCode = templateContext.ContentDetail.Rows[0]["SKIN_NAME"].ToString();
				if(string.Empty !=  skinNameCode)
				{
					skinNameValue = Utility.GetSkinNameValue(connection,skinNameCode, templateContext);
				}
			}
			// Check for category details if skinNameValue is still not set
			if(string.Empty == skinNameValue)
			{
				if(0!= templateContext.CategoryId)
				{
					skinNameCode = templateContext.CategoryDetail.Rows[0]["SKIN_NAME"].ToString();
					if(string.Empty !=  skinNameCode)
					{
						skinNameValue = Utility.GetSkinNameValue(connection,skinNameCode, templateContext);
					}
				}
			}

			// if skinNamevalue is stil null or empty set the default skin name
			if( string.Empty == skinNameValue)
			{
				skinNameValue = Constants.DEFAULT_SKIN_NAME;
			}
			return skinNameValue;
		}
		#endregion

		#region IsValidEmailAddress
		public static bool IsValidEmailAddress(string email)
		{
			string regExpression = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
			if(System.Text.RegularExpressions.Regex.IsMatch(email,regExpression))
				return true;
			else
				return false;
		}
		#endregion	

		#region Base64Decode
		public string Base64Decode(string data)
		{
			try
			{
				System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding(); 
				System.Text.Decoder utf8Decode = encoder.GetDecoder();

				byte[] todecode_byte = Convert.FromBase64String(data);
				int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length); 
				char[] decoded_char = new char[charCount];
				utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0); 
				string result = new String(decoded_char);
				return result;
			}
			catch(Exception e)
			{
				throw new Exception("Error in base64Decode" + e.Message);
			}
		}
		#endregion

		#region Base64Encode
		public string Base64Encode(string data)
		{
			try
			{
				byte[] encData_byte = new byte[data.Length];
				encData_byte = System.Text.Encoding.UTF8.GetBytes(data); 
				string encodedData = Convert.ToBase64String(encData_byte);
				return encodedData;
			}
			catch(Exception e)
			{
				throw new Exception("Error in base64Encode" + e.Message);
			}
		}
		#endregion

		#region FindControlRecursive
		public static System.Web.UI.Control FindControlRecursive(System.Web.UI.WebControls.WebControl  Root, string Id) 
		{
			if (Root.ID == Id) 
			{
				return Root; 
			}
			foreach (System.Web.UI.Control  Ctl in Root.Controls) 
			{
				System.Web.UI.Control FoundCtl = FindControlRecursive(Ctl, Id); 
				if (FoundCtl != null) 
				{
					return FoundCtl; 
				}
			}
			return null; 
		}
		#endregion 

		#region FindControlRecursive - OVERLOADED
		public static System.Web.UI.Control FindControlRecursive(System.Web.UI.Control Root, string Id) 
		{
			if (Root.ID == Id) 
			{
				return Root; 
			}
			foreach (System.Web.UI.Control  Ctl in Root.Controls) 
			{
				System.Web.UI.Control FoundCtl = FindControlRecursive(Ctl, Id); 
				if (FoundCtl != null) 
				{
					return FoundCtl; 
				}
			}
			return null; 
		}
		#endregion 

		#region GetPageTitleAsPerRegistrationMode
		public static string GetPageTitleAsPerRegistrationMode(string mode, bool isUserExistsInCurrentInitiativeType)
		{	
			string returnValue = string.Empty;		
			switch(mode.ToLower())
			{
				case "prelogin" :
					returnValue = "Sign Up For Your Online Yamaha My Account";
					break;
				case "login" :
					returnValue = "Online Yamaha My Account Sign In";
					break;
				case "register" :
					returnValue = "Sign Up For Your Online Yamaha My Account";
					break;
				case "edit" :
					if(isUserExistsInCurrentInitiativeType)
					{
						returnValue = "Update Your Yamaha My Account Information";
					}
					else
					{
						returnValue = "Sign Up For Your Online Yamaha My Account";
					}
					break;
				case "editprofile" :
					returnValue = "Update Your Yamaha My Account Information";
					break;
				case "forgotpassword" :
					returnValue = "Online My Forgot Password";
					break;
				case "passwordsent" :
					returnValue = "Online Yamaha My Account Forgot Password";
					break;
			}
			return returnValue;
		}	
		#endregion

		#region GetTrackingPageNameAsPerRegistrationMode
		public static string GetTrackingPageNameAsPerRegistrationMode(string mode,bool isUserExistsInCurrentInitiativeType)
		{	
			string returnValue = string.Empty;		
			switch(mode.ToLower())
			{
				case "prelogin" :
					returnValue = "Yamaha My Account Pre Sign In";
					break;
				case "login" :
                    returnValue = "Yamaha My Account Sign In";
					break;
				case "register" :
                    returnValue = "Yamaha My Account Sign Up - New";
					break;
				case "edit" :
					if(isUserExistsInCurrentInitiativeType)
					{
                        returnValue = "Update Your Yamaha My Account Information";
					}
					else
					{
                        returnValue = "Yamaha My Account Sign Up - Add YCA";
					}
					break;
				case "editprofile" :
                    returnValue = "Yamaha My Account Sign Up - Already YCA";
					break;
				case "forgotpassword" :
                    returnValue = "Yamaha My Account Forgot Password Question";
					break;
				case "passwordsent" :
                    returnValue = "Yamaha My Account Forgot Password Answer";
					break;
			}
			return returnValue;
		}	
		#endregion


		#region "Yamaha V3"
		public static ListDictionary LoadContentTypeColumnMapping(IDbConnection connection)
		{
			Object cacheItem = HttpContext.Current.Cache["contenttypecolumns"] as ListDictionary;
			if(cacheItem == null) 
			{
				Object lockObject = new Object();
				lock(lockObject)
				{
					if (cacheItem == null)
					{
						cacheItem = new Data.DataLib().GetV3ContentColumnMapping(connection);
						if (cacheItem != null)
						{
							HttpContext.Current.Cache.Insert("contenttypecolumns", cacheItem);
						}
					}
				}
			}
			return (ListDictionary)cacheItem;
		}

		public static string GetContentTypeColumn(ListDictionary mappingInfo, String contentType, String columnName)
		{			
			if (null != mappingInfo)
			{
				if (mappingInfo.Contains(contentType.ToUpper()+"_"+columnName.ToUpper()))
				{
					return mappingInfo[contentType.ToUpper()+"_"+columnName.ToUpper()].ToString();
				}
				else
				{
					return columnName;
				}
			}
			else
			{
				return columnName;
			}
		}

		#endregion


        public static DataTable GetEmailTemplateInfoV2(IDbConnection connection, string initiative)
        {
            DataTable emailTemplateInfo = new DataTable();
            try
            {
                emailTemplateInfo = DataLib.GetEmailTemplateDetailsFromInitiativeV2(connection, initiative);
            }
            catch (Exception)
            { }
            return emailTemplateInfo;
        }
	}
}
