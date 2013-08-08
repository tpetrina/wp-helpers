using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace UiHelpers.TestApp.ViewModel
{
    public class EmptyListBehaviorExamplePageViewModel : ViewModelBaseEx
    {
        private ObservableCollection<string> _items;

        public ObservableCollection<string> Items
        {
            get { return _items; }
            set { Set(ref _items, value); }
        }

        public ICommand RemoveCollectionCommand { get; set; }
        public ICommand AddItemToCollectionCommand { get; set; }

        public EmptyListBehaviorExamplePageViewModel()
        {
            RemoveCollectionCommand = new RelayCommand(RemoveCollection);
            AddItemToCollectionCommand = new RelayCommand(AddItemToCollection);
        }

        private void RemoveCollection()
        {
            Items = null;
        }

        private void AddItemToCollection()
        {
            if (Items == null)
                Items = new ObservableCollection<string>();

            Items.Add("Hello");
        }
    }
}
