using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class DbConnection : Connection, IDbConnection
    {
        public string DbName { get; }

        public DbConnection(DbConnectionInfo connectionInfo) : base(connectionInfo)
        {
            DbName = connectionInfo.DbName;
        }
    }
}
