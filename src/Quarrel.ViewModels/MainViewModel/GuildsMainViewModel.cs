﻿// Copyright (c) Quarrel. All rights reserved.

using GalaSoft.MvvmLight.Command;
using JetBrains.Annotations;
using Quarrel.ViewModels.Helpers;
using Quarrel.ViewModels.Messages.Navigation;
using Quarrel.ViewModels.Models.Bindables;
using Quarrel.ViewModels.Models.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Quarrel.ViewModels
{
    /// <summary>
    /// The ViewModel for all data throughout the app.
    /// </summary>
    public partial class MainViewModel
    {
        private RelayCommand<IGuildListItem> _navigateGuild;
        private BindableGuild _currentGuild;
        private BindableGuildMember _currentGuildMember;

        /// <summary>
        /// Gets a command that sends Messenger Request to change Guild.
        /// </summary>
        public RelayCommand<IGuildListItem> GuildListItemClicked => _navigateGuild = _navigateGuild ?? new RelayCommand<IGuildListItem>(
            (guildListItem) =>
            {
                if (guildListItem is BindableGuild bGuild)
                {
                    CurrentGuild.Selected = false;
                    bGuild.Selected = true;
                    MessengerInstance.Send(new GuildNavigateMessage(bGuild));
                }
                else if (guildListItem is BindableGuildFolder bindableGuildFolder)
                {
                    bool collapsed = !bindableGuildFolder.IsCollapsed;
                    bindableGuildFolder.IsCollapsed = collapsed;
                    foreach (var guildId in bindableGuildFolder.Model.GuildIds)
                    {
                        _guildsService.AllGuilds[guildId].IsCollapsed = collapsed;
                    }
                }
            });

        /// <summary>
        /// Gets or sets the currently selected guild.
        /// </summary>
        public BindableGuild CurrentGuild
        {
            get => _currentGuild;
            set => Set(ref _currentGuild, value);
        }

        /// <summary>
        /// Gets the current user's BindableGuildMember in the current guild.
        /// </summary>
        public BindableGuildMember CurrentGuildMember
        {
            get => _currentGuildMember;
            private set => Set(ref _currentGuildMember, value);
        }

        /// <summary>
        /// Gets all Guilds the current member is in.
        /// </summary>
        [NotNull]
        public ObservableRangeCollection<IGuildListItem> BindableGuilds { get; private set; } =
            new ObservableRangeCollection<IGuildListItem>();

        private void RegisterGuildsMessages()
        {
            MessengerInstance.Register<GuildNavigateMessage>(this, m =>
            {
                if (CurrentGuild != m.Guild)
                {
                    BindableChannel channel =
                        m.Guild.Channels.FirstOrDefault(x => x.IsTextChannel && x.Permissions.ReadMessages);
                    _dispatcherHelper.CheckBeginInvokeOnUi(() =>
                    {
                        CurrentChannel = channel;
                        CurrentGuild = m.Guild;
                        BindableMessages.Clear();

                        if (m.Guild.IsDM)
                        {
                            CurrentBindableMembers.Clear();
                        }

                        if (!m.Guild.IsDM)
                        {
                            CurrentGuildMember = _guildsService.GetGuildMember(_currentUserService.CurrentUser.Model.Id, m.Guild.Model.Id);
                        }
                        else
                        {
                            CurrentGuildMember = new BindableGuildMember(
                            new DiscordAPI.Models.GuildMember()
                            {
                                User = _currentUserService.CurrentUser.Model,
                            },
                            "DM",
                            _currentUserService.CurrentUser.Presence);
                        }
                    });

                    if (channel != null)
                    {
                        MessengerInstance.Send(new ChannelNavigateMessage(channel));
                    }

                    _analyticsService.Log(m.Guild.IsDM ? Constants.Analytics.Events.OpenDMs : Constants.Analytics.Events.OpenGuild);
                }
            });

            // Handles string message used for App Events
            MessengerInstance.Register<string>(this, m =>
            {
                if (m == "GuildsReady")
                {
                    _dispatcherHelper.CheckBeginInvokeOnUi(() =>
                    {
                        // Show guilds
                        BindableGuilds.Clear();
                        BindableGuilds.Add(_guildsService.AllGuilds["DM"]);
                        foreach (var folder in _guildsService.AllGuildFolders)
                        {
                            if (folder.Model.Id != null)
                            {
                                BindableGuilds.Add(folder);
                            }

                            foreach (var guildId in folder.Model.GuildIds)
                            {
                                BindableGuilds.Add(_guildsService.AllGuilds[guildId]);
                            }
                        }
                    });
                }
            });
        }
    }
}
