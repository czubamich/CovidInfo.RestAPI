namespace CovidWPolsce_RestAPI.Data
{
    public interface ICovidDataSource
    {
        void UpdateData();
        void ReloadAllData();
    }
}