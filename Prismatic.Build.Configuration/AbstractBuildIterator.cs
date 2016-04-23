using System.Collections;
using System.Collections.Generic;
using Prismatic.Build.Configuration.Interfaces;

namespace Prismatic.Build.Configuration
{
    public abstract class AbstractBuildIterator : IBuildIterator<List<string>>
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
            if(_count >= BuildOrder.Count) return null;
            var currentBuildItem = BuildOrder[_count++];
            return (List<string>)BuildConfig.GetType().GetProperty(currentBuildItem).GetValue(BuildConfig);
        }

        public IEnumerator<List<string>> GetEnumerator()
        {
            var enumerator = GetNext();
            while (enumerator != null)
            {
                yield return enumerator;
                enumerator = GetNext();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}