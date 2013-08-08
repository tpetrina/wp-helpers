using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace UiHelpers
{
    public static class ImageControlProperties
    {
        public static readonly DependencyProperty IsoImageSourceProperty =
            DependencyProperty.RegisterAttached(
            "IsoImageSource",
            typeof(string),
            typeof(ImageControlProperties),
            new PropertyMetadata(default(BitmapImage), IsoImageSourceChanged));

        private static void IsoImageSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var image = sender as Image;
            if (image == null)
                throw new InvalidOperationException("IsoImageSource can only be used on Image or classes derived from it.");

            using (var isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var stream = isoStore.OpenFile((string)args.NewValue, FileMode.Open, FileAccess.Read);
                var bi = new BitmapImage();
                bi.SetSource(stream);
                image.Source = bi;
            }
        }

        public static void SetIsoImageSource(UIElement element, string value)
        {
            element.SetValue(IsoImageSourceProperty, value);
        }

        public static string GetIsoImageSource(UIElement element)
        {
            return (string)element.GetValue(IsoImageSourceProperty);
        }
    }

}
