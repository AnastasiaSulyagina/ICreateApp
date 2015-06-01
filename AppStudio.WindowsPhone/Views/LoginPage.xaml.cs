using AppStudio.Services;
using AppStudio.ViewModels;
using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace AppStudio.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();

            PasswordBox.GotFocus += RemovePassword;
            PasswordBox.LostFocus += AddPassword;
            LoginBox.GotFocus += RemoveText;
            LoginBox.LostFocus += AddText;
        }

        protected void RemovePassword(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = "";
        }

        protected void AddPassword(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "")
                PasswordBox.Password = "пароль";
        }

        protected void RemoveText(object sender, RoutedEventArgs e)
        {
            LoginBox.Text = "";
        }

        protected void AddText(object sender, RoutedEventArgs e)
        {
            if (LoginBox.Text == "")
                LoginBox.Text = "логин";
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void LoginBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            await ServerAPI.Register(LoginBox.Text, PasswordBox.Password, PasswordBox.Password);
            NavigationServices.NavigateToPage("MainPage");
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            await ServerAPI.Login(LoginBox.Text, PasswordBox.Password);
            NavigationServices.NavigateToPage("MainPage");
        }
    }
}
