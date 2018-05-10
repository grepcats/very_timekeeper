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
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Create(VeryTimekeeper.Models.Task task)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            task.User = await _userManager.FindByIdAsync(userId);
            _db.Tasks.Add(task);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
