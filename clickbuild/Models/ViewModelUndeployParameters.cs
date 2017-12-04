using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clickbuild.Models
{
	public class ViewModelUndeployParameters
	{
		public PowerShellScript powershellScriptModel { get; set; }
		public IEnumerable<UndeployParameters> undeployParametersModel { get; set; }
	}
}