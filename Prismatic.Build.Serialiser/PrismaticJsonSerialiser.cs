using System.IO;
using Newtonsoft.Json;

namespace Prismatic.Build.Serialiser
{
    public class PrismaticJsonSerialiser : IPrismaticSerialiser
    {
        private JsonSerializer _jsonSerializer;

        public PrismaticJsonSerialiser()
        {
            _jsonSerializer = new Newtonsoft.Json.JsonSerializer();
        }

        public T Deserialise<T>(StringReader stringReader)
        {
            return _jsonSerializer.Deserialize<T>(new JsonTextReader(stringReader));
        }

        public T Deserialise<T>(string document)
        {
            return _jsonSerializer.Deserialize<T>(new JsonTextReader(new StringReader(document)));
        }

        public void Serialise(TextWriter textWriter, object obj)
        {
            _jsonSerializer.Serialize(textWriter, obj);
        }
    }
}