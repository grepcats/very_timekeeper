using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VeryTimekeeper.ViewModels;
using VeryTimekeeper.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace VeryTimekeeper.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TasksController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            List<Models.Task> model = _db.Tasks.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Models.Task task)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            task.User = await _userManager.FindByIdAsync(userId);
            int hr = 0;
            int min = 0;
            int sec = 0;
            if (String.IsNullOrEmpty(Request.Form["hours"]))
            {
                hr = 0;
            }
            else
            {
                hr = Int32.Parse(Request.Form["hours"]);
            }

            if (String.IsNullOrEmpty(Request.Form["minutes"]))
            {
                min = 0;
            }
            else
            {
                min = Int32.Parse(Request.Form["minutes"]);
            }

            if (String.IsNullOrEmpty(Request.Form["seconds"]))
            {
                sec = 0;
            }
            else
            {
                sec = Int32.Parse(Request.Form["seconds"]);
            }

        
            task.timeRemaining = (hr * 3600) + (min * 60) + sec;
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
