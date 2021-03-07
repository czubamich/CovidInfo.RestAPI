using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using CovidWPolsce_RestAPI;

namespace CovidWPolsce_RestAPI.Controllers
{
    public class CountryController : ApiController
    {
        private readonly CovidContext db = new CovidContext();
        const int paginationCount = 10;

        [HttpGet]
        [Route("api/country/{countryID}/news/{page}")]
        [ResponseType(typeof(CountryNews))]
        public IHttpActionResult GetCountryNews(String countryID, int page)
        {
            page = page > 0 ? page - 1 : 0;
            var countryNews = db.CountryNews.Where((x) => x.CountryId == countryID)?
                .OrderByDescending((x) => x.Timestamp)
                .Skip(page * paginationCount)
                .Take(paginationCount)
                .ToList<CountryNews>();

            if (countryNews == null || countryNews.Count == 0)
            {
                return NotFound();
            }

            return Ok(countryNews);
        }

        [HttpGet]
        [Route("api/country/{countryID}/news/pages")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetCountryNewsPagination(String countryID)
        {
            var newsCount = db.CountryNews.Where((x) => x.CountryId == countryID)?.Count() / paginationCount + 1;

            return Ok(newsCount);
        }

        [HttpGet]
        [Route("api/country/{countryID}")]
        [ResponseType(typeof(CountriesHistory))]
        public IHttpActionResult GetCountryHistory(String countryID)
        {
            var countriesHistory = db.CountriesHistory.Where((x) => x.CountryID == countryID)?.ToList<CountriesHistory>();
            if (countriesHistory == null || countriesHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(countriesHistory);
        }

        [HttpGet]
        [Route("api/country/{countryID}/day/{date}")]
        [ResponseType(typeof(CountriesHistory))]
        public IHttpActionResult GetCountryHistoryOnDay(String countryID, DateTime date)
        {
            date = date.Date;
            var countriesHistory = db.CountriesHistory.Where((x) => x.CountryID == countryID && x.Date == date)?.ToList<CountriesHistory>();
            if (countriesHistory == null || countriesHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(countriesHistory);
        }

        [HttpGet]
        [Route("api/country/{countryID}/today")]
        [ResponseType(typeof(CountriesHistory))]
        public IHttpActionResult GetCountryHistoryOnDay(String countryID)
        {
            var countriesHistory = db.CountriesHistory.OrderByDescending((x) => x.Date)?.Take(1).ToList<CountriesHistory>(); ;
            if (countriesHistory == null || countriesHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(countriesHistory);
        }

        [HttpGet]
        [Route("api/country/{countryId}/since/{dateSince}")]
        [Route("api/country/{countryId}/{dateSince}")]
        [ResponseType(typeof(CountriesHistory))]
        public IHttpActionResult GetCountryHistorySince(String countryId, DateTime dateSince)
        {
            var countriesHistory = db.CountriesHistory.Where((x) => x.CountryID == countryId && x.Date.CompareTo(dateSince) >= 0)?.ToList<CountriesHistory>();
            if (countriesHistory == null || countriesHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(countriesHistory);
        }

        [HttpGet]
        [Route("api/country/list")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountryList()
        {
            return Ok(db.Countries);
        }

        // GET: api/Countries/PL
        [HttpGet]
        [Route("api/country/list/{countryId}")]
        [ResponseType(typeof(Country))]
        public IHttpActionResult GetCountry(String countryId)
        {
            var country = db.Countries.Find(countryId);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}