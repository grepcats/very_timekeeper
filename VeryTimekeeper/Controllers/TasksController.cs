﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VeryTimekeeper.ViewModels;
using VeryTimekeeper.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

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
            ModelState.Clear();
            List<Models.Task> model = _db.Tasks.OrderBy(x => x.timeToFinish).ToList();
            ModelState.Clear();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ListTasks(string taskIds)
        {
            List<string> fullTaskIds = taskIds.Split(',').ToList();
            List<int> intIds = new List<int>();
            foreach (string task in fullTaskIds)
            {
                string newTask = task.Remove(0, 5);
                intIds.Add(Int32.Parse(newTask));
            }
            return PartialView();
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

        [HttpPost]
        public IActionResult UpdateTaskTime(int incomingId, string incomingContent, int incomingTimeRemaining)
        {
            var thisTask = _db.Tasks.FirstOrDefault(Tasks => Tasks.TaskId == incomingId);
            thisTask.timeRemaining = incomingTimeRemaining;
            thisTask.timeToFinish = DateTime.Now;
            _db.Entry(thisTask).State = EntityState.Modified;
            _db.SaveChanges();
            
            //List<Models.Task> model = _db.Tasks.ToList();
            //if (model.Count > 0)
            //{
            //    Models.Task first = model[0];
            //    model.RemoveAt(0);
            //    model.Insert((model.Count), first);
            //   // model.Insert(0, new Models.Task { Content = "hello guys", TaskId = 3, timeRemaining = 33 });
            //}

            List<Models.Task> model = _db.Tasks.OrderBy(x => x.timeToFinish).ToList();


            return View("Index", model);
        }
    }
}
