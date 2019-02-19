using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JadiscoPowiadamiacz
{
    public class JadiscoData
    {
        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("viewers")]
        public int Viewers { get; set; }

        [JsonProperty("services")]
        public List<StreamService> Services { get; set; }

        [JsonProperty("online_at")]
        public DateTime OnlineAt { get; set; }
        [JsonProperty("offline_at")]
        public DateTime OfflineAt { get; set; }

    }
}
