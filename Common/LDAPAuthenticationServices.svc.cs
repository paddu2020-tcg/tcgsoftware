using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.Protocols;
using System.Collections;
using System.Net;
using System.DirectoryServices;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;
using System.Web.Services;
using System.Configuration;
namespace YamahaApp.Common
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class LDAPAuthenticationServices : ILDAPAuthenticationServices
    {
        public ADSVCUserResultInfo ValidateLDAPUser(string userid, string password, string reqMode)
        {
            ADSVCUserResultInfo aDUserResultInfo = new ADSVCUserResultInfo();
            
            string domainPath = ConfigurationManager.AppSettings.Get("domainPath").ToString();
            string userName = ConfigurationManager.AppSettings.Get("defaultUserName").ToString();
            string Password = ConfigurationManager.AppSettings.Get("defaultPassword").ToString();
            aDUserResultInfo.Status = "C";
            aDUserResultInfo.Message = string.Empty;
            
            try
            {
                using (HostingEnvironment.Impersonate())
                {

                    DirectoryEntry directoryEntry = null;

                    if (userid != "" && password != "" && reqMode == "AUTHENTICATE")
                    {
                        directoryEntry = new DirectoryEntry(domainPath,  ADUtility.GetOnlyUserAccountName(userid), password);
                    }
                    else
                    {
                        directoryEntry = new DirectoryEntry(domainPath, userName, Password);
                    }
                    aDUserResultInfo.Mode = reqMode;
                    try
                    {
                        SearchResult userDirectorySearcher = ADUtility.GetUserDirectorySearcher(directoryEntry, ADUtility.ConvertUserToUPN(userid + "@" + ConfigurationManager.AppSettings.Get("domain").ToString()));
                        if (userDirectorySearcher == null)
                        {
                            aDUserResultInfo.Status = "E";
                            aDUserResultInfo.Message = string.Format("User Not Found {0}", userid);
                        }
                        else
                        {
                            aDUserResultInfo.FirstName = ADUtility.GetPropertyValue(userDirectorySearcher, "givenName");
                            aDUserResultInfo.LastName = ADUtility.GetPropertyValue(userDirectorySearcher, "sn");
                            aDUserResultInfo.Email = ADUtility.GetPropertyValue(userDirectorySearcher, "mail");
                            aDUserResultInfo.Department = ADUtility.GetPropertyValue(userDirectorySearcher, "department");
                            aDUserResultInfo.Company = ADUtility.GetPropertyValue(userDirectorySearcher, "company");
                            string propertyValue = ADUtility.GetPropertyValue(userDirectorySearcher, "distinguishedname");
                            if (reqMode == "AUTHENTICATE")
                            {
                                aDUserResultInfo.Grouplist = ADUtility.GetArrayPropertyValue(userDirectorySearcher, "memberOf");
                            }
                            SearchResult userGroupDirectorySearcher = ADUtility.GetUserGroupDirectorySearcher(directoryEntry, propertyValue);
                            if (userGroupDirectorySearcher == null)
                            {
                                aDUserResultInfo.Message = string.Format("User {0} validated but ", userid) + "--" + string.Format("Could Not Find Groups For User {0}", userid);
                            }
                            else
                            {
                                aDUserResultInfo.Group = ADUtility.GetPropertyValue(userGroupDirectorySearcher, "sAMAccountName");
                                aDUserResultInfo.Message = string.Format("User {0} {1}d", userid, reqMode.ToLower());
                            }


                        }

                    }
                    catch (Exception ex)
                    {
                        aDUserResultInfo.Status = "E";
                        aDUserResultInfo.ErrorMessage = ((System.DirectoryServices.DirectoryServicesCOMException)(ex)).ExtendedErrorMessage.ToString();
                        if (aDUserResultInfo.ErrorMessage.Contains("775"))
                        {
                            aDUserResultInfo.Message = string.Format("Account is locked for this user {0}", userid);
                        }
                        else if (aDUserResultInfo.ErrorMessage.Contains("52e"))
                        {
                            aDUserResultInfo.Message = string.Format("Invalid User/Password {0}", userid); ;
                        }
                        else
                        {
                            aDUserResultInfo.Message = string.Format("Err {0}", userid);
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                aDUserResultInfo.Status = "E";
                aDUserResultInfo.Message = string.Format("User credentials are not valid {0}", userid);
                aDUserResultInfo.ErrorMessage = ex1.StackTrace.ToString();
            }
            return aDUserResultInfo;
        }

        
    }
}
