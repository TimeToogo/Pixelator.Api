using System.Web.Script.Serialization;
using NUnit.Framework;

namespace Pixelator.Api.Tests.Codec.Layout
{
    static class AssertEx
    {
        public static void AreEqualByJson(object expected, object actual)
        {
            var serializer = new JavaScriptSerializer();
            var expectedJson = serializer.Serialize(expected);
            var actualJson = serializer.Serialize(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
