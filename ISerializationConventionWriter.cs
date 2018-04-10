using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public interface ISerializationConventionWriter
    {
        ISerializationConventionWriter WriteName(string name);
        ISerializationConventionWriter WriteValue(string value);
    }
}
