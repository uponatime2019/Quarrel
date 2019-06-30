﻿using System;
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
using Quarrel.Services;
using GalaSoft.MvvmLight.Messaging;
using Quarrel.Messages.Navigation.SubFrame;
using Quarrel.SubPages.Interfaces;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Quarrel.SubPages
{
    public sealed partial class LoginPage : UserControl, IFullscreenSubPage
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Setup Discord Service
            ServicesManager.Discord.Login(Username.Text, Password.Password);
            Messenger.Default.Send(new SubFrameCloseRequestMessage());
        }

        private void LoginWithToken_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Login with token page
        }

        private async void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://discordapp.com/"));
        }

        private async void Register_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://discordapp.com/register"));
        }

        private void Username_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if ((e.Key == Windows.System.VirtualKey.Enter || e.Key == Windows.System.VirtualKey.Tab) && Password.Visibility == Visibility.Visible)
                Password.Focus(FocusState.Keyboard);
        }

        private void Password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                LoginButton_Click(null, null);
        }

        private void Token_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                LoginButton_Click(null, null);
        }

        private void MFAPassword_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                LoginButton_Click(null, null);
        }

        private void MFAsms_Click(object sender, RoutedEventArgs e)
        {
            // TODO: MFA sms
        }

        private void Navigating(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            // TODO: Captcha
        }

        public bool Hideable { get; } = false;
    }
}