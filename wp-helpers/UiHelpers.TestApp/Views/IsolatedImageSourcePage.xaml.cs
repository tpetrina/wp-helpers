using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;

namespace UiHelpers.TestApp.Views
{
    public partial class IsolatedImageSourcePage
    {
        public IsolatedImageSourcePage()
        {
            var fileStream = Application.GetResourceStream(new Uri("/UiHelpers.TestApp;component/img.png", UriKind.Relative));
            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!isoStore.FileExists("img.png"))
                {
                    using (var file = isoStore.OpenFile("img.png", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        fileStream.Stream.CopyTo(file);
                    }
                }
            }

            InitializeComponent();
        }
    }
}