using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web.Script.Serialization; 

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for DataStructures used in PIM.
	/// </summary>
	public class DataStructures
	{
        public class StatusLogStruct
        {
            public string apikey { get; set; }
            public string appid { get; set; }
            public string externaldomain { get; set; }
            public List<Data> data { get; set; }

            public StatusLogStruct() { apikey = appid = externaldomain = ""; data = new List<Data>(); }

            public string Serialize()
            {
                StringBuilder strb = new StringBuilder();
                JavaScriptSerializer jsonobj = new JavaScriptSerializer();
                jsonobj.MaxJsonLength = 5000000;
                jsonobj.Serialize(this, strb);
                return strb.ToString();
            }
        }

        public class Data
        {
            public string apikey { get; set; }
            public string appid { get; set; }
            public string externaldomain { get; set; }
            public string calldatetime { get; set; }
            public string userid { get; set; }
            public string status { get; set; }
            public string reason { get; set; }
            public string details { get; set; }
            public string[] notifylist { get; set; }
        }
	}
}
