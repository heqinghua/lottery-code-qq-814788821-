using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = System.Windows.WindowState.Maximized;
           // this.ResizeMode = System.Windows.ResizeMode.CanMinimize;
            this.borwers.Source = new Uri("http://mh666.net/login.html");
            this.borwers.LoadCompleted += borwers_LoadCompleted;
            this.borwers.Navigating += borwers_Navigating;
            SetWebBrowserSilent(this.borwers, true);
        }

        void borwers_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            //MessageBox.Show("borwers_Navigating");
            //MainGrid.Visibility = System.Windows.Visibility.Collapsed;
            //borwers.Visibility = System.Windows.Visibility.Collapsed;
            changeLoading.Visibility = System.Windows.Visibility.Visible;
        }

        void borwers_LoadCompleted(object sender, NavigationEventArgs e)
        {
            //MessageBox.Show("borwers_LoadCompleted");
           changeLoading.Visibility = System.Windows.Visibility.Hidden;
           //borwers.Visibility = System.Windows.Visibility.Visible;
        }

        private void borwers_Loaded_1(object sender, RoutedEventArgs e)
        {
            borwers.Visibility = System.Windows.Visibility.Visible;
        }


        /// <summary>
        /// 设置浏览器静默，不弹错误提示框
        /// </summary>
        /// <param name="webBrowser">要设置的WebBrowser控件浏览器</param>
        /// <param name="silent">是否静默</param>
        private void SetWebBrowserSilent(WebBrowser webBrowser, bool silent)
        {
            FieldInfo fi = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fi != null)
            {
                object browser = fi.GetValue(webBrowser);
                if (browser != null)
                    browser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, browser, new object[] { silent });
            }
        }

    }
}
