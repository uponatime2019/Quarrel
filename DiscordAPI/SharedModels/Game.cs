﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discord_UWP.SharedModels
{
    public class Game
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("timestamps")]
        public timestamps TimeStamps{get;set;}
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("details")]
        public string Details { get; set; }
        [JsonProperty("session_id")]
        public string SessionId { get; set; }
        [JsonProperty("party")]
        public party Party { get; set; }
        [JsonProperty("flags")]
        public int Flags { get; set; }
        [JsonProperty("assets")]
        public assets Assets { get; set; }
        [JsonProperty("application_id")]
        public string ApplicationId { get; set; }
    }
    public class timestamps
    {
        [JsonProperty("start")]
        public long? Start;
        [JsonProperty("end")]
        public long? End;
    }
    public class party
    {
        [JsonProperty("size")]
        public int?[] Size { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    public class assets
    {
        [JsonProperty("small_image")]
        public string SmallImage { get; set; }
        [JsonProperty("large_image")]
        public string LargeImage { get; set; }
        [JsonProperty("small_text")]
        public string SmallText { get; set; }
        [JsonProperty("large_text")]
        public string LargeText { get; set; }
    }
}