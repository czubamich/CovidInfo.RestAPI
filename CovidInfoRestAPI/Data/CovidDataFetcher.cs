using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;

namespace CovidWPolsce_RestAPI.Data
{
    public class CovidDataFetcher
    {
        private CovidDataFetcher() { }

        private static CovidDataFetcher _instance;
        public static CovidDataFetcher Instance => _instance ?? (_instance = new CovidDataFetcher());

        public ICovidDataSource CovidDataSource { get; set; }

        public void UpdateData() { if (CovidDataSource != null) CovidDataSource.UpdateData(); else Debug.WriteLine("No Datasource attached"); }
        public void ReloadAllData() { if (CovidDataSource != null) CovidDataSource.ReloadAllData(); else Debug.WriteLine("No Datasource attached"); }

        public DateTime GetLastCountryUpdate()
        {
            using (var db = new CovidContext())
            {
                DateTime returnValue = db.CountriesHistory.Where((x) => x.CountryID == "PL").OrderByDescending((x) => x.Date).Select((x) => x.Date).FirstOrDefault();
                return returnValue;
            }
        }

        public DateTime GetLastRegionalUpdate()
        {
            using (var db = new CovidContext())
            {
                DateTime returnValue = db.RegionsHistory.Where(x => x.Regions.CountryID == "PL").OrderByDescending(x => x.Date).Select(x => x.Date).First();
                return returnValue;
            }
        }

        public bool CheckIfCountryDataOutdated() => DateTime.Now.Subtract(GetLastCountryUpdate()).Hours > 12;
        public bool CheckIfRegionalDataOutdated() => DateTime.Now.Subtract(GetLastRegionalUpdate()).Hours > 12;
    }
}