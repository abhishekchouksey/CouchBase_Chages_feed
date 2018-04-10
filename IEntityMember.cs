using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public interface IEntityMember
    {
        string GetPropertyNameFor(Type type);
        int? GetMemberRankingIndex(Type entityType, string membername);
        string GetValueFrom<T>(T entity) where T : class;
        void SetValueTo<T>(T entity, string value) where T : class;
    }
}
