using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JadiscoPowiadamiacz
{
    public class StreamService
    {
        [JsonProperty("streamer_id")]
        public int StreamerID { get; set; }

        [JsonProperty("name")]
        public String PlatformName { get; set; }

        [JsonProperty("id")]
        public String ID { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

    }
}
