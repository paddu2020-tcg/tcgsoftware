using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text; 

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for Constants.
	/// </summary>
	public class Constants
	{
        public static ListDictionary MessageList = new ListDictionary();
		public Constants()
		{
		}
		static Constants()
		{
//			StringBuilder sendForgotPasswordMail = new StringBuilder ();
//			sendForgotPasswordMail.Append("<html>");
//			sendForgotPasswordMail.Append("<head>");
//			sendForgotPasswordMail.Append("<style>");
//			sendForgotPasswordMail.Append("body{");
//			sendForgotPasswordMail.Append("padding:10px;");
//			sendForgotPasswordMail.Append("margin:0;");
//			sendForgotPasswordMail.Append("font-family:Verdana, Arial, Helvetica, sans-serif;");
//			sendForgotPasswordMail.Append("font-size:small;");
//			sendForgotPasswordMail.Append("color:#333333;");
//			sendForgotPasswordMail.Append("}");
//			sendForgotPasswordMail.Append("</style>");
//			sendForgotPasswordMail.Append("</head>");
//
//			sendForgotPasswordMail.Append("<body>");
//
//			sendForgotPasswordMail.Append("%user,");
//			sendForgotPasswordMail.Append("<br>");
//			sendForgotPasswordMail.Append("Your requested password is:<br /><br />");
//			sendForgotPasswordMail.Append("%password.<br/><br /> You may now login at <a href=\"http://www.yamaha.com/yca/registration.html?regmode=login\">http://www.yamaha.com/yca/registration.html</a>");
//			sendForgotPasswordMail.Append("<br />");
//			sendForgotPasswordMail.Append("Sincerely,<br />Yamaha Corp Inc.");
//			sendForgotPasswordMail.Append("</body></html>");
//
//			Messages.Add(FORGOT_PASSWORD_MAIL, sendForgotPasswordMail.ToString());



            #region YASI_PROMO_REG

            StringBuilder yasiForgotPassword = new StringBuilder();
            yasiForgotPassword.Append("<!DOCTYPE html PUBLIC \"//W3C//DTD XHTML 1.0 Transitional//EN\\\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            yasiForgotPassword.Append("<html>");
            yasiForgotPassword.Append("<head>");
            yasiForgotPassword.Append("<title>YASI - Forgot Password</title>");
            yasiForgotPassword.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">");
            yasiForgotPassword.Append("</head>");
            yasiForgotPassword.Append(" ");
            yasiForgotPassword.Append("<body bgcolor=\"#ffffff\" leftmargin=\"0\" topmargin=\"0\" marginwidth=\"0\" marginheight=\"0\">");
            yasiForgotPassword.Append("<table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");
            //yasiForgotPassword.Append("  <tr>");
            //yasiForgotPassword.Append("     <td colspan=\"3\"><div align=\"center\"><font color=\"#111111\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\">This e-mail was sent to you by Yamaha Corporation of America. <a href=\"http://www.yamaha.com/yasinew/forgotpassword.asp?fname=%firstname\" target=\"_blank\">Click to view this message in a browser</a><br /> ");
            //yasiForgotPassword.Append("     </font></div></td> ");
            //yasiForgotPassword.Append("   </tr> ");
            yasiForgotPassword.Append("  <tr>");
            yasiForgotPassword.Append("    <td colspan=\"3\"><a href=\"http://www.yamaha.com/yasi/home.html\" target=\"_blank\"><img src=\"http://www.yamaha.com/yamahavgn/cda/images/registration/yasi/email_header.jpg\" alt=\"Yamaha Artist Services\" width=\"600\" height=\"275\" border=\"0\" /></a>");
            yasiForgotPassword.Append("    </td>");
            yasiForgotPassword.Append("  </tr>");
            yasiForgotPassword.Append("  <tr>");
            yasiForgotPassword.Append("    <td width=\"50\" rowspan=\"2\"><img src=\"http://www.yamaha.com/yamahavgn/cda/images/blank.gif\" width=\"50\" height=\"245\" alt=\"\" /></td>");
            yasiForgotPassword.Append("    <td colspan=\"2\"><p>&nbsp;</p>    </td>");
            yasiForgotPassword.Append("  </tr>");
            yasiForgotPassword.Append("  <tr>");
            yasiForgotPassword.Append("    <td width=\"550\" colspan=\"2\">");
            yasiForgotPassword.Append("    <div style=\"width:535px;line-height:18px;\">");
            yasiForgotPassword.Append("    <p align=\"left\"><font size=\"2\" face=\"Verdana, Arial, Helvetica, sans-serif\">Hello %firstname,</font></p>");
            yasiForgotPassword.Append("      <p align=\"left\"><font size=\"2\" face=\"Verdana, Arial, Helvetica, sans-serif\">You are receiving this email in response to your Forgot Password request on the %websitename web site.<br>");
            yasiForgotPassword.Append("        <br>");
            yasiForgotPassword.Append("      Your Online EasyPass password is: <b>%password</b></font></p>");
            yasiForgotPassword.Append("      <p align=\"left\"><font size=\"2\" face=\"Verdana, Arial, Helvetica, sans-serif\">Another advantage of your Online EasyPass is that you can easily and securely update your personal information or change your password any time. To access your EasyPass account, simply Sign In at any one of the participating EasyPass sites and click on the My EasyPass link. </p>");                        
            yasiForgotPassword.Append("      <p align=\"left\"><font size=\"2\" face=\"Verdana, Arial, Helvetica, sans-serif\">With your Online EasyPass, you are in total control over how Yamaha communicates with you and, because we respect your privacy, you can be assured that any and all personal information will only be used in accordance with our Privacy Policy. <a href=\"http://usa.yamaha.com/privacy_policy\" style=\"color:#7f0416;\">Please click here to review the entire Privacy Policy</a>.</p>");            
            yasiForgotPassword.Append("                <p>Thank you!</p>");
            yasiForgotPassword.Append("                <p>%yamahaentityname</p>");
            yasiForgotPassword.Append("");
            yasiForgotPassword.Append("  <tr>");
            yasiForgotPassword.Append("    <td><p>&nbsp;</p></td>");
            yasiForgotPassword.Append("    <td width=\"550\" colspan=\"2\">&nbsp;</td>");
            yasiForgotPassword.Append("  </tr>");
            yasiForgotPassword.Append("  ");
            yasiForgotPassword.Append(" ");
            yasiForgotPassword.Append("  <tr>");
            yasiForgotPassword.Append("    <td colspan=\"3\"><p><a href=\"http://www.yamaha.com/thehub\" target=\"_blank\"><img src=\"http://www.yamaha.com/pacsupport/images/email_badge_hub.jpg\" alt=\"The Hub\" width=\"200\" height=\"80\" border=\"0\"></a><a href=\"http://www.facebook.com/yasinewyork\" target=\"_blank\"><img src=\"http://www.yamaha.com/pacsupport/images/email_badge_facebook.jpg\" alt=\"Facebook\" width=\"200\" height=\"80\" border=\"0\"></a><a href=\"http://www.twitter.com/YASINewYork\" target=\"_blank\"><img src=\"http://www.yamaha.com/pacsupport/images/email_badge_twitter.jpg\" alt=\"Twitter\" width=\"200\" height=\"80\" border=\"0\"></a><br>");
            yasiForgotPassword.Append("        <a href=\"http://www.yamaha.com/usa\" target=\"_blank\"><img src=\"http://www.yamaha.com/pacsupport/images/corp_footer.jpg\" width=\"600\" height=\"42\" border=\"0\" alt=\"Yamaha\" /></a></p>");
            yasiForgotPassword.Append("    </td>");
            yasiForgotPassword.Append("  </tr>");
            yasiForgotPassword.Append("</table>");
            yasiForgotPassword.Append("");
            yasiForgotPassword.Append("<br>");
            yasiForgotPassword.Append("<br style=\"clear: both;\">");
            yasiForgotPassword.Append(" ");
            yasiForgotPassword.Append(" ");
            yasiForgotPassword.Append(" ");
            yasiForgotPassword.Append("<style>@media print{#_t { background-image: url(\"https://b3.emltrk.com/6D34BDBD?p&d={EMAIL_ADDRESS}\");}} div.OutlookMessageHeader {background-image:url(\"https://b3.emltrk.com/6D34BDBD?f&d={EMAIL_ADDRESS}\")} table.moz-email-headers-table {background-image:url(\"https://b3.emltrk.com/6D34BDBD?f&d={EMAIL_ADDRESS}\")} blockquote #_t {background-image:url(\"https://b3.emltrk.com/6D34BDBD?f&d={EMAIL_ADDRESS}\")} #MailContainerBody #_t {background-image:url(\"https://b3.emltrk.com/6D34BDBD?f&d={EMAIL_ADDRESS}\")}</style><div id=\"_t\"></div><img src=\"https://b3.emltrk.com/6D34BDBD?d={EMAIL_ADDRESS}\" width=\"1\" height=\"1\" border=\"0\" />");
            yasiForgotPassword.Append("</body>");
            yasiForgotPassword.Append("</html>");
            yasiForgotPassword.Append(" ");
            yasiForgotPassword.Append(" ");
            yasiForgotPassword.Append("");
            yasiForgotPassword.Append("");
            yasiForgotPassword.Append("");
            MessageList.Add(YASI_PROMO_REG_FORGOT_PASS_MAIL, yasiForgotPassword.ToString());


            StringBuilder yasiWelcome = new StringBuilder();
            yasiWelcome.Append("<!DOCTYPE html PUBLIC \"//W3C//DTD XHTML 1.0 Transitional//EN\\\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\n");
            yasiWelcome.Append("<html>");
            yasiWelcome.Append("<head>");
            yasiWelcome.Append("<title>YASI - WELCOME</title>");
            yasiWelcome.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">");
            yasiWelcome.Append("</head>");
            yasiWelcome.Append(" ");
            yasiWelcome.Append("<body bgcolor=\"#ffffff\" leftmargin=\"0\" topmargin=\"0\" marginwidth=\"0\" marginheight=\"0\">");
            yasiWelcome.Append("<table width=\"600\" border=\"0\" align=\"center\" cellpadding=\"0\" cellspacing=\"0\">");
            yasiWelcome.Append("  <tr>");
            yasiWelcome.Append("     <td colspan=\"3\"><div align=\"center\"><font color=\"#111111\" size=\"1\" face=\"Verdana, Arial, Helvetica, sans-serif\">This e-mail was sent to you by Yamaha Corporation of America. <a href=\"http://www.yamaha.com/emails/yasi.asp?fname=%firstname\" target=\"_blank\">Click to view this message in a browser</a><br /> ");            
            yasiWelcome.Append("   </tr> ");
            yasiWelcome.Append("   <tr> ");
            yasiWelcome.Append("    <td colspan=\"3\"><a href=\"http://www.yamaha.com/yasi/home.html\" target=\"_blank\"><img src=\"http://www.yamaha.com/yamahavgn/cda/images/registration/yasi/email_header.jpg\" alt=\"Yamaha Artist Services\" width=\"600\" height=\"275\" border=\"0\" /></a>");
            yasiWelcome.Append("    </td>");
            yasiWelcome.Append("  </tr>");
            yasiWelcome.Append("  <tr>");
            yasiWelcome.Append("    <td width=\"50\" rowspan=\"2\"><img src=\"http://www.yamaha.com/yamahavgn/cda/images/blank.gif\" width=\"50\" height=\"245\" alt=\"\" /></td>");
            yasiWelcome.Append("    <td colspan=\"2\"><p>&nbsp;</p>    </td>");
            yasiWelcome.Append("  </tr>");
            yasiWelcome.Append("  <tr>");
            yasiWelcome.Append("    <td width=\"550\" colspan=\"2\">");
            yasiWelcome.Append("    <div style=\"width:535px;line-height:18px;\">");
            yasiWelcome.Append("    <p align=\"left\"><font size=\"2\" face=\"Verdana, Arial, Helvetica, sans-serif\">Hi %firstname,</font></p>");
            yasiWelcome.Append("      <p align=\"left\"><font size=\"2\" face=\"Verdana, Arial, Helvetica, sans-serif\">By signing up for Yamaha&#039;s Online EasyPass, you'll be added to the Yamaha Artist Services New York mailing list where you can be notified of the many Yamaha Artists events.  You will also be able to enjoy the many advantages listed below.<br>");
            yasiWelcome.Append("        <table cellpadding=\"0\" cellspacing=\"0\" width=\"90%\" align=\"center\" style=\"font-family:Arial;font-size:10pt;color:#4a4a4a;\">");
            yasiWelcome.Append("          <tr>");
            yasiWelcome.Append("           <td valign=\"top\" width=\"50%\"><ul style=\"text-align:left;margin-top:6px;margin-bottom:6px;\">");
            yasiWelcome.Append("              <li>Access to members-only content on select Yamaha websites </li>");
            yasiWelcome.Append("              <li>Participate in members-only promotions and entry into special sweepstakes and contests </li>");
            yasiWelcome.Append("              <li>Access product manuals and updates online</li>");
            yasiWelcome.Append("              <li>Learn about new products related to the products you already own</li>");
            yasiWelcome.Append("              <li>Register Yamaha products quickly and easily</li>");
            yasiWelcome.Append("              <li>Provide the Yamahaproduct development team your thoughts about our current and future products</li>");
            yasiWelcome.Append("              <li>Receive up-to-date news from the wide world of Yamaha</li>");
            yasiWelcome.Append("          </ul></td>");
            yasiWelcome.Append("          </tr>");
            yasiWelcome.Append("       </table>");
            yasiWelcome.Append("  <div style=\"font-family:Arial;font-size:10pt;color:#4a4a4a;\">");
            yasiWelcome.Append("  <p>Thank you for connecting with Yamaha and creating your Yamaha Online EasyPass. </p>");
            yasiWelcome.Append(" <p>%yamahaentityname</p>");
            yasiWelcome.Append("      </div></td>");
            yasiWelcome.Append("  </tr>");
            yasiWelcome.Append("");
            yasiWelcome.Append("  <tr>");
            yasiWelcome.Append("    <td><p>&nbsp;</p></td>");
            yasiWelcome.Append("    <td width=\"550\" colspan=\"2\">&nbsp;</td>");
            yasiWelcome.Append("  </tr>");
            yasiWelcome.Append("  ");
            yasiWelcome.Append(" ");
            yasiWelcome.Append("  <tr>");
            yasiWelcome.Append("    <td colspan=\"3\"><p><a href=\"http://www.yamaha.com/thehub\" target=\"_blank\"><img src=\"http://www.yamaha.com/pacsupport/images/email_badge_hub.jpg\" alt=\"The Hub\" width=\"200\" height=\"80\" border=\"0\"></a><a href=\"http://www.facebook.com/yasinewyork\" target=\"_blank\"><img src=\"http://www.yamaha.com/pacsupport/images/email_badge_facebook.jpg\" alt=\"Facebook\" width=\"200\" height=\"80\" border=\"0\"></a><a href=\"http://www.twitter.com/YASINewYork\" target=\"_blank\"><img src=\"http://www.yamaha.com/pacsupport/images/email_badge_twitter.jpg\" alt=\"Twitter\" width=\"200\" height=\"80\" border=\"0\"></a><br>");
            yasiWelcome.Append("        <a href=\"http://www.yamaha.com/usa\" target=\"_blank\"><img src=\"http://www.yamaha.com/pacsupport/images/corp_footer.jpg\" width=\"600\" height=\"42\" border=\"0\" alt=\"Yamaha\" /></a></p>");
            yasiWelcome.Append("    </td>");
            yasiWelcome.Append("  </tr>");
            yasiWelcome.Append("</table>");
            yasiWelcome.Append("");
            yasiWelcome.Append("<br>");
            yasiWelcome.Append("<br style=\"clear: both;\">");
            yasiWelcome.Append(" ");
            yasiWelcome.Append(" ");
            yasiWelcome.Append(" ");
            yasiWelcome.Append("<style>@media print{#_t { background-image: url(\"https://b3.emltrk.com/6D34BDBD?p&d={EMAIL_ADDRESS}\");}} div.OutlookMessageHeader {background-image:url(\"https://b3.emltrk.com/6D34BDBD?f&d={EMAIL_ADDRESS}\")} table.moz-email-headers-table {background-image:url(\"https://b3.emltrk.com/6D34BDBD?f&d={EMAIL_ADDRESS}\")} blockquote #_t {background-image:url(\"https://b3.emltrk.com/6D34BDBD?f&d={EMAIL_ADDRESS}\")} #MailContainerBody #_t {background-image:url(\"https://b3.emltrk.com/6D34BDBD?f&d={EMAIL_ADDRESS}\")}</style><div id=\"_t\"></div><img src=\"https://b3.emltrk.com/6D34BDBD?d={EMAIL_ADDRESS}\" width=\"1\" height=\"1\" border=\"0\" />");
            yasiWelcome.Append("</body>");
            yasiWelcome.Append("</html>");
            MessageList.Add(YASI_PROMO_REG_WELCOME_MAIL, yasiWelcome.ToString());
            #endregion
		}


		#region Constants

		//### PN
		public const string TECHNOLOGY_CONTENT_TYPE = "TECHNOLOGY";
		public const Int64 RLTN_TYPE_ID_FOR_TECHNOLOGY = 1503;
		public const Int64 RLTN_TYPE_ID_FOR_TECHNOLOGY_TO_FAQ = 2387;
		//### PN
		public const Int64 RLTN_TYPE_ID_MEDIA_CLIPS = 100;

		public const Int64 RLTN_TYPE_ID_FOR_PANEL_VIEW = 1504;

		//public const Int64 RLTN_TYPE_ID_BANNER_IMAGES = 995;
		public const Int64 RLTN_TYPE_ID_URL_LINKS = 195;
		public const Int64 RLTN_TYPE_ID_BANNER_WITH_MULTIMEIDA = 2367;
		public const Int64 RLTN_TYPE_ID_SPOTLIGHT_IMAGE = 65;
		public const Int64 RLTN_TYPE_ID_THUMBNAIL_IMAGE = 70;

		public const string CDS_TYPE_DEV = "DEV";
		public const string CDS_TYPE_LIVE = "LIVE";

		public const Int64 CONTENT_RLN_TYPE_TITLE_IMG = 50;
        
		public const string RELATION_TYPE = "RELATION";
		public const string ATTRIBUTE_TYPE = "ATTRIBUTE";

		public const string CACHE_FILE_PATH = "/yamahavgn/cachefiles";
		public const string TEMPLATE_TYPE_NORMAL = "NORMAL";
		public const string TEMPLATE_TYPE_VIGNETTE = "VIGNETTE";
		public const string TEMPLATE_TYPE_REDIRECT = "REDIRECT";
		
		public const string DISPLAY_TYPE_INTERNAL	= "INT";
		public const string DISPLAY_TYPE_EXTERNAL	= "EXT";

		/*
		public const string HTTP_STRING = "http://" ;
		public const string HTTPS_STRING = "https://" ;

		public const string g_sServerHost = "http://203.193.183.25";
		public const string g_sHost = "203.193.183.25";
		*/

		public const string DEFAULT_LANGUAGE_CODE = "EN";
		public const string DEFAULT_ACTIVE_FLAG = "Y";
		public const string DEFAULT_LEGACY_FLAG = "N";
		public const string DEFAULT_HISTORY_FLAG = "N";
		public const string DEFAULT_SKIN_NAME = "/yamahavgn/CDA/Skins/Corporate_skin.jpg";
		
		public const int DEFAULT_WIN_WIDTH  =  300;
		public const int DEFAULT_WIN_HEIGHT =  500;
		public const int DEFAULT_GLOSSARY_WIN_HEIGHT = 300;
		public const int DEFAULT_GLOSSARY_WIN_WIDTH = 300;
			
		public const string LEAF_CAT_TYPE = "LEAF";
		public const string NODE_CAT_TYPE = "NODE";
		public const string MENU_CAT_TYPE = "MENU";
		public const string CONTENT_CAT_TYPE = "CONTENT";
		public const string ADMIN_CAT_TYPE = "ADMIN";


		public const string CATALOG_PAGE_TYPE = "CATALOG";
		public const string DETAIL_PAGE_TYPE = "DETAIL";
		public const string DETAIL_TITLE_PAGE_TYPE = "DETAIL_TITLE";

		public const string WORKING_VERSION_NAME = "WORKING";
		public const string PREVIOUS_VERSION_NAME = "PREVIOUS";
		public const string LIVE_VERSION_NAME = "LIVE";
		public const string DEFAULT_VERSION_NAME = "LIVE";
		
		public const string ACTIVE = "Y";
		public const string LEGACY = "N";

        public const string BROADCAST_CONTENT_TYPE = "BROADCAST";
        public const string TESTIMONIAL_CONTENT_TYPE = "TESTIMONIAL";
		public const string PRODUCT_CONTENT_TYPE = "PRODUCT";
		public const string ARTIST_CONTENT_TYPE = "ARTIST";
		public const string SPOTLIGHT_CONTENT_TYPE = "SPOTLIGHT";
		public const string IMAGE_CONTENT_TYPE = "IMAGE";
		public const string DOCUMENT_CONTENT_TYPE = "DOCUMENT";
		public const string LIFESTYLE_CONTENT_TYPE = "LIFESTYLE";
		public const string FILE_SUB_CONTENT_TYPE = "FILE";
		public const string MULTIMEDIA_CONTENT_TYPE = "MULTIMEDIA";
		public const string PRODUCT_MEDIA_CONTENT_TYPE = "PRODUCT_MEDIA";
		public const string EVENTS_CONTENT_TYPE = "EVENT";
		public const string NEWS_CONTENT_TYPE = "NEWS";
		public const string KNOWLEDGE_CONTENT_TYPE = "KNOWLEDGE_ITEM";
		public const string GLOSSARY_CONTENT_TYPE = "GLOSSARY";
		public const string GENERIC_CONTENT_TYPE = "GENERAL";
		public const string URL_LINKS_CONTENT_TYPE = "URL_LINK";
		public const string WRAPPER_CONTENT_TYPE = "WRAPPER";
		public const string OVERVIEW_CONTENT_TYPE = "OVERVIEW";
		public const string POPTEXT_CONTENT_TYPE = "POPTEXT";
		public const string COLLECTION_CONTENT_TYPE = "COLLECTION";
		public const string PLACEMENT_HORIZONTAL_SPOTLIGHT = "HORIZONTAL_SPOTLIGHT";
		public const string PLACEMENT_GROUP_ON_LEFT_NO_TITLE	= "GROUP_ON_LEFT_NO_TITLE";
		public const string PLACEMENT_MEDIA_CONTROL	= "MEDIA_CONTROL";
		public const string ARTIST_NEWS_CONTENT_TYPE = "ARTIST_NEWS";
		//public const string BANNER_CONTENT_TYPE  = "BANNER";
		public const string DETAIL_SECTION_CONTENT_TYPE = "DETAIL_SECTION";
		public const string CHANNEL_SECTION_CONTENT_TYPE = "CHANNEL_SECTION";
		public const string SKU_CONTENT_TYPE			= "SKU";
        public const string MEDIA_CONTENT_TYPE = "MEDIA";
		public const string TEXT_INCLUDE_CONTENT_TYPE			= "TEXT_INCLUDE";

		public const string PLACEMENT_SPOTS_ON_LEFT_NO_TITLE = "SPOTS_ON_LEFT_NO_TITLE";
		public const string PLACEMENT_HORIZONTAL_BAR = "HORIZONTAL_BAR";
		public const string PLACEMENT_SPOTS_ON_LEFT = "SPOTS_ON_LEFT";
		public const string PLACEMENT_FEATURED_LINK = "FEATURED_LINK"; 
		public const string PLACEMENT_BANNER_LEFT_W_TEXT = "BANNER_LEFT_W_TEXT"; 
		public const string PLACEMENT_BANNER_LEFT_NO_TEXT = "BANNER_LEFT_NO_TEXT"; 
		public const string PLACEMENT_SECTION_ON_LEFT = "SECTION_ON_LEFT";

		public const string OWNERSHIP_CONTENT_CAT_RLTN_TYPE = "OWNERSHIP";

		public const Int64 CHANNEL_FOR_DUMMY_MODELS	= 550976;
		public const string CONTENT_TYPE_EXCLUDED_LIST  = "'SPOTLIGHT','LIFESTYLE','OVERVIEW','POPTEXT','IMAGE','TEXT_INCLUDE','IPLAY_STORY','WORSHIP_ARTICLE','WORSHIP_EVENT','WORSHIP_NEWS','WORSHIP_PROFILE','WORSHIP_TROOM'";
		
		public const string CONTENT_TYPE_WORSHIP_LIST = "'WORSHIP_ARTICLE','WORSHIP_EVENT','WORSHIP_NEWS','WORSHIP_PROFILE','WORSHIP_TROOM'";

		public const string URL_FOR_CACHING	= "UrlForCaching";
		public const string ORIGINAL_URL	= "OriginalUrl";
		public const string BROWSER_PATH	= "BrowserPath";
		
		#endregion

		#region Ysiss constants

		public const string SERVICE_MANUAL_CONTENT_TYPE = "SERVICE_MANUAL";
		public const string FIELD_TIP_CONTENT_TYPE = "FIELD_TIP";
		public const string SERVICE_INFO_CONTENT_TYPE = "SERVICE_INFO";
		public const string SERVICE_PRODUCT_CONTENT_TYPE = "SERVICE_PRODUCT";

		#endregion

        public const string YCA_KEYWORD_CONSTANT = "";

		public const string ROOT_COOKIE_NAME = "root";
		public const string YSISS_COOKIE_NAME = "ysiss";
		public const string YEC_COOKIE_NAME = "yec";
        public const string GLOBAL_PWD_RESET_MAIL = "GLOBAL_PWD_RESET_MAIL";
        //### 20100407
        //for ecomm
        public const string ECOMM_CART_ID = "ECOMM_CART_ID";
        public const string SHOPTRN_CART_ID = "SHOPTRN_CART_ID";
                                                                 
		public const string ROLE_4WRDIT = "4WD";
		public const string ROLE_4WRDIT_ADMIN = "4AD";
		//public const string ROLE_4WRDIT_ADMIN = "ROLE_4WRDIT_ADMIN";
		public const string REGISTRATION_4WRDIT_REG = "4WRDIT_REG";
		public const string REGISTRATION_4WRDIT_ADMIN_REG = "4WRDIT_ADMIN_REG";
		public const string FORWRDIT_APPLICATION_CODE = "4WRDIT";
		public const string FORWRDIT_ASSIGNMENT_TYPE = "EXPLICIT";
		//### 20100407
		public const string ROLE_YEC = "YEC";
		public const string ROLE_BODIBEAT_PRODUCT_OWNER = "BBR";
		public const string ROLE_YSISS = "YSISS";
		//### 20061220
		public const string INTERMEDIA_SPECIAL_CHARS = "\\,*&?{}()[]-;~#|$!>%_'ой^\"";
		//### 20061220
		//public const string CDA_USER_COOKIE_NAME = "user";
		public const string CDA_LOGGEDIN_COOKIE_NAME = "loggedin";
		public const string CDA_USER_FIRSTNAME_COOKIE_NAME = "userFirstName";
		public const string CDA_HBX_USER_ID = "hbx_user_id";
		public const string CDA_S_NR = "s_nr";
		public const string CDA_USER_ZIP_CODE_COOKIE = "user_zip";
		
		public const string CDA_INITIATIVES_COOKIE_NAME = "listofinitiatives";
		public const string USE_OWNER_CATEGORY = "OWNER";
		public const string USE_CURRENT_CATEGORY = "CURRENT";
		public const string USE_NONE_CATEGORY = "NONE";

		//public const string FORGOT_PASSWORD_MAIL = "FORGOT_PASSWORD_MAIL";
		public const string FORGOT_PASSWORD_MAILFROM_ADDRESS = "yca@yamaha.com";
		public const string FORGOT_PASSWORD_MAIL_SUBJECT = "Yamaha - Password Request";

		public const string MAIL_TYPE_FORGOTPASSWORD = "FORGOTPASSWORD";
		public const string MAIL_TYPE_WELCOME = "WELCOME";
		public const string YAMAHA_ENTITY_NAME = "Yamaha Corporation of America";
		//public const string YAMAHA_COPYRIGHT_YEAR = "2009";

		public const Int64 RLTN_TYPE_ID_FOR_PRODUCT_AND_SKU = 2500; 
		public const Int64 RLTN_TYPE_ID_FOR_SKU_AND_SKU = 2510; 

		public const string JOBS_CONTENT_TYPE = "JOBS";
		public const string REGULAR_IMAGE_CONTENT_TYPE = "REGULAR_IMAGE";
		public const string TRAINING_CONTENT_TYPE = "TRAINING";
		public const string CDA_CONTENT_TYPE_EXCLUDED_LIST_FOR_DISPLAY  = "'SKU','TECHNOLOGY','SPOTLIGHT','LIFESTYLE','OVERVIEW','POPTEXT','WRAPPER','IMAGE','TEXT_INCLUDE','GENERAL','URL_LINK','PRODUCT_MEDIA','MULTIMEDIA','EVENT','IPLAY_STORY','WORSHIP_ARTICLE','WORSHIP_EVENT','WORSHIP_NEWS','WORSHIP_PROFILE','WORSHIP_TROOM','COLLECTION','SERVICE_INFO','SERVICE_MANUAL','SERVICE_PRODUCT','CHANNEL_SECTION','DETAIL_SECTION','FIELD_TIP','REGULAR_IMAGE','TRAINING','EVENT','ARTIST_NEWS'";
		public const string CDA_CONTENT_TYPE_EXCLUDED_LIST  = "'SKU','TECHNOLOGY','SPOTLIGHT','LIFESTYLE','OVERVIEW','POPTEXT','IMAGE','TEXT_INCLUDE','IPLAY_STORY','WORSHIP_ARTICLE','WORSHIP_EVENT','WORSHIP_NEWS','WORSHIP_PROFILE','WORSHIP_TROOM','COLLECTION','SERVICE_INFO','SERVICE_MANUAL','SERVICE_PRODUCT','CHANNEL_SECTION','DETAIL_SECTION','FIELD_TIP','ARTIST_NEWS'";


        public const string ROLE_4WD_YAMAHA_ADMIN = "4WD_YAMAHA_ADMIN";
        public const string ROLE_4WD_YAMAHA_USER = "4WD_YAMAHA_USER";
        public const string ROLE_4WD_DEALER_PRINCIPLE = "4WD_DEALER_PRINCIPLE";

        public const Int64 YML_MEDIA_COLLECTION_RLTN_ID = 4221;
        public const Int64 YML_PRINT_MEDIA_RLTN_ID = 4223;
        public const Int64 YML_WEB_MEDIA_RLTN_ID = 4222;
        public const Int64 YML_MULTIMEDIA_RLTN_ID = 4224;
        public const Int64 YML_POP_RLTN_ID = 4225;
        public const Int64 YML_POSTER_IMAGE_RLTN_ID = 4226;
        public const Int64 YML_EMBED_HTML_RLTN_ID = 4241;
        public const Int64 YML_REGULARIMAGE_RLTN_ID = 80;
        public const Int64 NEWS_SPOTLIGHT_RLTN_TYPE_ID = 1288;


        //YASI Constants................

        public const Int64 YASI_ABOUT_CAT_ID = 5202001;
        public const Int64 YASI_VIDEOS_CAT_ID = 5202006;
        public const Int64 YASI_PARTNERS_CAT_ID = 5202010;
        public const Int64 YASI_NEWS_CAT_ID = 5202009;
        //public const Int64 EVENT_CAT_ID = 5200758;
        public const Int64 YASI_HOME_CAT_ID = 5202000;
        public const Int64 YASI_ARTIST_CAT_ID = 5202016;
        public const Int64 YASI_BAND_ORCHESTRA_CAT_ID = 5202021;
        public const Int64 YASI_FEATURES_VIDEO_ID = 5202008;
        public const Int64 YASI_VIDEO_LIBRARY_ID = 5202007;
        public const Int64 REMOTE_LIVE_VIDEO_LIBRARY_ID = 5310002;
        public const string YASI_EVENT_DETAIL_URL = "/yasi/eventdetail.html?ENTID=";
        public const Int64 APP_ID = 1;
        public const Int64 YASI_CLASSICAL_ARTIST = 5310020;
        public const Int64 YASI_JAZZ_ARTIST = 5310021;
        public const Int64 YASI_CONTEMPORARY_ARTIST = 5208563;
       
        //E3Disklavier Welcome Mail
        public const string YASI_PROMO_REG = "YASI_PROMO_REG";
        public const string YASI_PROMO_REG_WELCOME_MAIL = "YASI_PROMO_REG_WELCOME_MAIL";
        public const string YASI_PROMO_REG_WELCOME_MAIL_FROM_ADDRESS = "easypass@yamaha.com";
        public const string YASI_PROMO_REG_WELCOME_MAIL_SUBJECT = "YASI - Welcome Confirmation";

        //E3Disklavier Forgot Password
        public const string YASI_PROMO_REG_FORGOT_PASS_MAIL = "YASI_PROMO_REG_FORGOT_PASS_MAIL";
        public const string YASI_PROMO_REG_FORGOT_PASS_MAIL_FROM_ADDRESS = "easypass1@yamaha.com";
        public const string YASI_PROMO_REG_FORGOT_PASS_MAIL_SUBJECT = "YASI - Password Request";

        //RemoteLive Constants
        public const Int64 REMOTE_LIVE_APP_ID = 4;
        public const Int64 REMOTE_LIVE_FEATURE_ONDEMAND_ID = 5310002;
        public const Int64 REMOTE_LIVE_VIDEOS_ONDEMAND_ID = 5310001;
        public const Int64 REMOTE_LIVE_VIDEOS_ALL_ID = 5208723;
        public const string REMOTELIVE_SPECIAL_KEYWORDS = "RL_SPECIAL_KEYWORDS";


        //Start NAMM CMA 
        public const Int64 NAMM_CMA_CONTENT_CATEGORY_ID = 5208125;
        public const Int64 NAMM_CMA_CHANNEL_CATEGORY_ID = 5300003;
        public const Int64 NAMM_CMA_DETAIL_TEMPLATE_ID = 22751;
        public const Int64 NAMM_CMA_IMAGE_CONTENT_CATEGORY_ID = 5200663;
        public const string WEB4_IP_ADDRESS = "web4.usa.yamaha.com";
        public const string LIVE_IP_ADDRESS = "web4.usa.yamaha.com";

		public const Int64 NAMM_MEDIA_CONTENT_CATEGORY_ID = 5208125;
        public const Int64 NAMM_IMAGE_CONTENT_CATEGORY_ID = 5200663;

			//        Images/NAMM -- 5040726

			// medialibrary/NAMM -- 5208125

        //public const Int64 HOME_CAT_ID = 5202000;
        //public const Int64 ABOUT_CAT_ID = 5202001;
        //public const Int64 VIDEOS_CAT_ID = 5202005;
        //public const Int64 PARTNERS_CAT_ID = 5202010;
        //public const Int64 ARTIST_CAT_ID = 5202015;
        //public const Int64 BAND_ORCHESTRA_CAT_ID = 5202018;
        //public const Int64 NEWS_CAT_ID = 5202008;

        public const string YA_WELCOME_MAIL = "YA_WELCOME_MAIL";
        public const string GLOBAL_FORGOT_PASS_MAIL = "GLOBAL_FORGOT_PASS_MAIL";
        public const Int64 YASI_LIFESTYLE_CONTENT_ID = 6666622;
        //Myaccount cookie
        public const string MA_AUTH_TOKEN = "MA_AUTH_TOKEN";
        public const string MA_GUEST = "MA_GUEST";
        public const string PIM_API_NOTIFY_SUMMARY_MAIL = "PIM_API_NOTIFY_SUMMARY_MAIL";
        public const string PIM_API_STATUS_LOG_MAIL = "PIM_API_STATUS_LOG_MAIL";
        public const string MA_REG = "MA_REG";
	}
}
