using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WhereToEat.Controllers
{
    public class GroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            ViewData["Message"] = "";
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult AddUser(Guid groupId, params string[] newUsers)
        {
            return View();
        }

        public IActionResult AddUser()
        {
            return View();
        }
    }
}