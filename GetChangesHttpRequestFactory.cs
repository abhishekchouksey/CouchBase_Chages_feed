using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class GetChangesHttpRequestFactory : GetChangesHttpRequestFactoryBase
    {
        public override HttpRequest Create(GetChangesRequest request)
        {
            Ensure.Any.IsNotNull(request, nameof(request));

            EnsureNonContinuousFeedIsRequested(request);

            return base.Create(request);
        }

        protected virtual void EnsureNonContinuousFeedIsRequested(GetChangesRequest request)
        {
            if (request.Feed.HasValue && request.Feed == ChangesFeed.Continuous)
                throw new ArgumentException(ExceptionStrings.GetChangesForNonContinuousFeedOnly, nameof(request));
        }
    }
}
