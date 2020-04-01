using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestToDo.Api.Models
{
    public class InsertTodo
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}