using System.Collections.Generic;

namespace Prismatic.BuildConfiguration
{
    public interface IBuildConfig
    {
        string Language { get; set; }
        string Solution { get; set; }
        List<string> before_install { get; set; }
        List<string> install { get; set; }
        List<string> before_script { get; set; }
        List<string> script { get; set; }
        List<string> after_success { get; set; }
        List<string> after_failure { get; set; }
        List<string> before_deploy { get; set; }
        List<string> deploy { get; set; }
        List<string> after_deploy { get; set; }
        List<string> after_script { get; set; }
    }
}