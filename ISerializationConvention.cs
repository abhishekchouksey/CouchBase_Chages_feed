using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
   public  interface ISerializationConvention
    {
        void Apply(DocumentSerializationMeta meta, ISerializationConventionWriter writer);
    }
}
