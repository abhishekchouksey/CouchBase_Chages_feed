﻿using EnsureThat;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{
    public class SerializationConfiguration
    {
        public JsonSerializerSettings Settings { get; protected set; }
        public SerializationConventions Conventions { get; protected set; }

        public SerializationConfiguration(IContractResolver contractResolver)
        {
            EnsureArg.IsNotNull(contractResolver, nameof(contractResolver));

            Settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = contractResolver,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
                Formatting = Formatting.None,
                DefaultValueHandling = DefaultValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            };
            Settings.Converters.Add(new StringEnumConverter());
            Conventions = new SerializationConventions();
        }

        public virtual T ApplyConfigToWriter<T>(T writer) where T : JsonWriter
        {
            writer.Culture = Settings.Culture;
            writer.DateFormatHandling = Settings.DateFormatHandling;
            writer.DateFormatString = Settings.DateFormatString;
            writer.DateTimeZoneHandling = Settings.DateTimeZoneHandling;
            writer.FloatFormatHandling = Settings.FloatFormatHandling;
            writer.Formatting = Settings.Formatting;
            writer.StringEscapeHandling = Settings.StringEscapeHandling;

            return writer;
        }

        public virtual T ApplyConfigToReader<T>(T reader) where T : JsonReader
        {
            reader.Culture = Settings.Culture;
            reader.DateParseHandling = Settings.DateParseHandling;
            reader.DateTimeZoneHandling = Settings.DateTimeZoneHandling;
            reader.FloatParseHandling = Settings.FloatParseHandling;
            reader.MaxDepth = Settings.MaxDepth;

            return reader;
        }
    }
}
