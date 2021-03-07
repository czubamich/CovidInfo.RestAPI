using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CovidWPolsce_RestAPI.Data.DataReaders
{
    public class CountryDataReader : IDataReader
    {
        public enum Offset
        {
            NewCases = 1,
            TotalCases = 11,
            NewDeaths = 7,
            TotalDeaths = 12,
            NewRecoveries = 8,
            TotalRecoveries = 13,
            ActiveCases = 15,
        }

        public void InsertGrowth(System.Data.DataTable table, DateTime since = default)
        {
            using (var db = new CovidContext())
            {
                foreach (DataRow row in table.Rows)
                {
                    DateTime date;
                    if (!DateTime.TryParse(row[0].ToString(), out date))
                        break;
                    if (since.CompareTo(date) > 0)
                        continue;

                    var n = new CountriesHistory()
                    {
                        CountryID = "PL",
                        Timestamp = DateTime.Now,
                        Date = date,
                        NewCases = row[(int)Offset.NewCases].ToString().ToNullableInt(),
                        TotalCases = row[(int)Offset.TotalCases].ToString().ToNullableInt(),
                        NewDeaths = row[(int)Offset.NewDeaths].ToString().ToNullableInt(),
                        TotalDeaths = row[(int)Offset.TotalDeaths].ToString().ToNullableInt(),
                        NewRecoveries = row[(int)Offset.NewRecoveries].ToString().ToNullableInt(),
                        TotalRecoveries = row[(int)Offset.TotalRecoveries].ToString().ToNullableInt(),
                        ActiveCases = row[(int)Offset.ActiveCases].ToString().ToNullableInt()
                    };

                    var e = db.CountriesHistory.Find(n.Date, n.CountryID);
                    if (e != null)
                    {
                        e.NewCases = n.NewCases;
                        e.TotalCases = n.TotalCases;
                        e.NewDeaths = n.NewDeaths;
                        e.TotalDeaths = n.TotalDeaths;
                        e.NewRecoveries = n.NewRecoveries;
                        e.TotalRecoveries = n.TotalRecoveries;
                        e.ActiveCases = n.ActiveCases;
                        e.Timestamp = n.Timestamp;
                    }
                    else
                    {
                        db.CountriesHistory.Add(n);
                    }
                }
                db.SaveChanges();
            }
        }

        public void InsertTests(System.Data.DataTable table, DateTime since = default)
        {
            using (var db = new CovidContext())
            {
                foreach (DataRow row in table.Rows)
                {
                    DateTime date;
                    if (!DateTime.TryParse(row[1].ToString(), out date))
                        break;
                    date = date.AddDays(1);
                    if (since.CompareTo(date) > 0)
                        continue;

                    var newVal = new CountriesHistory()
                    {
                        CountryID = "PL",
                        Timestamp = DateTime.Now,
                        Date = date,
                        NewTests = row[5].ToString().ToNullableInt(),
                        TotalTests = row[4].ToString().ToNullableInt(),
                    };

                    var e = db.CountriesHistory.Find(newVal.Date, newVal.CountryID);
                    if (e != null)
                    {
                        e.Timestamp = newVal.Timestamp;
                        e.NewTests = newVal.NewTests;
                        e.TotalTests = newVal.TotalTests;
                    }
                    else
                    {
                        db.CountriesHistory.Add(newVal);
                    }
                }
                db.SaveChanges();
            }
        }
    }
}