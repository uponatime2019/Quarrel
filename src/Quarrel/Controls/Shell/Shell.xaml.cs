﻿// Copyright (c) Quarrel. All rights reserved.

using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quarrel.ViewModels;
using Quarrel.ViewModels.Messages.Gateway;
using Quarrel.ViewModels.Messages.Navigation;
using Quarrel.ViewModels.Services.Discord.CurrentUser;
using Quarrel.ViewModels.Services.Settings;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Quarrel.Controls.Shell
{
    /// <summary>
    /// Main shell used for all Quarrel content.
    /// </summary>
    public sealed partial class Shell : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shell"/> class.
        /// </summary>
        public Shell()
        {
            try
            {
                this.InitializeComponent();

                // Setup SideDrawer
                ContentContainer.SetupInteraction();

                Messenger.Default.Register<GuildNavigateMessage>(this, m =>
                {
                    ContentContainer.OpenLeft();
                });

                Messenger.Default.Register<ChannelNavigateMessage>(this, m =>
                {
                    ContentContainer.CloseLeft();
                });

                Messenger.Default.Register<GatewayMessageRecievedMessage>(this, async m =>
                {
                if (SimpleIoc.Default.GetInstance<ISettingsService>().Roaming.GetValue<bool>(SettingKeys.MentionGlow) &&
                    (m.Message.MentionEveryone ||
                    m.Message.Mentions.Any(x => x.Id == SimpleIoc.Default.GetInstance<ICurrentUserService>().CurrentUser.Model.Id)))
                    {
                        await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                        {
                            FlashMention.Begin();
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                var logger = App.ServiceProvider.GetService<ILogger<Shell>>();

                do
                {
                    logger.LogError(default, ex, "Error constructing Shell");
                    ex = ex.InnerException;
                }
                while (ex != null);

                throw;
            }
        }

        /// <summary>
        /// Gets the MainViewModel for the app.
        /// </summary>
        public MainViewModel ViewModel => App.ViewModelLocator.Main;

        /// <summary>
        /// Gets a value indicating whether or not the view should use the large or small view QuarrelCommandBar.
        /// </summary>
        private bool UseLargeCommandBar => UISize.CurrentState == Large || UISize.CurrentState == ExtraLarge;

        /// <summary>
        /// Updates bindings when UI Size changes.
        /// </summary>
        private void UISize_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            this.Bindings.Update();
        }

        /// <summary>
        /// Toggles Left panes when Hamburger button is press.
        /// </summary>
        private void HamburgerClicked(object sender, EventArgs e)
        {
            ContentContainer.ToggleLeft();
        }

        /// <summary>
        /// Toggles right pane when MemberListToggle button is press.
        /// </summary>
        private void MemberListButtonClicked(object sender, EventArgs e)
        {
            ContentContainer.ToggleRight();
        }
    }
}
