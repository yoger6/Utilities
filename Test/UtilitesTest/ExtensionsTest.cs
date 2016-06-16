using System.Linq;
using NUnit.Framework;
using Utilities;

namespace UtilitiesTest
{
    public class ExtensionsTest
    {
        private class IntContainer
        {
            public int Number { get; set; }
        }

        [Test]
        public void EachExecutesActionOnEachEnumeratedElement()
        {
            var elements = new[]
            {
                new IntContainer(),
                new IntContainer(),
                new IntContainer()
            };

            elements.Each( x => x.Number++ );

            Assert.IsTrue( elements.All( x=>x.Number == 1 ) );
        }
    }
}
