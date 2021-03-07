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
    public class RegionController : ApiController
    {
        private readonly CovidContext db = new CovidContext();

        [HttpGet]
        [Route("api/region/{countryId}/summary")]
        [Route("api/region/{countryId}/today")]
        [ResponseType(typeof(RegionsHistory))]
        public IHttpActionResult GetRegionsSummary(String countryId)
        {
            if (db.Countries.Find(countryId) == null)
            {
                return BadRequest();
            }

            var newestDate = db.RegionsHistory.OrderByDescending(x => x.Date).First().Date;
            var regionsHistory = db.RegionsHistory.Where(x => x.Date == newestDate).ToList<RegionsHistory>();
            if (regionsHistory == null || regionsHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(regionsHistory);
        }

        [HttpGet]
        [Route("api/region/{countryId}/{regionID}")]
        [ResponseType(typeof(RegionsHistory))]
        public IHttpActionResult GetRegionsHistory(String countryId, String regionId)
        {
            if (db.Countries.Find(countryId)==null)
            {
                return BadRequest();
            }

            var regionsHistory = db.RegionsHistory.Where((x) => x.RegionID == regionId)?.ToList<RegionsHistory>();
            if (regionsHistory == null || regionsHistory.Count==0)
            {
                return BadRequest();
            }

            return Ok(regionsHistory);
        }

        [HttpGet]
        [Route("api/region/{countryId}/{regionID}/day/{date}")]
        [ResponseType(typeof(RegionsHistory))]
        public IHttpActionResult GetRegionHistoryOnDay(String countryId, String regionId, DateTime date)
        {
            if (db.Countries.Find(countryId) == null)
            {
                return BadRequest();
            }

            var regionsHistory = db.RegionsHistory.Where((x) => x.RegionID == regionId && x.Date == date)?.ToList<RegionsHistory>();
            if (regionsHistory == null || regionsHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(regionsHistory);
        }

        [HttpGet]
        [Route("api/region/{countryID}/{regionID}/today")]
        [ResponseType(typeof(RegionsHistory))]
        public IHttpActionResult GetCountryHistoryOnDay(String countryId, String regionId)
        {
            var regionsHistory = db.RegionsHistory.Where((x) => x.RegionID == regionId).OrderByDescending((x) => x.Date)?.Take(1).ToList<RegionsHistory>(); ;
            if (regionsHistory == null || regionsHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(regionsHistory);
        }

        [HttpGet]
        [Route("api/region/{countryId}/{regionID}/since/{dateSince}")]
        [ResponseType(typeof(RegionsHistory))]
        public IHttpActionResult GetRegionHistorySince(String countryId, String regionId, DateTime dateSince)
        {
            if (db.Countries.Find(countryId) == null)
            {
                return BadRequest();
            }

            var regionsHistory = db.RegionsHistory.Where((x) => x.RegionID == regionId && x.Date.CompareTo(dateSince)>=0)?.ToList<RegionsHistory>();
            if (regionsHistory == null || regionsHistory.Count == 0)
            {
                return NotFound();
            }

            return Ok(regionsHistory);
        }


        [ResponseType(typeof(Region))]
        [Route("api/region/{countryID}/list")]
        public IHttpActionResult GetCountryRegions(String countryId)
        {
            if (db.Countries.Find(countryId) == null)
            {
                return BadRequest();
            }

            var regions = db.Regions.Where((x) => x.CountryID == countryId);
            if (regions == null || regions.Count() == 0)
            {
                return NotFound();
            }

            return Ok(regions);
        }

        [HttpGet]
        [Route("api/region/{countryID}/list/{regionID}")]
        [ResponseType(typeof(Region))]
        public IHttpActionResult GetCountryRegions(String countryId, String regionId)
        {
            if (db.Countries.Find(countryId) == null)
            {
                return BadRequest();
            }

            var regions = db.Regions.Find(regionId);
            if (regions == null)
            {
                return NotFound();
            }

            return Ok(regions);
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