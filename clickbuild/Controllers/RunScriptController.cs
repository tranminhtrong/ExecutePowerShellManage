using System.Web.Mvc;
using clickbuild.Bus;
using clickbuild.Models;

namespace clickbuild.Controllers
{
	public class RunScriptController : Controller
    {
        // GET: RunScript
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult RunScript(PowerShellScript data)
		{
			string result = RunScriptLogic.RunScript(data.ScriptCommand);
			ViewBag.Result = result;
			return View("Index");
		}
    }
}