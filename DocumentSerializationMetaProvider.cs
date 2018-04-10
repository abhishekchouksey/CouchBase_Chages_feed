using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class DocumentSerializationMetaProvider : IDocumentSerializationMetaProvider
    {
        protected const string AnonymousTypePrefix = "<>";

        protected ConcurrentDictionary<Type, DocumentSerializationMeta> Cache { get; private set; }

        public DocumentSerializationMetaProvider()
        {
            Cache = new ConcurrentDictionary<Type, DocumentSerializationMeta>();
        }

        public virtual DocumentSerializationMeta Get(Type docType)
        {
            return Cache.GetOrAdd(docType, CreateFor);
        }

        protected virtual DocumentSerializationMeta CreateFor(Type docType)
        {
            var isAnonymous = CheckIfDocTypeIsAnonymous(docType);
            var metaAttr = ExtractMetaDataAttribute(docType);
            var type = metaAttr == null ? docType.Name : metaAttr.DocType ?? docType.Name;

            var meta = new DocumentSerializationMeta(docType, type, isAnonymous);
            if (metaAttr != null)
            {
                meta.DocNamespace = metaAttr.DocNamespace;
                meta.DocVersion = metaAttr.DocVersion;
            }

            return meta;
        }

        protected virtual DocumentAttribute ExtractMetaDataAttribute(Type docType)
        {
            return docType.GetTypeInfo().GetCustomAttribute<DocumentAttribute>();
        }

        protected virtual bool CheckIfDocTypeIsAnonymous(Type docType)
        {
            var info = docType.GetTypeInfo();

            return info.IsClass &&
                info.IsNotPublic &&
                info.IsSealed &&
                info.IsGenericType &&
                info.BaseType == typeof(object) &&
                info.Namespace == null &&
                info.Name.StartsWith(AnonymousTypePrefix);
        }
    }
}
