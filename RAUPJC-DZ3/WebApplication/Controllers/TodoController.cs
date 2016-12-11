using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Interfaces;
using Microsoft.AspNetCore.Identity;
using WebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using WebApplication.Models.TodoViewModel;

namespace WebApplication.Controllers
{
    public class TodoController : Controller
    {


        private readonly ITodoRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        // Inject user manager into constructor
        public TodoController(ITodoRepository repository,
        UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser currentUser = await
            _userManager.GetUserAsync(HttpContext.User);
            if (currentUser == null)
            {
                ViewData["logged"] = "F";
                return View();
            }
            else
            {
                ViewData["logged"] = "T";
                return View(_repository.GetActive(new Guid(currentUser.Id)));
            }
        }


        [Authorize]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddTodoViewModel m)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = await
           _userManager.GetUserAsync(HttpContext.User);
                _repository.Add(new TodoSqlRepository.TodoItem(m.Text, new Guid(currentUser.Id)));
                return RedirectToAction("Index");
            }
            return View(m);
        }

        [Authorize]
        public async Task<IActionResult> Completed()
        {
            ApplicationUser currentUser = await
            _userManager.GetUserAsync(HttpContext.User);
            return View(_repository.GetCompleted(new Guid(currentUser.Id)));
        }

        [Authorize]
        [HttpGet("{guid}")]
        public async Task<IActionResult> MarkCompleted(string guid)
        {
            if (guid != null)
            {
                ApplicationUser currentUser = await
                _userManager.GetUserAsync(HttpContext.User);
                _repository.MarkAsCompleted(new Guid(guid), new Guid(currentUser.Id));
            }
            return RedirectToAction("Index");
        }
    }
}