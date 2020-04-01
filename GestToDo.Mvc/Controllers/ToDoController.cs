using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestToDo.Models.Client.Data;
using GestToDo.Models.Client.Services;
using GestToDo.Models.Interfaces;
using GestToDo.Mvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestToDo.Mvc.Controllers
{
    public class ToDoController : Controller
    {
        private IRepository<int, ToDo> _repository;

        public ToDoController()
        {
            _repository = new ToDoRepository("https://localhost:44314/api/");
        }
        // GET: ToDo
        public ActionResult Index()
        {
            return View(_repository.Get());
        }

        // GET: ToDo/Details/5
        public ActionResult Details(int id)
        {
            return View(_repository.Get(id));
        }

        // GET: ToDo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ToDo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateToDoForm form)
        {
            if(ModelState.IsValid)
            {
                _repository.Insert(new ToDo(form.Title, form.Description));
                return RedirectToAction(nameof(Index));
            }

            return View(form);
        }

        // GET: ToDo/Edit/5
        public ActionResult Edit(int id)
        {
            ToDo toDo = _repository.Get(id);
            if (toDo is null)
                return new NotFoundResult();

            return View(new EditToDoForm() { Id = toDo.Id, Title = toDo.Title, Description = toDo.Description, Done = toDo.Done });
        }

        // POST: ToDo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditToDoForm form)
        {
            if (ModelState.IsValid)
            {
                ToDo todo = new ToDo(id, form.Title, form.Description, form.Done);
                _repository.Update(id, todo);
                return RedirectToAction(nameof(Index));
            }

            return View(form);
        }

        // GET: ToDo/Delete/5
        public ActionResult Delete(int id)
        {
            ToDo toDo = _repository.Get(id);
            if (toDo is null)
                return new NotFoundResult();

            return View(new EditToDoForm() { Id = toDo.Id, Title = toDo.Title, Description = toDo.Description, Done = toDo.Done });
        }

        // POST: ToDo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}