using System;
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
            var lastTask = new Models.Task();
            for (int i = 0; i < fullTaskIds.Count; i++)
            {
                int newTaskId = Int32.Parse(fullTaskIds[i].Remove(0, 11));

                var thisTask = _db.Tasks.FirstOrDefault(Tasks => Tasks.TaskId == newTaskId);

                if (i == 0)
                {
                    thisTask.timeToFinish = DateTime.Now.AddSeconds(thisTask.timeRemaining);
                    lastTask = thisTask;
                    _db.Entry(thisTask).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                else
                {
                    thisTask.timeToFinish = lastTask.timeToFinish.AddSeconds(lastTask.timeRemaining);
                    lastTask = thisTask;
                    _db.Entry(thisTask).State = EntityState.Modified;
                    _db.SaveChanges();
                }


            }
            List<Models.Task> model = _db.Tasks.Where(x => x.timeRemaining != 0).OrderBy(x => x.timeToFinish).ToList();
            return PartialView(model);
        }

        public IActionResult ListFinishedTasks()
        {
            List<Models.Task> model = _db.Tasks.Where(x => x.timeRemaining == 0).OrderBy(x => x.timeToFinish).ToList();

            return PartialView(model);
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

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var thisTask = _db.Tasks.FirstOrDefault(Tasks => Tasks.TaskId == id);
            return View(thisTask);
        }

        [HttpPost]
        public IActionResult Update(Models.Task task)
        {
            _db.Entry(task).State = EntityState.Modified;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var thisTask = _db.Tasks.FirstOrDefault(Tasks => Tasks.TaskId == id);
            return View(thisTask);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisTask = _db.Tasks.FirstOrDefault(Tasks => Tasks.TaskId == id);
            _db.Tasks.Remove(thisTask);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
