using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public abstract class ApiContextBase<TConnection> where TConnection : class, IConnection
    {
        protected TConnection Connection { get; }

        protected ApiContextBase(TConnection connection)
        {
            Ensure.Any.IsNotNull(connection, nameof(connection));

            Connection = connection;
        }

        protected virtual Task<HttpResponseMessage> SendAsync(HttpRequest httpRequest)
        {
            return Connection.SendAsync(httpRequest);
        }

        protected virtual Task<HttpResponseMessage> SendAsync(HttpRequest httpRequest, CancellationToken cancellationToken)
        {
            return Connection.SendAsync(httpRequest, cancellationToken);
        }

        protected virtual Task<HttpResponseMessage> SendAsync(HttpRequest httpRequest, HttpCompletionOption completionOption)
        {
            return Connection.SendAsync(httpRequest, completionOption);
        }

        protected virtual Task<HttpResponseMessage> SendAsync(HttpRequest httpRequest, HttpCompletionOption completionOption, CancellationToken cancellationToken)
        {
            return Connection.SendAsync(httpRequest, completionOption, cancellationToken);
        }
    }
}
