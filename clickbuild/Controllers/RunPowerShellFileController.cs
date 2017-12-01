using clickbuild.Models;
using System.Web.Mvc;
using clickbuild.Bus;

namespace clickbuild.Controllers
{
	public class RunPowerShellFileController : Controller
    {
        // GET: RunPowerShellFile
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult ExecutePowerShellFile(PowerShellScript data)
		{
			data.FilePath = @"D:\toys\deleteBinFromTestProject.ps1";
			var result = RunScriptLogic.ExecutePowerShellFile(data.FilePath, null);
			ViewBag.Result = result;

			return View("Index");
		}
    }
}