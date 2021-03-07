using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidWPolsce_RestAPI.Data
{
    static class ExtensionMethods
    {
        public static int? ToNullableInt(this string s)
        {
            int i;
            if (int.TryParse(s, out i)) return i;
            return null;
        }
    }
}