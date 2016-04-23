using System.Collections.Generic;

namespace Prismatic.BuildConfiguration
{
    public abstract class AbstractBuildIterator : IBuildIterator
    {
        protected readonly IBuildConfig BuildConfig;
        protected List<string> BuildOrder;
        private int _count;

        protected AbstractBuildIterator(IBuildConfig buildConfig, List<string> buildOrder = null)
        {
            BuildConfig = buildConfig;
            BuildOrder = buildOrder ?? new List<string>();
        }

        public List<string> GetNext()
        {
            var currentBuildItem = BuildOrder[_count++];
            return (List<string>)BuildConfig.GetType().GetProperty(currentBuildItem).GetValue(BuildConfig);
        }
    }
}