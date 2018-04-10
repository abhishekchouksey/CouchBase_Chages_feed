﻿using EnsureThat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class StringSerializationConvention : ISerializationConvention
    {
        protected readonly string PropertyName;
        protected readonly Func<DocumentSerializationMeta, string> Convention;

        public StringSerializationConvention(string propertyName, Func<DocumentSerializationMeta, string> convention)
        {
            EnsureArg.IsNotNullOrWhiteSpace(propertyName, nameof(propertyName));
            EnsureArg.IsNotNull(convention, nameof(convention));

            PropertyName = propertyName;
            Convention = convention;
        }

        public virtual void Apply(DocumentSerializationMeta meta, ISerializationConventionWriter writer)
        {
            WriteIfValueExists(Convention(meta), writer);
        }

        protected virtual void WriteIfValueExists(string value, ISerializationConventionWriter writer)
        {
            if (value == null)
                return;

            writer
                .WriteName(PropertyName)
                .WriteValue(value);
        }
    }
}
