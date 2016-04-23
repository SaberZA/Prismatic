using System.Collections.Generic;

namespace Prismatic.BuildConfiguration
{
    public class DefaultBuildIterator : AbstractBuildIterator
    {
        public DefaultBuildIterator(IBuildConfig buildConfig)
            : base(buildConfig)
        {
            BuildOrder = new List<string>
            {
                "before_install",
                "install",
                "before_script",
                "script",
                "after_success",
                "after_failure",
                "before_deploy",
                "deploy",
                "after_deploy",
                "after_script"
            };
        }
    }
}