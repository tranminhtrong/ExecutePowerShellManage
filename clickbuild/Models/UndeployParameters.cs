using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace clickbuild.Models
{
	public class UndeployParameters
	{
		public UndeployParameters(string name, string value)
		{
			this.Name = name;
			this.Value = value;
		}
		public string Name { get; set; }
		public string Value { get; set; }
	}
}