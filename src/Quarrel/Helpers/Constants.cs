﻿// Copyright (c) Quarrel. All rights reserved.

using Newtonsoft.Json;
using Quarrel.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace Quarrel.Helpers
{
    /// <summary>
    /// Contains constant values to be used through the app.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Extracts Constants from a file.
        /// </summary>
        public static class FromFile
        {
            /// <summary>
            /// Loads a random <see cref="SplashText"/> from SplashText.txt.
            /// </summary>
            /// <returns>A random <see cref="SplashText"/>.</returns>
            public static async Task<SplashText> GetRandomSplash()
            {
                // Load the list of Splash entries
                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/Data/SplashText.txt"));
                IList<string> rawSplashes = await FileIO.ReadLinesAsync(file);

                // Add date based items
                try
                {
                    string filePath = $"ms-appx:///Assets/Data/" + string.Format("Splash-{0}-{1}.txt", DateTime.Now.Month, DateTime.Now.Day);
                    file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(filePath));
                    foreach (string item in await FileIO.ReadLinesAsync(file))
                    {
                        rawSplashes.Add(item);
                    }
                }
                catch
                {
                    // Not a special day
                }

                // Select item from list
                int i = ThreadSafeRandom.Instance.Next(rawSplashes.Count);
                return new SplashText(rawSplashes[i]);
            }

            /// <summary>
            /// Gets the full unicode a Emoji list.
            /// </summary>
            /// <returns>The full unicode emoji list.</returns>
            public static async Task<IEnumerable<Emoji>> GetEmojiLists()
            {
                try
                {
                    // Read Emoji list from json file
                    var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/Data/Emojis.json"));
                    string json = await FileIO.ReadTextAsync(file);
                    return JsonConvert.DeserializeObject<IEnumerable<Emoji>>(json);
                }
                catch
                {
                    return null;
                }
            }

            /// <summary>
            /// A class containing a Splash text and credit.
            /// </summary>
            public class SplashText
            {
                /// <summary>
                /// Initializes a new instance of the <see cref="SplashText"/> class.
                /// </summary>
                /// <param name="raw">Raw Splash and credit line.</param>
                public SplashText(string raw)
                {
                    var items = raw.Split(new string[] { @"\," }, StringSplitOptions.RemoveEmptyEntries);
                    Text = items[0];
                    if (items.Count() == 2)
                    {
                        Credit = items[1];
                    }
                }

                /// <summary>
                /// Gets or sets the Splash Text.
                /// </summary>
                public string Text { get; set; } = string.Empty;

                /// <summary>
                /// Gets or sets the user credit.
                /// </summary>
                public string Credit { get; set; } = string.Empty;
            }
        }
    }
}
