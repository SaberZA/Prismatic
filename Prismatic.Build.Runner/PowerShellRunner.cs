using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace Prismatic.Build.Runner
{
    public class PowerShellRunner : IScriptRunner
    {
        public ScriptResult RunScript(string script)
        {
            var scriptResult = new ScriptResult();
            using (var powerShellInstance = PowerShell.Create())
            {
                powerShellInstance.AddScript("function Write-Host {}").Invoke();
                powerShellInstance.Commands.Clear();

                powerShellInstance.AddScript("Write-Host \"Hello World!\";");
                powerShellInstance.AddScript(script);
                

                powerShellInstance.AddScript("param($param1) $d = get-date; $s = 'test string value'; " +
                "$d; $s; $param1;");

                // use "AddParameter" to add a single parameter to the last command/script on the pipeline.
                powerShellInstance.AddParameter("param1", "parameter 1 value!");

                var psObjects = powerShellInstance.Invoke();

                if (powerShellInstance.Streams.Error.Count > 0)
                {
                    // error records were written to the error stream.
                    // do something with the items found.
                    throw new Exception(powerShellInstance.Streams.Error[0].Exception.Message);
                }

                foreach (var psObject in psObjects)
                {
                    scriptResult.ScriptOutput += psObject.BaseObject;
                }
            }
            
            return scriptResult;
        }
    }
}
