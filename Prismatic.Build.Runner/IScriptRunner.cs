namespace Prismatic.Build.Runner
{
    public interface IScriptRunner
    {
        ScriptResult RunScript(string script);
    }
}