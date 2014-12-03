using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Cornball.Web.Models
{
    [DataContract(Name = "DataItem", Namespace = "http://lantisen.stodell.se/")]
    public class DataRecord
    {
        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }

        [DataMember]
        [JsonProperty("value")]
        public int Value { get; set; }

        [DataMember]
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Date { get; set; }
    }
}