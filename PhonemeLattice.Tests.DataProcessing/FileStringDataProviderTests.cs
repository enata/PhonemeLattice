using System.Linq;
using NUnit.Framework;
using PhonemeLattice.DataProcessing;

namespace PhonemeLattice.Tests.DataProcessing
{
    [TestFixture]
    public sealed class FileStringDataProviderTests
    {
        [Test]
        public void GetStringsTest()
        {
            const string filePath = "test.txt";
            var provider = new FileStringDataProvider(filePath);

            var data = provider.GetStrings().ToArray();

            Assert.AreEqual(2, data.Length);
            Assert.AreEqual("1", data[0]);
            Assert.AreEqual("2", data[1]);
        }
    }
}