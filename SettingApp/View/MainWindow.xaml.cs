using System.Windows;
using System.Configuration;
using System;
using System.Windows.Media;
using System.Windows.Controls.Primitives;

namespace SettingApp.View
{
    /// <summary>
    /// Interaction logic for WindowView.xaml
    /// </summary>
    public partial class WindowView : Window
    {
        public static string suffix = null;
        public int MaxRow = 0;
        public double row_height = 0;
        public double header_height = 0;
        public WindowView()
        {
            InitializeComponent();
            suffix = ConfigurationManager.AppSettings["FILE_SUFFIX"];
            MaxRow = Int32.Parse(ConfigurationManager.AppSettings["MAX_ROW_COUNT"]);                      
        }
        private static T FindVisualChild<T>(DependencyObject current) where T : DependencyObject
        {
            if (current == null) return null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(current);
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(current, i);
                if (child is T) return (T)child;
                T result = FindVisualChild<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            row_height = SettingViews.myGrid.RowHeight;
            DataGridColumnHeadersPresenter headers = FindVisualChild<DataGridColumnHeadersPresenter>(SettingViews.myGrid);
            if (headers != null)
            {
                header_height = headers.ActualHeight;
            }
            SettingViews.myGrid.MaxHeight = row_height * MaxRow + header_height;
        }
    }
}
