using AcunMedyaAkademiWebApi.Context;
using AcunMedyaAkademiWebUI.DTOs.Product;
using AcunMedyaAkademiWebUI.Models;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System.Net.Http;

namespace AcunMedyaAkademiWebUI.Controllers
{
    public class AdminLayoutController : Controller
    {

        private readonly IHttpClientFactory _httpClientFactory;


        public AdminLayoutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult _AdminLayout()
        {
            return View();
        }

        public PartialViewResult HeadPartial()
        {
            return PartialView();
        }
        public PartialViewResult NavbarPartial()
        {
            return PartialView();
        }
        public PartialViewResult SidebarPartial()
        {
            return PartialView();
        }
        public PartialViewResult HeaderPartial()
        {
            return PartialView();
        }
        public PartialViewResult SidebarkPartial()
        {
            return PartialView();
        }
        public PartialViewResult FooterPartial()
        {
            return PartialView();
        }
        public PartialViewResult ScriptPartial()
        {
            return PartialView();
        }




        }

    }
