using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clickbuild.Bus
{
	public class JsonUtilities
	{
		string _serverListFullPath = HttpContext.Current.Server.MapPath("/PowerShellScript/server-list.json");

		public static IList<Models.UndeployParameters> GetServerList()
		{
			IList<Models.UndeployParameters> serverList = new List<Models.UndeployParameters>();

			return serverList;
		}
	}
}