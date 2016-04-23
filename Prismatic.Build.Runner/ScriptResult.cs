namespace Prismatic.Build.Runner
{
    public class ScriptResult
    {
        private string _scriptOutput;
        public bool IsSuccess { get; set; }

        public string ScriptOutput
        {
            get { return _scriptOutput; }
            set
            {
                _scriptOutput = value;
                IsSuccess = true;
            }
        }
    }
}