using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UiHelpers.TestApp
{
    public partial class MainPage : INotifyPropertyChanged
    {
        private ObservableCollection<string> _items;

        public ObservableCollection<string> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                RaisePropertyChanged("Items");
            }
        }

        public MainPage()
        {
            InitializeComponent();

            DataContext = this;
        }

        private void InitCollection(object sender, EventArgs e)
        {
            Items = new ObservableCollection<string>();
        }

        private void AddItemToCollection(object sender, EventArgs e)
        {
            if (Items == null)
                Items = new ObservableCollection<string>();

            Items.Add("Hello");
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}