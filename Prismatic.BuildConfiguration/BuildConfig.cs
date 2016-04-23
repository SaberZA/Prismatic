using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prismatic.BuildConfiguration
{
    public class DefaultBuildConfig : IBuildConfig
    {
        public string Language { get; set; }
        public string Solution { get; set; }
        public List<string> before_install { get; set; }
        public List<string> install { get; set; }
        public List<string> before_script { get; set; }
        public List<string> script { get; set; }
        public List<string> after_success { get; set; }
        public List<string> after_failure { get; set; }
        public List<string> before_deploy { get; set; }
        public List<string> deploy { get; set; }
        public List<string> after_deploy { get; set; }
        public List<string> after_script { get; set; }
    }
}
