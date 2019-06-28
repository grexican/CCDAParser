using System.Collections.Generic;
using System.IO;
using System.Reflection;
using HtmlAgilityPack;
using Xunit.Sdk;

namespace CqmSolution.XmlExtensions.Tests
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

            var doc = new HtmlDocument();
            doc.Load(path);

            var ret = new List<object>();

            ret.Add(doc);
            ret.AddRange(_expectations);

            yield return ret.ToArray();
        }
    }
}
