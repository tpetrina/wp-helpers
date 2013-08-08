using System.Collections.Generic;

namespace UiHelpers.TestApp.ViewModel
{
    public class MainViewModel : ViewModelBaseEx
    {
        private List<ExampleViewModel> _examples;

        public List<ExampleViewModel> Examples
        {
            get { return _examples; }
            set { Set(ref _examples, value); }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
                Examples = new List<ExampleViewModel>
                    {
                        new ExampleViewModel
                            {
                                Name = "Test example"
                            }
                    };
            }
            else
            {
                // Code runs "for real"
                Examples = new List<ExampleViewModel>
                    {
                        new ExampleViewModel
                            {
                                Name = "Empty List Box Template behavior",
                                Uri = "/Views/EmptyListBehaviorExamplePage.xaml"
                            },
                        new ExampleViewModel
                            {
                                Name = "Isolated image source",
                                Uri = "/Views/IsolatedImageSourcePage.xaml"
                            }
                    };
            }
        }
    }
}