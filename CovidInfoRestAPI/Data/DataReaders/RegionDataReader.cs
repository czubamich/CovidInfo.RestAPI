using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CovidWPolsce_RestAPI.Data.DataReaders
{
    public class RegionDataReader : IDataReader
    {
        static readonly Dictionary<String, int> regions = new Dictionary<String, int>()
        {
            {"SK", 1},
            {"MZ", 2},
            {"MA", 3},
            {"WP", 4},
            {"DS", 5},
            {"LU", 6},
            {"PM", 7},
            {"KP", 8},
            {"PD", 9},
            {"LB", 10},
            {"ZP", 11},
            {"SL", 12},
            {"WN", 13},
            {"OP", 14},
            {"PK", 15},
            {"LD", 16}
        };

        public enum Outset
        {
            NewCases = 6,
            TotalCases = 29,
            NewDeaths = 49,
            TotalDeaths = 69,
            NewRecoveries = 89,
            TotalRecoveries = 109,
            ActiveCases = 129,
            TotalTests = 1,
            NewTests = 21
        }

        public void InsertGrowth(DataTable regionGrowthTable, DateTime since = default)
        {
            using (var db = new CovidContext())
            {
                regionGrowthTable = Utils.PivotTable(regionGrowthTable);

                foreach (DataRow row in regionGrowthTable.Rows.Cast<DataRow>().Skip(1))
                {
                    Func<Outset, int, string> readColumn = (x, y) => row[(int)x + y].ToString();
                    DateTime date;
                    if (!DateTime.TryParse(readColumn(Outset.NewCases, 0), out date))
                        break;
                    if (since.CompareTo(date) > 0)
                        continue;


                    foreach (var region in regions)
                    {
                        var n = new RegionsHistory()
                        {
                            RegionID = region.Key,
                            Timestamp = DateTime.Now,
                            Date = date,
                            NewCases = readColumn(Outset.NewCases, region.Value).ToNullableInt(),
                            TotalCases = readColumn(Outset.TotalCases, region.Value).ToNullableInt(),
                            NewDeaths = readColumn(Outset.NewDeaths, region.Value).ToNullableInt(),
                            TotalDeaths = readColumn(Outset.TotalDeaths, region.Value).ToNullableInt(),
                            NewRecoveries = readColumn(Outset.NewRecoveries, region.Value).ToNullableInt(),
                            TotalRecoveries = readColumn(Outset.TotalRecoveries, region.Value).ToNullableInt(),
                            ActiveCases = readColumn(Outset.ActiveCases, region.Value).ToNullableInt()
                        };

                        var e = db.RegionsHistory.Find(n.Date, n.RegionID);
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
                            db.RegionsHistory.Add(n);
                        }
                    }
                    db.SaveChanges();
                }
            }
        }

        public void InsertTests(DataTable regionTestTable, DateTime since = default)
        {
            using (var db = new CovidContext())
            {
                regionTestTable = Utils.PivotTable(regionTestTable);

                foreach (DataRow row in regionTestTable.Rows.Cast<DataRow>().Skip(1))
                {
                    Func<Outset, int, string> readColumn = (x, y) => row[(int)x + y].ToString();
                    DateTime date;
                    if (!DateTime.TryParse(readColumn(Outset.TotalTests, 0), out date))
                        break;
                    if (since.CompareTo(date.AddDays(7)) > 0)
                        continue;

                    foreach (var region in regions)
                    {
                        var n = new RegionsHistory()
                        {
                            RegionID = region.Key,
                            Timestamp = DateTime.Now,
                            Date = date,
                            NewTests = readColumn(Outset.NewTests, region.Value).ToNullableInt(),
                            TotalTests = readColumn(Outset.TotalTests, region.Value).ToNullableInt()
                        };

                        var e = db.RegionsHistory.Find(n.Date, n.RegionID);
                        if (e != null)
                        {
                            e.NewTests = n.NewTests;
                            e.TotalTests = n.TotalTests;
                            e.Timestamp = n.Timestamp;
                        }
                        else
                        {
                            db.RegionsHistory.Add(n);
                        }
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}