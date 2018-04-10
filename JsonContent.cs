using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class JsonContent : StringContent
    {
        public JsonContent()
            : base(string.Empty, MyCouchRuntime.DefaultEncoding, HttpContentTypes.Json) { }

        public JsonContent(string content)
            : base(content, MyCouchRuntime.DefaultEncoding, HttpContentTypes.Json) { }
    }
}
