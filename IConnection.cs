using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChangeFeeds
{

    public interface IConnection : IDisposable
    {
        Uri Address { get; }
        TimeSpan Timeout { get; }
        Action<HttpRequest> BeforeSend { set; }
        Action<HttpResponseMessage> AfterSend { set; }

        Task<HttpResponseMessage> SendAsync(HttpRequest httpRequest);
        Task<HttpResponseMessage> SendAsync(HttpRequest httpRequest, CancellationToken cancellationToken);
        Task<HttpResponseMessage> SendAsync(HttpRequest httpRequest, HttpCompletionOption completionOption);
        Task<HttpResponseMessage> SendAsync(HttpRequest httpRequest, HttpCompletionOption completionOption, CancellationToken cancellationToken);
    }
}
