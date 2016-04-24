using System.Collections.Generic;

namespace Prismatic.Build.Runner
{
    public interface IScriptRunner
    {
        ScriptResult RunScript(string script);
        List<ScriptResult> RunScripts(IEnumerable<List<string>> scriptList);
    }
}