using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using TwitterMonitor.Models;
using TwitterMonitor.TwitterService;

namespace TwitterMonitor.Controllers
{
    public class GetDataController : Controller
    {
        //
        // GET: /GetData/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendRequest(RequestSpec data)
        {
            if (ModelState.IsValid)
            {
                RequestInfo info = new RequestInfo();
                TwitterServiceClient client = new TwitterServiceClient();
                try
                {
                    client.Open();
                    info.NumberTweets = data.NumberTweets;
                    info.TrackWord = data.TrackWord;
                    info = client.InitStreamAndSaveData(info);
                    client.Close();
                }
                catch (FaultException<StreamFaultInfo> ex) //?streamFaultInfo???
                {
                    client.Abort();
                    data.ExceptionMessage = ex.Message;
                    return View("Index", data);
                }
                catch (TimeoutException)
                {
                    client.Abort();
                    data.ExceptionMessage = "Service did not responded.";
                    return View("Index", data);
                }
                catch (Exception)
                {
                    client.Abort();
                    data.ExceptionMessage = "Unexpected problems occurerd while communicating with service.";
                    return View("Index", data);
                }
                               
                data.Code = info.Code;
                return View("Success", data);
            }
            return View("Index",data);
        }

        public ActionResult Success(RequestSpec data)
        {
            return View(data);
        }
	}
}