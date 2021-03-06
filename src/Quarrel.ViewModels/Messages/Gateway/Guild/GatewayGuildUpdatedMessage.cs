﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Quarrel.ViewModels.Messages.Gateway.Guild
{
    public class GatewayGuildUpdatedMessage
    {
        public GatewayGuildUpdatedMessage(DiscordAPI.Models.Guild guild)
        {
            Guild = guild;
        }

        public DiscordAPI.Models.Guild Guild { get; }
    }
}
