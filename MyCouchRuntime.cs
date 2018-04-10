using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public static class MyCouchRuntime
    {
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        public static readonly CultureInfo FormatingCulture = CultureInfo.InvariantCulture;
        public static readonly string DateTimeFormatPattern = "yyyy-MM-ddTHH:mm:ss";
    }
}
