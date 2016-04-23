using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Prismatic.Build.Serialiser
{
    public class PrismaticDefaultSerialiser : IPrismaticSerialiser
    {
        private Deserializer _deserializer;
        private Serializer _serializer;

        public PrismaticDefaultSerialiser()
        {
            _deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
            _serializer = new Serializer();
        }

        public T Deserialise<T>(StringReader stringReader)
        {
            return _deserializer.Deserialize<T>(stringReader);
        }

        public void Serialise(TextWriter textWriter, object obj)
        {
            _serializer.Serialize(textWriter, obj);
        }

        public T Deserialise<T>(string document)
        {
            return _deserializer.Deserialize<T>(new StringReader(document));
        }
    }
}