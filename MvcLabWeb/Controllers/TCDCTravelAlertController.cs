using MvcLabWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MvcLabWeb.Controllers
{
    public class TCDCTravelAlertController : Controller
    {
        // GET: TCDCTravelAlert
        public async System.Threading.Tasks.Task<ActionResult> Index()
        {
            string targetURI = "http://data.gov.tw/iisi/logaccess/5040?dataUrl=http://www.cdc.gov.tw/ExportOpenData.aspx?Type=json&FromWeb=2&ndctype=JSON&ndcnid=10567";
            HttpClient client = new HttpClient();
            client.MaxResponseContentBufferSize = Int32.MaxValue;
            var response = await client.GetStringAsync(targetURI);
            var collection = JsonConvert.DeserializeObject<IEnumerable<TravelAlert>>(response);

            //ViewBag.Result = response;
            return View(collection);
        }
    }
}