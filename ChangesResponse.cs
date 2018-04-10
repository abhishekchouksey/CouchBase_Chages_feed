using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class ChangesResponse : ChangesResponse<string> { }

    public class ChangesResponse<TIncludedDoc> : Response
    {
        [JsonProperty(JsonScheme.LastSeq)]
        public string LastSeq { get; set; }
        public Row[] Results { get; set; }

        public class Change
        {
            public string Rev { get; set; }
        }

        public class Row
        {
            public virtual string Id { get; set; }
            public virtual string Seq { get; set; }
            public virtual Change[] Changes { get; set; }
            public virtual bool Deleted { get; set; }
            [JsonProperty(JsonScheme.IncludedDoc)]
            [JsonConverter(typeof(MultiTypeDeserializationJsonConverter))]
            public virtual TIncludedDoc IncludedDoc { get; set; }
        }
    }
}
