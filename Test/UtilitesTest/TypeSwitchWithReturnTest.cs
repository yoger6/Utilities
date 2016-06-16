using System;
using NUnit.Framework;
using Utilities;

namespace UtilitiesTest
{
    [TestFixture]
    public class TypeSwitchWithReturnTest
    {
        [SetUp]
        public void Setup()
        {
            _switch = new TypeSwitch<int>();
        }

        private TypeSwitch<int> _switch;

        [Test]
        public void ExecuteInvokesActionAssignedToType()
        {
            _switch.Set( typeof(int), () => 0 );
            _switch.Set( typeof(string), () => 1 );

            var result = _switch.Execute( typeof(string) );

            Assert.AreEqual( 1, result );
        }

        [Test]
        public void ExecutesFallbackActionAssignedInConstructorWhenExecutedTypeNotFoundOrNull()
        {
            var swithWithFallback = new TypeSwitch<int>( () => 0 );
            swithWithFallback.Set( typeof(int), () => 1 );

            var result = swithWithFallback.Execute( typeof(bool) );

            Assert.AreEqual( 0, result );
        }

        [Test]
        public void ThrowsIfExecutedTypeIsNotSetAndNoFallbackSet()
        {
            TestDelegate execute = () => _switch.Execute( typeof(bool) );

            Assert.Throws<InvalidOperationException>( execute );
        }

        [Test]
        public void ThrowsIfExecutedTypeIsNullAndNoFallbackSet()
        {
            TestDelegate setting = () => _switch.Execute( null );

            Assert.Throws<InvalidOperationException>( setting );
        }

        [Test]
        public void ThrowsIfTypeIsAlreadySet()
        {
            TestDelegate setting = () => _switch.Set( typeof(int), () => 0 );

            setting.Invoke();

            Assert.Throws<ArgumentException>( setting );
        }

        [Test]
        public void ThrowsIfTypeIsSetWithNullAction()
        {
            TestDelegate setting = () => _switch.Set( typeof(int), null );
            ;

            Assert.Throws<ArgumentNullException>( setting );
        }
    }
}