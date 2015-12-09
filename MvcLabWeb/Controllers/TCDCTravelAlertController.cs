using MvcLabWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;
using System.Threading.Tasks;
using PagedList;

namespace MvcLabWeb.Controllers
{
    public class TCDCTravelAlertController : Controller
    {
        private int pageSize = 5;
        // GET: TCDCTravelAlert
        //public async Task<ActionResult> Index()
        //{
        //    var travelAlertSource = await this.GetTravelAlertData();
        //    ViewData.Model = travelAlertSource;
        //    return View();
        //}

        private async Task<IEnumerable<TravelAlert>> GetTravelAlertData()
        {
            string cacheName = "TRAVEL_ALERT";
            
            ObjectCache cache = MemoryCache.Default;
            CacheItem cacheContents = cache.GetCacheItem(cacheName);

            if(cacheContents == null)
            {
                return await RetriveTravelAlertData(cacheName);
            }
            else
            {
                return cacheContents.Value as IEnumerable<TravelAlert>;
            }
        }

        private async Task<IEnumerable<TravelAlert>> RetriveTravelAlertData(string cacheName)
        {
            string targetURI = "http://data.gov.tw/iisi/logaccess/5040?dataUrl=http://www.cdc.gov.tw/ExportOpenData.aspx?Type=json&FromWeb=2&ndctype=JSON&ndcnid=10567";
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURI);
            var collection = JsonConvert.DeserializeObject<IEnumerable<TravelAlert>>(response);

            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

            ObjectCache cacheItem = MemoryCache.Default;
            cacheItem.Add(cacheName, collection, policy);
            collection = collection.OrderBy(x => x.headline); //
            return collection;
        }

        private async Task<List<string>> GetSecurityLevels()
        {
            var source = await this.GetTravelAlertData();
            if (source != null)
            {
                var securityLevel = source.OrderBy(x => x.severity_level)
                                            .Select(x => x.severity_level)
                                            .Distinct();
                return securityLevel.ToList();
            }
            return new List<string>();
        }

        private async Task<IEnumerable<SelectListItem>> SecurityLevelSelectList(string securityLevels)
        {
            var securitySource = await this.GetSecurityLevels();
            var securityLevelList = securitySource.Select(item => new SelectListItem()
            {
                Text = item,
                Value = item,
                Selected = !string.IsNullOrWhiteSpace(securityLevels) 
                        && item.Equals(securityLevels, StringComparison.OrdinalIgnoreCase)
            });
            return securityLevelList;
        }

        //public async Task<ActionResult> Index(string securityLevels)
        //{
        //    ViewBag.SecurityLevels = await this.SecurityLevelSelectList(securityLevels);
        //    ViewBag.SelectedSecurityLevel = securityLevels;

        //    var source = await this.GetTravelAlertData();
        //    source = source.AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(securityLevels))
        //    {
        //        source = source.Where(x => x.severity_level == securityLevels);
        //    }
        //    return View(source.OrderBy(x => x.severity_level).ToList());
        //}

        public async Task<ActionResult> Index(string securityLevels, int page = 1)
        {
            ViewBag.SecurityLevels = await this.SecurityLevelSelectList(securityLevels);
            ViewBag.SelectedSecurityLevel = securityLevels;
            int currentPage = page < 1 ? 1 : page;
            var source = await this.GetTravelAlertData();

            source = source.AsQueryable();

            if (!string.IsNullOrWhiteSpace(securityLevels))
            {
                source = source.Where(x => x.severity_level == securityLevels);
            }
            return View(source.OrderByDescending(x => x.severity_level).ToPagedList(currentPage, pageSize));
        
        
        }
    }
}