using System;
using System.Data;

namespace CovidWPolsce_RestAPI.Data
{
    public interface IDataReader
    {
        void InsertGrowth(DataTable table, DateTime since = default);
        void InsertTests(DataTable table, DateTime since = default);
    }
}