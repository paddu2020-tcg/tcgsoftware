using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;

namespace YamahaApp.Common
{
    public class ADUtility
    {
        #region DirectoryService
        public static string[] GetArrayPropertyValue(SearchResult res, string name)
        {
            string[] array = new string[res.Properties[name].Count];
            int num = 0;
            foreach (string text in res.Properties[name])
            {
                if (text.Contains("CN="))
                {
                    array[num] = text.Split(new char[]
                     {
                           ','
                     })[0].Split(new char[]
                     {
                           '='
                     })[1];
                    num++;
                }
            }
            return array;
        }

        public static string GetPropertyValue(SearchResult res, string name)
        {
            string result;
            System.Collections.IEnumerator enumerator = res.Properties[name].GetEnumerator();
            while (enumerator.MoveNext())
            {
                //if (enumerator.MoveNext())
                {
                    string text = (string)enumerator.Current;
                    result = text;
                    return result;
                }
            }

            result = string.Empty;
            return result;
        }
        public static SearchResult GetUserDirectorySearcher(DirectoryEntry entry, string user)
        {
            return new DirectorySearcher(entry)
            {
                SearchScope = System.DirectoryServices.SearchScope.Subtree,
                Filter = BuildSearchFilter(user),
                PropertiesToLoad = 
                      {
                             "mail",
                             "givenName",
                             "sn",
                             "memberOf",
                             "distinguishedName",
                             "department",
                             "company"
                      }
            }.FindOne();
        }
        public static string ConvertUserToUPN(string user)
        {
            int num = user.IndexOf("@");
            string result;
            if (num > -1)
            {
                result = user.Substring(num + 1) + "\\" + user.Substring(0, num);
            }
            else
            {
                result = user;
            }
            return result;
        }
        public static string GetOnlyUserAccountName(string user)
        {
            int num = user.IndexOf("@");
            string result;
            if (num > -1)
            {
                result = user.Substring(0,num);
            }
            else
            {
                result = user;
            }
            return result;
        }
        public static string BuildSearchFilter(string user)
        {
            int num = user.IndexOf("\\");
            string result;
            if (num > -1)
            {
                result = string.Format("(&(objectCategory=person)(objectClass=user)(sAMAccountName={0}))", user.Substring(num + 1));
            }
            else
            {
                result = string.Format("(&(objectCategory=person)(objectClass=user)(userPrincipalName={0}))", user);
            }
            return result;
        }
        public static SearchResult GetUserGroupDirectorySearcher(DirectoryEntry entry, string distinguishedname)
        {
            return new DirectorySearcher(entry)
            {
                SearchScope = System.DirectoryServices.SearchScope.Subtree,
                Filter = string.Format("(member={0})", distinguishedname),
                PropertiesToLoad = 
                      {
                             "sAMAccountName"
                      }
            }.FindOne();
        }
        #endregion
    }
}