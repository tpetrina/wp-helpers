// Copyright (C) 2013 by Toni Petrina
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace WP.Helpers.Ui
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
