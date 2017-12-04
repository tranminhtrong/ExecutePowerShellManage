using clickbuild.Models;
using System.Web.Mvc;
using clickbuild.Bus;
using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using System.Linq;

namespace clickbuild.Controllers
{
	public class RunPowerShellFileController : Controller
    {
        // GET: RunPowerShellFile
        public ActionResult Index()
        {
			IList<Models.UndeployParameters> undeployParameters = new List<Models.UndeployParameters>();
			undeployParameters.Add(new UndeployParameters("CSCTRSNWK001_SiteList", "CSCTRSNWK001_SiteList.xml"));
			undeployParameters.Add(new UndeployParameters("CSCTRSNWK002_SiteList", "CSCTRSNWK002_SiteList.xml"));
			undeployParameters.Add(new UndeployParameters("CSCTRSNWK003_SiteList", "CSCTRSNWK003_SiteList.xml"));
			undeployParameters.Add(new UndeployParameters("CSCTRSNWK005_SiteList", "CSCTRSNWK005_SiteList.xml"));
			undeployParameters.Add(new UndeployParameters("CSCTRSNWK006_SiteList", "CSCTRSNWK006_SiteList.xml"));
			undeployParameters.Add(new UndeployParameters("CSCTRSNWK007_SiteList", "CSCTRSNWK007_SiteList.xml"));
			undeployParameters.Add(new UndeployParameters("CSCTRSNWK008_SiteList", "CSCTRSNWK008_SiteList.xml"));

			var data = new ViewModelUndeployParameters();
			data.undeployParametersModel = undeployParameters.AsEnumerable<Models.UndeployParameters>();
            return View(data);
        }

		public ActionResult ExecutePowerShellFile(ViewModelUndeployParameters data)
		{
			data.powershellScriptModel.FileName = "undeploy.ps1";

			var result = string.Empty;//RunScriptLogic.ExecutePowerShellFile(data, null);
			ViewBag.Result = result;

			return View("Index");
		}
    }
}