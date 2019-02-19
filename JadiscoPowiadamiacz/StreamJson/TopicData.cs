using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JadiscoPowiadamiacz
{
    public class TopicData
    {
        [JsonProperty("id")]
        public long ID { get; set; }
        [JsonProperty("text")]
        public String Text { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

    }
}
