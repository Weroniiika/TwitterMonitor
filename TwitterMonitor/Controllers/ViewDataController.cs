using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using TwitterMonitor.Models;
using TwitterMonitor.TwitterService;

namespace TwitterMonitor.Controllers
{
    public class ViewDataController : Controller
    {
        //
        // GET: /ViewData/
        public ActionResult Index(string code = null)
        {
            var button = new PresentationSpec();
            AddListOfPresentations(button);
            if (!(code==null))
                button.CODE = code;
            return View(button);
        }

        private void AddListOfPresentations(PresentationSpec ps) {

            ps.Presentations.Add(new PresentationType { Id = 1, Text = "Hashtags" });
            ps.Presentations.Add(new PresentationType { Id = 2, Text = "Sentiment" });
            ps.Presentations.Add(new PresentationType { Id = 3, Text = "Pictures" });
        }

        [HttpPost]
        public ActionResult Show(PresentationSpec data)
        {
            if (ModelState.IsValid)
            {
                if (data.selectedPresentationId == 1)
                    return RedirectToAction(actionName: "Hashtags", routeValues: data);
                else if (data.selectedPresentationId == 2)
                    return RedirectToAction(actionName: "Sentiment", routeValues: data);
                else if (data.selectedPresentationId == 3)
                    return RedirectToAction(actionName: "Pictures", routeValues: data);
                else
                {
                    data.ExceptionMessage = "Data presentation types are not correct.";
                    AddListOfPresentations(data);
                    return View("Index", data);
                }
            }
            else
            {
                AddListOfPresentations(data);
                return View("Index", data);
            }
        }

         public ActionResult Hashtags(PresentationSpec data)
        {
            string code = data.CODE;
            if (code == null)
                return RedirectToAction("Index");
            HashtagsCountData hashData = null;
            TwitterServiceClient client = new TwitterServiceClient();
            try
            {
                client.Open();
                hashData = client.HashtagsFrequencyData(code);
                client.Close();
            }
            catch (FaultException<string> ex)
            {
                client.Abort();
                data.CODE = ex.Detail;
                AddListOfPresentations(data);
                data.ExceptionMessage = ex.Message;
                return View("Index", data);
            }
            catch (TimeoutException)
            {
                client.Abort();
                AddListOfPresentations(data);
                data.ExceptionMessage = "Service did not responded. You can try again.";
                return View("Index", data);
            }
            catch (Exception)
            {
                client.Abort();
                AddListOfPresentations(data);
                data.ExceptionMessage = "Unexpected problems occurerd while communicating with service.";
                return View("Index", data);
            }
            return View(hashData);
        }

        public ActionResult Sentiment(PresentationSpec data)
        {
            string code = data.CODE;
            if (code == null)
                return RedirectToAction("Index");
            SentimentData sentimentData = null;
            TwitterServiceClient client = new TwitterServiceClient();
            try
            {
                client.Open();
                sentimentData = client.Sentiment(code);
                client.Close();
            }
            catch (FaultException<string> ex)
            {
                client.Abort();
                data.CODE = ex.Detail;
                AddListOfPresentations(data);
                data.ExceptionMessage = ex.Message;
                return View("Index", data);
            }
            catch (TimeoutException)
            {
                client.Abort();
                AddListOfPresentations(data);
                data.ExceptionMessage = "Service did not responded. You can try again.";
                return View("Index", data);
            }
            catch (Exception)
            {
                client.Abort();
                AddListOfPresentations(data);
                data.ExceptionMessage = "Service did not responded. You can try again.";
                return View("Index", data);
            }

            return View(sentimentData);
        }

        public ActionResult Pictures(PresentationSpec data)
        {
            string code = data.CODE;
            if (code == null)
                return RedirectToAction("Index");

            PicturesData pictures = null;
            TwitterServiceClient client = new TwitterServiceClient();
            try
            {
                client.Open();
                pictures = client.Pictures(code);
                client.Close();
            }
            catch (FaultException<string> ex)
            {
                client.Abort();
                data.CODE = ex.Detail;
                AddListOfPresentations(data);
                data.ExceptionMessage = ex.Message;
                return View("Index", data);
            }
            catch (TimeoutException)
            {
                client.Abort();
                AddListOfPresentations(data);
                data.ExceptionMessage = "Service did not responded. You can try again.";
                return View("Index", data);
            }
            catch (Exception)
            {
                client.Abort();
                AddListOfPresentations(data);
                data.ExceptionMessage = "Service did not responded. You can try again.";
                return View("Index", data);
            }
            return View(pictures);
        }
	}
}