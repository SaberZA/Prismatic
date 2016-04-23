using System.IO;

namespace Prismatic.Build.Serialiser
{
    public interface IPrismaticSerialiser
    {
        T Deserialise<T>(StringReader stringReader);
        T Deserialise<T>(string document);
        void Serialise(TextWriter textWriter, object obj);
    }
}