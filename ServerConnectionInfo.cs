using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class ServerConnectionInfo : ConnectionInfo
    {
        public ServerConnectionInfo(string serverAddress) : this(new Uri(serverAddress)) { }
        public ServerConnectionInfo(Uri serverAddress) : base(serverAddress) { }
    }
}
