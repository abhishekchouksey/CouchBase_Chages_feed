using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public delegate Task ResponseMaterializer<in T>(T response, HttpResponseMessage httpResponse) where T : Response;
}

