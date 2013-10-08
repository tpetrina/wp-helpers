using System;
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

        #region RowCol attached property
        public static string GetRowCol(DependencyObject obj)
        {
            return (string)obj.GetValue(RowColProperty);
        }

        public static void SetRowCol(DependencyObject obj, string value)
        {
            obj.SetValue(RowColProperty, value);
        }

        // Using a DependencyProperty as the backing store for RowCol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowColProperty =
            DependencyProperty.RegisterAttached("RowCol", typeof(string), typeof(Grid), new PropertyMetadata(string.Empty, RowCol_PropertyChangedCallback));

        private static void RowCol_PropertyChangedCallback(DependencyObject o, DependencyPropertyChangedEventArgs args)
        {
            var grid = o as Grid;
            if (grid == null)
                return;

            var format = args.NewValue as string;
            if (format != null)
            {
                var values = format.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
                if (values.Length == 1)
                {
                    grid.RowDefinitions.ParseAndFill(values[0]);
                    grid.ColumnDefinitions.ParseAndFill(null);
                }
                else if (values.Length > 1)
                {
                    grid.RowDefinitions.ParseAndFill(values[0]);
                    grid.ColumnDefinitions.ParseAndFill(values[1]);
                }
            }
            else
            {
                grid.RowDefinitions.ParseAndFill(null);
                grid.ColumnDefinitions.ParseAndFill(null);
            }
        }
        #endregion
    }
}
