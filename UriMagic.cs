using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public static class UriMagic
    {
        public static Uri Abracadabra(params string[] parts)
            => new Uri(string.Join(
                "/",
                parts.Select(p => p.Trim(' ', '/'))));
    }
}
