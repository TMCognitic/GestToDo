using GestToDo.Api.Models.Client.Mappers;
using GestToDo.Model.Global;
using GestToDo.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ToolBox.Connections;

namespace GestToDo.Api.Models.Client.Services
{
    public class ToDoRepository : IRepository<int, ToDo>
    {
        private readonly IConnection _dbConnection;
        public ToDoRepository()
        {
            _dbConnection = new Connection(@"Data Source=AW-Briareos\SQL2016DEV;Initial Catalog=GestToDo;User Id=thierry;password=********;", SqlClientFactory.Instance);
        }

        public IEnumerable<ToDo> Get()
        {
            Command command = new Command("Select Id, Title, [Description], Done, ValidationDate from ToDo;");
            return _dbConnection.ExecuteReader(command, dr => dr.ToToDo());
        }

        public ToDo Get(int key)
        {
            Command command = new Command("Select Id, Title, [Description], Done, ValidationDate from ToDo where Id = @Id;");
            command.AddParameter("Id", key);
            return _dbConnection.ExecuteReader(command, dr => dr.ToToDo()).SingleOrDefault();
        }

        public ToDo Insert(ToDo entity)
        {
            Command command = new Command("Insert into ToDo (Title, [Description]) output Inserted.Id values (@Title, @Description);");
            command.AddParameter("Title", entity.Title);
            command.AddParameter("Description", entity.Description);
            entity.Id = (int)_dbConnection.ExecuteScalar(command);
            return entity;
        }

        public bool Update(int key, ToDo entity)
        {
            Command command = new Command("Update ToDo set Title = @Title, [Description] = @Description, Done = @Done where Id = @Id;");
            command.AddParameter("Id", key);
            command.AddParameter("Title", entity.Title);
            command.AddParameter("Description", entity.Description);
            command.AddParameter("Done", entity.Done);
            return _dbConnection.ExecuteNonQuery(command) == 1;
        }

        public bool Delete(int key)
        {
            Command command = new Command("Delete From ToDo where Id = @Id;");
            command.AddParameter("Id", key);
            return _dbConnection.ExecuteNonQuery(command) == 1;
        }
    }
}
