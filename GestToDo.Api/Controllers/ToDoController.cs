using GestToDo.Api.Models;
using GestToDo.Api.Models.Client.Services;
using GestToDo.Model.Global;
using GestToDo.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GestToDo.Api.Controllers
{
    public class ToDoController : ApiController
    {
        IRepository<int, ToDo> _repository;

        public ToDoController()
        {
            _repository = new ToDoRepository();
        }

        // GET api/values
        public IEnumerable<ToDo> Get()
        {
            return _repository.Get();
        }

        // GET api/values/5
        public ToDo Get(int id)
        {
            return _repository.Get(id);
        }

        // POST api/values
        public ToDo Post([FromBody]InsertTodo content)
        {
            return _repository.Insert(new ToDo() { Title = content.Title, Description = content.Description });
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]ToDo value)
        {
            _repository.Update(id, value);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
