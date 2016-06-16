using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace UtilitiesTest
{
    [TestClass]
    public class TypeSwitchWithReturnTest
    {
        private TypeSwitch<int> _switch;

        [TestInitialize]
        public void Initialize()
        {
            _switch = new TypeSwitch<int>();
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void ThrowsIfTypeIsSetWithNullAction()
        {
            _switch.Set( typeof( int ), null );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void ThrowsIfTypeIsAlreadySet()
        {
            _switch.Set( typeof( int ), () => 0 );
            _switch.Set( typeof( int ), () => 0 );
        }

        [TestMethod]
        public void ExecuteInvokesActionAssignedToType()
        {
            _switch.Set( typeof( int ), () => 0 );
            _switch.Set( typeof( string ), () => 1 );

            var result = _switch.Execute( typeof( string ) );

            Assert.AreEqual<int>( 1, result );
        }

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void ThrowsIfExecutedTypeIsNullAndNoFallbackSet()
        {
            _switch.Execute( null );
        }

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void ThrowsIfExecutedTypeIsNotSetAndNoFallbackSet()
        {
            _switch.Execute( typeof( bool ) );
        }


        [TestMethod]
        public void ExecutesFallbackActionAssignedInConstructorWhenExecutedTypeNotFoundOrNull()
        {
            var swithWithFallback = new TypeSwitch<int>( () => 0 );
            swithWithFallback.Set( typeof( int ), () => 1 );

            var result = swithWithFallback.Execute( typeof( bool ) );

            Assert.AreEqual<int>( 0, result );
        }
    }
}