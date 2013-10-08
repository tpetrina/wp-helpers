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

using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Collections.Specialized;
using Wp7nl.Behaviors;

namespace WP.Helpers.Ui.Behaviors
{
    public class EmptyListBoxBehavior : SafeBehavior<ListBox>
    {
        #region EmptyTemplate dependency property

        public ControlTemplate EmptyTemplate
        {
            get { return (ControlTemplate)GetValue(EmptyTemplateProperty); }
            set { SetValue(EmptyTemplateProperty, value); }
        }

        public static readonly DependencyProperty EmptyTemplateProperty = DependencyProperty.Register(
            "EmptyTemplate", typeof(ControlTemplate), typeof(EmptyListBoxBehavior), new PropertyMetadata(null));

        #endregion

        #region DefaultTemplate dependency property

        public ControlTemplate DefaultTemplate
        {
            get { return (ControlTemplate)GetValue(DefaultTemplateProperty); }
            set { SetValue(DefaultTemplateProperty, value); }
        }

        public static readonly DependencyProperty DefaultTemplateProperty = DependencyProperty.Register(
            "DefaultTemplate", typeof(ControlTemplate), typeof(EmptyListBoxBehavior), new PropertyMetadata(null));

        #endregion

        #region ItemsSource dependency property

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof(IEnumerable), typeof(EmptyListBoxBehavior), new PropertyMetadata(null, ItemsSourceChanged));

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            BindingOperations.SetBinding(this, ItemsSourceProperty, new Binding("ItemsSource")
            {
                Source = AssociatedObject,
                Mode = BindingMode.TwoWay,
                Path = new PropertyPath("ItemsSource")
            });

            Update(null, null);
        }

        private static void ItemsSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((EmptyListBoxBehavior)o).Update(e.OldValue, e.NewValue);
        }

        private void Update(object oldValue, object newValue)
        {
            var oldSource = oldValue as IEnumerable;
            var newSource = newValue as IEnumerable;

            if ((newSource == null || !newSource.GetEnumerator().MoveNext()) && EmptyTemplate != null)
                AssociatedObject.Template = EmptyTemplate;
            else
                AssociatedObject.Template = DefaultTemplate;

            var oldCollection = oldSource as INotifyCollectionChanged;
            var newCollection = newSource as INotifyCollectionChanged;

            if (newCollection != null)
                newCollection.CollectionChanged += newCollection_CollectionChanged;
            if (oldCollection != null)
                oldCollection.CollectionChanged -= newCollection_CollectionChanged;
        }

        void newCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (((IList)sender).Count != 0)
                AssociatedObject.Template = DefaultTemplate;
            else
                AssociatedObject.Template = EmptyTemplate;
        }
    }
}
