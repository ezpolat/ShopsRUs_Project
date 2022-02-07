using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ShopsRUs_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {   //model doldurma işlemi
            ShopsRUsModel model = GetModel();
            return RedirectToAction("RequestApi", model);
        }
        public IActionResult RequestApi(ShopsRUsModel model)
        {
            HttpClient httpClient = new HttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("https://localhost:44343/api/Home", content).Result;
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var ShopsRUsMdl = JsonConvert.DeserializeObject<ShopsRUsModel>(responseMessage.Content.ReadAsStringAsync().Result);
                return View(ShopsRUsMdl);
            }
       
            return View();

        }
        //Modele değer verebilirsiniz
        public ShopsRUsModel GetModel()
        {
            ShopsRUsModel model = new ShopsRUsModel();
            model.Price = 200;
            model.IsStoreEmployee = false;
            model.IsGroseries = false;
            return model;
        }

    }
}
