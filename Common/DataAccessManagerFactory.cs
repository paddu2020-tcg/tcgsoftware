using System;
using YamahaApp.Data;

namespace YamahaApp.Common
{
	/// <summary>
	/// Summary description for DataAccessManagerFactory.
	/// </summary>
	public class DataAccessManagerFactory
	{
		public DataAccessManagerFactory()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static DataLib GetDataAccessManager (string type)
		{
			DataLib dataAccessManager = null;
			if ("YEC" == type)
			{
				dataAccessManager = new YEC.Data.YECDataLib();
			}
			if ("YSISS" == type)
			{
                dataAccessManager = new YSISS.Data.YSISSDataLib();
			}
			if ("YCA" == type)
			{
                dataAccessManager = new YCA.Data.YCADataLib();
			}
			if ("YCAS" == type)
			{
				dataAccessManager = new YCAS.Data.YCASDataLib();
			}
            return dataAccessManager;
		}
	}
}
