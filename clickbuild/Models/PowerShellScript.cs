using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clickbuild.Models
{
	public class PowerShellScript
	{
		public string ScriptCommand { get; set; }
		public string  _filePath { get; set; }
		public string FilePath {
			get {
				_filePath = HttpContext.Current.Server.MapPath("/PowerShellScript");
				return _filePath;
			}
			set {
				_filePath = value;
			}
		}
		public string FileName { get; set; }
	}
}