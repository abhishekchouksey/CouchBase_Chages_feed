using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class DbConnectionInfo : ConnectionInfo
    {
        public string DbName { get; }

        public DbConnectionInfo(string serverAddress, string dbName) : this(new Uri(serverAddress), dbName) { }
        public DbConnectionInfo(Uri serverAddress, string dbName) : base(UriMagic.Abracadabra(serverAddress.OriginalString, dbName))
        {
            Ensure.String.IsNotNullOrWhiteSpace(dbName, nameof(dbName));

            DbName = dbName.Trim(' ', '/');
        }
    }
}
