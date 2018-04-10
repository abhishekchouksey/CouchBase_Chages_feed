using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class ContinuousChangesResponseFactory : ResponseFactoryBase
    {
        protected readonly FailedResponseMaterializer FailedResponseMaterializer;

        public ContinuousChangesResponseFactory(ISerializer serializer)
        {
            Ensure.Any.IsNotNull(serializer, nameof(serializer));

            FailedResponseMaterializer = new FailedResponseMaterializer(serializer);
        }

        public virtual async Task<ContinuousChangesResponse> CreateAsync(HttpResponseMessage httpResponse)
        {
            EnsureArg.IsNotNull(httpResponse, nameof(httpResponse));

            return await MaterializeAsync<ContinuousChangesResponse>(
                httpResponse,
                null,
                FailedResponseMaterializer.MaterializeAsync).ForAwait();
        }
    }
}
