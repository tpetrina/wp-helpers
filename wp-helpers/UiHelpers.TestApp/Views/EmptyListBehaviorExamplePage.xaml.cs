using UiHelpers.TestApp.ViewModel;

namespace UiHelpers.TestApp.Views
{
    public partial class EmptyListBehaviorExamplePage
    {
        public EmptyListBehaviorExamplePage()
        {
            InitializeComponent();

            DataContext = new EmptyListBehaviorExamplePageViewModel();
        }

        private void RemoveCollection(object sender, System.EventArgs e)
        {
            ((EmptyListBehaviorExamplePageViewModel)DataContext).RemoveCollectionCommand.Execute(null);
        }

        private void AddItemToCollection(object sender, System.EventArgs e)
        {
            ((EmptyListBehaviorExamplePageViewModel)DataContext).AddItemToCollectionCommand.Execute(null);
        }
    }
}