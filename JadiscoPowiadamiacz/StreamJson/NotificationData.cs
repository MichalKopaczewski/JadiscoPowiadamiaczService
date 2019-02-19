﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JadiscoPowiadamiacz
{
    public class NotificationData
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("data")]
        public StreamData Data{ get; set; }

      
    }
}
