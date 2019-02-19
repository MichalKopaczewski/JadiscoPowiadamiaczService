using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JadiscoPowiadamiacz
{
    public class Streamer
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public String Name { get; set; }

    }
}
