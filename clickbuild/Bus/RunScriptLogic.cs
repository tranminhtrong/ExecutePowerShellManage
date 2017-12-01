using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;

namespace clickbuild.Bus
{
	public class RunScriptLogic
	{
		public static string RunScript(string scriptText)
		{
			// create Powershell runspace

			Runspace runspace = RunspaceFactory.CreateRunspace();

			// open it

			runspace.Open();

			// create a pipeline and feed it the script text

			Pipeline pipeline = runspace.CreatePipeline();
			pipeline.Commands.AddScript(scriptText);

			// add an extra command to transform the script
			// output objects into nicely formatted strings

			// remove this line to get the actual objects
			// that the script returns. For example, the script

			// "Get-Process" returns a collection
			// of System.Diagnostics.Process instances.

			pipeline.Commands.Add("Out-String");

			// execute the script

			Collection <PSObject> results = pipeline.Invoke();

			// close the runspace

			runspace.Close();

			// convert the script result into a single string

			StringBuilder stringBuilder = new StringBuilder();
			foreach (PSObject obj in results)
			{
				stringBuilder.AppendLine(obj.ToString());
			}

			return stringBuilder.ToString();
		}

		/// <summary>
		/// Runs a Powershell script taking it's path and parameters.
		/// </summary>
		/// <param name="scriptFullPath">The full file path for the .ps1 file.</param>
		/// <param name="parameters">The parameters for the script, can be null.</param>
		/// <returns>The output from the Powershell execution.</returns>
		//public static ICollection<PSObject> ExecutePowerShellFile(string scriptFullPath, ICollection<CommandParameter> parameters = null)
		//{
		//	var runspace = RunspaceFactory.CreateRunspace();
		//	runspace.Open();
		//	var pipeline = runspace.CreatePipeline();
		//	var cmd = new Command(scriptFullPath);
		//	if (parameters != null)
		//	{
		//		foreach (var p in parameters)
		//		{
		//			cmd.Parameters.Add(p);
		//		}
		//	}
		//	pipeline.Commands.Add(cmd);
		//	var results = pipeline.Invoke();
		//	pipeline.Dispose();
		//	runspace.Dispose();
		//	return results;
		//}

		
	}
}