using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace Ccda.Tests
{
    public class XmlDocumentDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly object[] _expectations;

        public XmlDocumentDataAttribute(string filePath, params object[] expectations)
        {
            _filePath = filePath;
            _expectations = expectations ?? new object[0];
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);
            
            var ret = new List<object>();

            ret.Add(path);
            ret.AddRange(_expectations);

            yield return ret.ToArray();
        }
    }
}
