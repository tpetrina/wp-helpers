using System.Windows;
using System.Windows.Controls;
using UiHelpers.Extensions;

namespace UiHelpers.AttachedProperties
{
    public static class GridEx
    {
        #region RowDefinitions attached property
        public static string GetRowDefinitions(DependencyObject obj)
        {
            return (string)obj.GetValue(RowDefinitionsProperty);
        }

        public static void SetRowDefinitions(DependencyObject obj, string value)
        {
            obj.SetValue(RowDefinitionsProperty, value);
        }

        public static readonly DependencyProperty RowDefinitionsProperty =
            DependencyProperty.RegisterAttached("RowDefinitions",
                                                typeof (string),
                                                typeof (Grid),
                                                new PropertyMetadata(string.Empty,
                                                                     RowDefinitions_PropertyChangedCallback));

        private static void RowDefinitions_PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var grid = o as Grid;
            if (grid != null)
                grid.RowDefinitions.ParseAndFill(args.NewValue as string);
        } 
        #endregion

        #region ColumnDefinitions attached property
        public static string GetColumnDefinitions(DependencyObject obj)
        {
            return (string)obj.GetValue(ColumnDefinitionsProperty);
        }

        public static void SetColumnDefinitions(DependencyObject obj, string value)
        {
            obj.SetValue(ColumnDefinitionsProperty, value);
        }

        public static readonly DependencyProperty ColumnDefinitionsProperty =
            DependencyProperty.RegisterAttached("ColumnDefinitions",
                                                typeof (string),
                                                typeof (Grid),
                                                new PropertyMetadata(string.Empty,
                                                                     ColumnDefinitions_PropertyChangedCallback));

        private static void ColumnDefinitions_PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var grid = o as Grid;
            if (grid != null)
                grid.ColumnDefinitions.ParseAndFill(args.NewValue as string);
        }
        #endregion
    }
}
