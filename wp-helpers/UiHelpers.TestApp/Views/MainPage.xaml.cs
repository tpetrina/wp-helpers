using System;
using GalaSoft.MvvmLight.Messaging;
using UiHelpers.TestApp.ViewModel;

namespace UiHelpers.TestApp.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Messenger.Default.Register<NavigateArgs>(this, Navigate);
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            Messenger.Default.Unregister<NavigateArgs>(this);
            base.OnNavigatedFrom(e);
        }

        private void Navigate(NavigateArgs args)
        {
            NavigationService.Navigate(new Uri(args.Uri, UriKind.Relative));
        }
    }
}