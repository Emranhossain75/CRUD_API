using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using CrudApi.Models;
using System.Web.Security;

namespace CrudApi.Controllers
{
    public class HomeController : Controller
    {
        
        CRUDEntities db = new CRUDEntities();

        public ActionResult UserRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserRegistration(Student_Information student_Information)
        {
            HttpClient Hc = new HttpClient();
            Hc.BaseAddress = new Uri("https://localhost:44352/api/StudentInformation");
            var CreateInfo = Hc.PostAsJsonAsync<Student_Information>("StudentInformation", student_Information);
            CreateInfo.Wait();
            var saveData = CreateInfo.Result;

            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("StudentInfor");
            }
            return View("Create");
        }


        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Models.AdminLogin model)
        {
            using (var context = new CRUDEntities())
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                bool Isvalid = context.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (Isvalid)
                {
                    return RedirectToAction("StudentInfor");
                }
                ModelState.AddModelError("", "Invalid UserName And Password");
                return View();
            }

        }
        public ActionResult StudentInfor()
        {
            IEnumerable<Student_Information> student = null;
            HttpClient Hc = new HttpClient();
            Hc.BaseAddress = new Uri("https://localhost:44352/api/StudentInformation");

            var consumeapi = Hc.GetAsync("StudentInformation");
            consumeapi.Wait();
            var ReadData = consumeapi.Result;
            if (ReadData.IsSuccessStatusCode)
            {
                var display = ReadData.Content.ReadAsAsync<IList<Student_Information>>();
                display.Wait();
                student = display.Result;
            }
            

            return View(student);
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Create(Student_Information student_Information)
        {
            HttpClient Hc = new HttpClient();
            Hc.BaseAddress = new Uri("https://localhost:44352/api/StudentInformation");
            var CreateInfo = Hc.PostAsJsonAsync<Student_Information>("StudentInformation", student_Information);
            CreateInfo.Wait();
            var saveData = CreateInfo.Result;

            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("StudentInfor");
            }
            return View("Create");
        }
        [HttpGet]
        public ActionResult StudentDetails(int id)
        {
            Information obj = null;
            HttpClient Hc = new HttpClient();
            Hc.BaseAddress = new Uri("https://localhost:44352/api/StudentInformation");
            var dc = Hc.GetAsync("StudentInformation?id=" + id.ToString());
            dc.Wait();
            var readData = dc.Result;
            if (readData.IsSuccessStatusCode)
            {
                var display = readData.Content.ReadAsAsync<Information>();
                display.Wait();
                obj = display.Result;
            }
            return View(obj);
        }

        public ActionResult Edit(int id)
        {
            Information obj = null;
            HttpClient Hc = new HttpClient();
            Hc.BaseAddress = new Uri("https://localhost:44352/api/StudentInformation");
            var dc = Hc.GetAsync("StudentInformation?id=" + id.ToString());
            dc.Wait();
            var readData = dc.Result;
            if (readData.IsSuccessStatusCode)
            {
                var display = readData.Content.ReadAsAsync<Information>();
                display.Wait();
                obj = display.Result;
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult Edit(Information ec)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44352/api/StudentInformation");
            var CreateInfo = hc.PutAsJsonAsync<Information>("StudentInformation", ec);
            CreateInfo.Wait();
            var saveData = CreateInfo.Result;

            if (saveData.IsSuccessStatusCode)
            {
                return RedirectToAction("StudentInfor");
            }
            else
            {
                ViewBag.message = "Not Update";
            }
            return View(ec);
        }
        public ActionResult Delete(int id)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44352/api/StudentInformation");
            var delrecord=   hc.DeleteAsync("StudentInformation/" +id.ToString());
            delrecord.Wait();
            var display = delrecord.Result;
            if (display.IsSuccessStatusCode)
            {
                return RedirectToAction("StudentInfor");
            }
            return View("StudentInfor");
        }
    }
}
