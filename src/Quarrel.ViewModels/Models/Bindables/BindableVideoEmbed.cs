﻿// Copyright (c) Quarrel. All rights reserved.

using DiscordAPI.Models;
using GalaSoft.MvvmLight.Command;
using JetBrains.Annotations;
using Quarrel.ViewModels.Models.Bindables.Abstract;

namespace Quarrel.ViewModels.Models.Bindables
{
    /// <summary>
    /// A Bindable wrapper on the <see cref="Embed"/> object for videos.
    /// </summary>
    public class BindableVideoEmbed : BindableEmbed
    {
        private bool _playing;
        private RelayCommand _playCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindableVideoEmbed"/> class.
        /// </summary>
        /// <param name="model">The base <see cref="Embed"/> object for the video.</param>
        public BindableVideoEmbed([NotNull] Embed model) : base(model)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the video is playing.
        /// </summary>
        public bool Playing
        {
            get => _playing;
            set
            {
                Set(ref _playing, value);
            }
        }

        /// <summary>
        /// Gets a command that sets the video's playing status to true.
        /// </summary>
        public RelayCommand PlayCommand => _playCommand ?? (_playCommand = new RelayCommand(() =>
        {
            Playing = true;
        }));
    }
}
