using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public abstract class ConnectionInfo
    {
        public Uri Address { get; }
        public TimeSpan? Timeout { get; set; }
        public BasicAuthString BasicAuth { get; set; }
        public bool AllowAutoRedirect { get; set; } = false;
        public bool ExpectContinue { get; set; } = false;
        public bool UseProxy { get; set; } = false;

        protected ConnectionInfo(Uri address)
        {
            Ensure.Any.IsNotNull(address, nameof(address));

            Address = RemoveUserInfoFrom(address);

            if (!string.IsNullOrWhiteSpace(address.UserInfo))
            {
                var userInfoParts = ExtractUserInfoPartsFrom(address);
                BasicAuth = new BasicAuthString(userInfoParts[0], userInfoParts[1]);
            }
        }

        private Uri RemoveUserInfoFrom(Uri address)
        {
            return new Uri(address.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.UserInfo, UriFormat.UriEscaped));
        }

        private string[] ExtractUserInfoPartsFrom(Uri address)
        {
            return address.UserInfo
                .Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(Uri.UnescapeDataString)
                .ToArray();
        }
    }
}
