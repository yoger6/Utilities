using Commands;

namespace CommandsTest
{
    internal class CommandTestViewModel : ObservableObject
    {
        private int _id;

        public int Id
        {
            private get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public WatchingCommand<object> Command { get; }
        public WatchingCommand ParameterlessCommand { get; }

        public CommandTestViewModel( int desiredPropertyValue )
        {
            Command = new WatchingCommand<object>( o => { }, () => Id == desiredPropertyValue, this, nameof( Id ) );
            ParameterlessCommand= new WatchingCommand( () => { }, () => Id == desiredPropertyValue, this, nameof( Id ) );
        }
    }
}