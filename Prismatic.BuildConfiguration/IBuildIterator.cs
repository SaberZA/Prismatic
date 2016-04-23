using System.Collections.Generic;

namespace Prismatic.BuildConfiguration
{
    public interface IBuildIterator
    {
        List<string> GetNext();
    }
}