using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Prismatic.Build.Runner
{
    public class PowerShellRunner : IScriptRunner
    {
        private readonly string _basePath;

        public PowerShellRunner(string basePath = "")
        {
            _basePath = basePath;
        }

        public ScriptResult RunScript(string script)
        {
            var scriptResult = new ScriptResult();
            using (var powerShellInstance = PowerShell.Create())
            {
                powerShellInstance.AddScript("function echo([String] $message) { return $message }").Invoke();
                powerShellInstance.AddScript("function Write-Host([String] $message) { return $message }").Invoke();
                powerShellInstance.Commands.Clear();

                powerShellInstance.AddScript(Path.Combine(_basePath, script));
                
                //powerShellInstance.AddScript("param($param1) $d = get-date; $s = 'test string value'; " +
                //"$d; $s; $param1;");

                // use "AddParameter" to add a single parameter to the last command/script on the pipeline.
                //powerShellInstance.AddParameter("param1", "parameter 1 value!");

                var psObjects = powerShellInstance.Invoke();

                if (powerShellInstance.Streams.Error.Count > 0)
                {
                    throw new Exception(powerShellInstance.Streams.Error[0].Exception.Message);
                }

                foreach (var psObject in psObjects)
                {
                    scriptResult.ScriptOutput += psObject.BaseObject;
                }
            }
            
            return scriptResult;
        }

        public List<ScriptResult> RunScripts(IEnumerable<List<string>> scriptList)
        {
            var scriptResults = new List<ScriptResult>();
            var scriptTaskLists = scriptList.ToList();
            foreach (var scriptTaskList in scriptTaskLists)
            {
                //build pipeline task
                var taskScriptResult = new ScriptResult();
                foreach (var task in scriptTaskList)
                {
                    taskScriptResult = RunScript(task);
                    taskScriptResult.ScriptOutput += Environment.NewLine;
                }
                scriptResults.Add(taskScriptResult);
            }
            return scriptResults;
        }
    }
}
