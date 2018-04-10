using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public enum ChangesFeed
    {
        Normal,
        Longpoll,
        Continuous
    }

    public static class ChangesFeedEnumExtensions
    {
        private static readonly Dictionary<ChangesFeed, string> Mappings;

        static ChangesFeedEnumExtensions()
        {
            Mappings = new Dictionary<ChangesFeed, string> {
                { ChangesFeed.Normal, "normal" },
                { ChangesFeed.Longpoll, "longpoll" },
                { ChangesFeed.Continuous, "continuous" }
            };
        }

        public static string AsString(this ChangesFeed feed)
        {
            return Mappings[feed];
        }
    }
}

