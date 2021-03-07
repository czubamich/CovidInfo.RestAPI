using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CovidWPolsce_RestAPI
{
    public class DataSourceConfig
    {
        public static String GetDataSourcePath()
        {
            //Google Spreadsheet Csv
            return "https://docs.google.com/spreadsheets/d/1ierEhD6gcq51HAm433knjnVwey4ZE5DCnu1bW7PRG3E/export?format=xlsx";
        }
    }
}