using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JadiscoPowiadamiacz
{
    public class StreamData
    {
        [JsonProperty("streamers")]
        public List<Streamer> Streamers { get; set; }

        [JsonProperty("stream")]
        public JadiscoData Stream { get; set; }

        [JsonProperty("topic")]
        public TopicData Topic { get; set; }
    }
}
