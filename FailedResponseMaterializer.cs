﻿using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class FailedResponseMaterializer
    {
        protected readonly ISerializer Serializer;

        public FailedResponseMaterializer(ISerializer serializer)
        {
            Ensure.Any.IsNotNull(serializer, nameof(serializer));

            Serializer = serializer;
        }

        public virtual async Task MaterializeAsync(Response response, HttpResponseMessage httpResponse)
        {
            var errorAndReasonExists = !string.IsNullOrWhiteSpace(response.Error) && !string.IsNullOrWhiteSpace(response.Reason);
            if (errorAndReasonExists)
                return;

            await SetErrorAndReasonAsync(response, httpResponse).ForAwait();
        }

        protected virtual async Task SetErrorAndReasonAsync(Response response, HttpResponseMessage httpResponse)
        {
            var info = await GetInfoAsync(httpResponse);

            if (string.IsNullOrWhiteSpace(response.Error))
                response.Error = info.Error;

            if (string.IsNullOrWhiteSpace(response.Reason))
                response.Reason = info.Reason;
        }

        protected virtual async Task<FailedResponseInfo> GetInfoAsync(HttpResponseMessage httpResponse)
        {
            var info = new FailedResponseInfo();

            using (var content = await httpResponse.Content.ReadAsStreamAsync().ForAwait())
                Serializer.Populate(info, content);

            return info;
        }

        protected class FailedResponseInfo
        {
            public string Error { get; set; }
            public string Reason { get; set; }
        }
    }
}
