using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class GetChangesRequest : Request
    {

        public ChangesFeed? Feed { get; set; }
     
        public ChangesStyle? Style { get; set; }
    
        public string Since { get; set; }

        public int? Limit { get; set; }
   
        public bool? Descending { get; set; }
     
        public int? Heartbeat { get; set; }
    
        public int? Timeout { get; set; }
 
        public bool? IncludeDocs { get; set; }
    
        public string Filter { get; set; }
    }
}
