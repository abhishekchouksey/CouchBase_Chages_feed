using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class BasicResponseMaterializer
    {
        public virtual Task MaterializeAsync(Response response, HttpResponseMessage httpResponse)
        {
            response.RequestUri = httpResponse.RequestMessage.RequestUri;
            response.StatusCode = httpResponse.StatusCode;
            response.RequestMethod = httpResponse.RequestMessage.Method;
            response.ContentLength = httpResponse.Content.Headers.ContentLength;
            response.ContentType = httpResponse.Content.Headers.ContentType?.ToString();
            response.ETag = httpResponse.Headers.GetETag();

            return Task.FromResult(true);
        }
    }
}
