using System.Collections;
using System.Collections.Generic;
using Prismatic.Build.Configuration.Interfaces;

namespace Prismatic.Build.Configuration
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