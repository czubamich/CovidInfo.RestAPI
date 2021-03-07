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

namespace CovidWPolsce_RestAPI.Data.DataReaders
{
    public class SpreadsheetDataSource : ICovidDataSource
    {

        public IDataReader CountryDataReader { get; set; }
        public IDataReader RegionDataReader { get; set; }
        public string SpreadSheetSource { get; set; }

        public SpreadsheetDataSource(IDataReader countryDataReader, IDataReader regionDataReader, string spreadSheetSource)
        {
            CountryDataReader = countryDataReader;
            RegionDataReader = regionDataReader;
            SpreadSheetSource = spreadSheetSource;
        }

        public string FilePath { get; set; } = Path.Combine(Path.GetTempPath(), "covid.xlsx");

        private enum CovidTables
        {
            Growth = 1,
            RegionGrowth = 5,
            Tests = 3,
            RegionTests = 4,
        }

        public void ReloadAllData() => UpdateDataAfter(default, default);

        public void UpdateData()
        {
            var regionalDate = CovidDataFetcher.Instance.GetLastRegionalUpdate();
            var countryDate = CovidDataFetcher.Instance.GetLastCountryUpdate();

            UpdateDataAfter(countryDate, regionalDate);
        }

        public void UpdateDataAfter(DateTime countryDate, DateTime regionalDate)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Debug.Write($"[{DateTime.Now}]: Starting data update.");
            Debug.Write($"[{DateTime.Now}]: Downloading data..");
            DownloadData(SpreadSheetSource);
            Debug.WriteLine($"[{DateTime.Now}]: Data downloaded, took {watch.ElapsedMilliseconds}ms"); watch.Restart();

            var result = SpreadsheetToDataset();
            var growthTable = result.Tables[(int)CovidTables.Growth];
            var regionGrowthTable = result.Tables[(int)CovidTables.RegionGrowth];
            var testsTable = result.Tables[(int)CovidTables.Tests];
            var regionTestsTable = result.Tables[(int)CovidTables.RegionTests];
            Debug.WriteLine($"[{DateTime.Now}]: Tables processed, took {watch.ElapsedMilliseconds}ms"); watch.Restart();

            watch.Restart();
            CountryDataReader.InsertGrowth(growthTable, countryDate);
            Debug.WriteLine($"[{DateTime.Now}]: Unpacked growth table, took {watch.ElapsedMilliseconds}ms"); watch.Restart();
            CountryDataReader.InsertTests(testsTable, regionalDate);
            Debug.WriteLine($"[{DateTime.Now}]: Unpacked tests table, took {watch.ElapsedMilliseconds}ms"); watch.Restart();
            RegionDataReader.InsertGrowth(regionGrowthTable, regionalDate);
            Debug.WriteLine($"[{DateTime.Now}]: Unpacked regions growth table, took {watch.ElapsedMilliseconds}ms"); watch.Restart();
            RegionDataReader.InsertTests(regionTestsTable, regionalDate);
            Debug.WriteLine($"[{DateTime.Now}]: Unpacked regions tests table, took {watch.ElapsedMilliseconds}ms"); watch.Stop();
        }

        private void DownloadData(string spreadsheetSource)
        {
            bool ok = false;
            WebException lastEx = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(spreadsheetSource, FilePath);
                    }
                    ok = true;
                    break;
                }
                catch (WebException e)
                {
                    lastEx = e;
                    Debug.WriteLine($"[{DateTime.Now}]: failed #{i}, reason: {e.Message}");
                    Thread.Sleep(60000);
                }
            }

            if (!ok) throw lastEx;
        }

        private static DataSet SpreadsheetToDataset()
        {
            DataSet result;
            using (var fileStream = File.Open(Path.Combine(Path.GetTempPath(), "covid.xlsx"), FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(fileStream))
                {
                    result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            EmptyColumnNamePrefix = "Column",
                            UseHeaderRow = true,
                            ReadHeaderRow = (rowReader) =>
                            {
                                //Skip first row
                                rowReader.Read();
                            },
                            FilterColumn = (rowReader, columnIndex) =>
                            {
                                return true;
                            }
                        }
                    });
                }
            }
            return result;
        }
    }
}