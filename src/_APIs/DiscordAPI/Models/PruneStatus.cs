﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordAPI.Models
{
    public class PruneStatus
    {
        [JsonProperty("pruned")]
        public int PrunedCount { get; set; }
    }
}
