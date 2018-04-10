using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public abstract class ResponseFactoryBase
    {
        protected readonly BasicResponseMaterializer BasicResponseMaterializer;

        protected ResponseFactoryBase()
        {
            BasicResponseMaterializer = new BasicResponseMaterializer();
        }

        protected virtual async Task<TResponse> MaterializeAsync<TResponse>(
            HttpResponseMessage httpResponse,
            ResponseMaterializer<TResponse> sucessfulResponseMaterializer = null,
            ResponseMaterializer<TResponse> failedResponseMaterializer = null) where TResponse : Response, new()
        {
            var response = new TResponse();

            await BasicResponseMaterializer.MaterializeAsync(response, httpResponse).ForAwait();

            if (response.IsSuccess && sucessfulResponseMaterializer != null)
            {
                await sucessfulResponseMaterializer(response, httpResponse).ForAwait();
                return response;
            }

            if (!response.IsSuccess && failedResponseMaterializer != null)
                await failedResponseMaterializer(response, httpResponse).ForAwait();

            return response;
        }
    }
}
