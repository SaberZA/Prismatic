using System.Collections;
using System.Collections.Generic;

namespace Prismatic.Build.Configuration.Interfaces
{
    public interface IBuildIterator<out T> : IEnumerable<T>
    {
        List<string> GetNext();
    }
}