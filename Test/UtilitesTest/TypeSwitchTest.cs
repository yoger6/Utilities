using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities;

namespace UtilitiesTest
{
    [TestClass]
    public class TypeSwitchTest
    {
        private TypeSwitch _switch;
        private bool _intActionInvoked;
        private bool _stringActionInvoked;

        [TestInitialize]
        public void Initialize()
        {
            _switch = new TypeSwitch();
        }
        

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void ThrowsIfTypeIsAlreadySet()
        {
            _switch.Set( typeof( int ), IntegerAction );
            _switch.Set( typeof( int ), IntegerAction );
        }

        [TestMethod]
        public void ExecuteInvokesActionAssignedToType()
        {
            _switch.Set( typeof( int ), IntegerAction );
            _switch.Set( typeof( string ), StringAction );

            _switch.Execute( typeof( int ) );

            Assert.IsTrue( _intActionInvoked );
            Assert.IsFalse( _stringActionInvoked );
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
            var swithWithFallback = new TypeSwitch( StringAction );
            swithWithFallback.Set( typeof( int ), IntegerAction );

            swithWithFallback.Execute( typeof( bool ) );

            Assert.IsTrue( _stringActionInvoked );
        }
        
        private void StringAction()
        {
            _stringActionInvoked = true;
        }

        private void IntegerAction()
        {
            _intActionInvoked = true;
        }
    }
}
