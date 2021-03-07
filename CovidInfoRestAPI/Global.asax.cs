using CovidWPolsce_RestAPI.Data;
using CovidWPolsce_RestAPI.Data.DataReaders;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CovidWPolsce_RestAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            CovidDataFetcher.Instance.CovidDataSource = new SpreadsheetDataSource(
                new CountryDataReader(),
                new RegionDataReader(),
                DataSourceConfig.GetDataSourcePath()
            );

            Task.Run(() => CovidDataFetcher.Instance.ReloadAllData());
            //Morning data update
            Data.TaskScheduler.Instance.ScheduleTask(11, 00, 24,
                () =>
                {
                    CovidDataFetcher.Instance.UpdateData();
                });
            //Evening data update
            Data.TaskScheduler.Instance.ScheduleTask(18, 00, 24,
                () =>
                {
                    CovidDataFetcher.Instance.UpdateData();
                });
        }
    }
}
