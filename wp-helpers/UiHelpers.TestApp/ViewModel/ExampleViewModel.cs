using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace UiHelpers.TestApp.ViewModel
{
    public class ExampleViewModel : ViewModelBaseEx
    {
        private string _name;
        private string _uri;

        public string Name
        {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public string Uri
        {
            get { return _uri; }
            set { Set(ref _uri, value); }
        }

        public ICommand TapCommand { get; set; }

        public ExampleViewModel()
        {
            TapCommand = new RelayCommand(TapExecuted);
        }

        private void TapExecuted()
        {
            Messenger.Default.Send(new NavigateArgs
                {
                    Uri = Uri
                });
        }
    }
}
