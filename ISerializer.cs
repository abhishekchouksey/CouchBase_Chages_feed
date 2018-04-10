﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangeFeeds
{

    public interface ISerializer
    {
        string Serialize<T>(T item) where T : class;
        T Deserialize<T>(string data);
        T Deserialize<T>(Stream data);
        void Populate<T>(T item, Stream data) where T : class;
        void Populate<T>(T item, string json) where T : class;

        string ToJson(object value);
        string ToJson(bool value);
        string ToJson(int value);
        string ToJson(long value);
        string ToJson(float value);
        string ToJson(double value);
        string ToJson(decimal value);
        string ToJson(DateTime value);
        string ToJsonArray<T>(IEnumerable<T> value);
    }
}
