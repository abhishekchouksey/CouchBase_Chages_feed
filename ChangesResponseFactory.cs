﻿using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class ChangesResponseFactory : ResponseFactoryBase
    {
        protected readonly ChangesResponseMaterializer SuccessfulResponseMaterializer;
        protected readonly FailedResponseMaterializer FailedResponseMaterializer;

        public ChangesResponseFactory(ISerializer serializer)
        {
            Ensure.Any.IsNotNull(serializer, nameof(serializer));

            SuccessfulResponseMaterializer = new ChangesResponseMaterializer(serializer);
            FailedResponseMaterializer = new FailedResponseMaterializer(serializer);
        }

        public virtual async Task<ChangesResponse> CreateAsync(HttpResponseMessage httpResponse)
        {
            return await MaterializeAsync<ChangesResponse>(
                httpResponse,
                SuccessfulResponseMaterializer.MaterializeAsync,
                FailedResponseMaterializer.MaterializeAsync).ForAwait();
        }

        public virtual async Task<ChangesResponse<T>> CreateAsync<T>(HttpResponseMessage httpResponse)
        {
            return await MaterializeAsync<ChangesResponse<T>>(
                httpResponse,
                SuccessfulResponseMaterializer.MaterializeAsync,
                FailedResponseMaterializer.MaterializeAsync).ForAwait();
        }
    }
}
