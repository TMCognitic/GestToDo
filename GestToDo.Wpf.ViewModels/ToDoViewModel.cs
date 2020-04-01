using GestToDo.Models.Client.Data;
using GestToDo.Models.Client.Services;
using GestToDo.Models.Interfaces;
using System;
using ToolBox.Patterns.Mediator;
using ToolBox.Patterns.MVVM.Commands;
using ToolBox.Patterns.MVVM.ViewModels;

namespace GestToDo.Wpf.ViewModels
{
    public class ToDoViewModel : EntityViewModelBase<ToDo>
    {
        IRepository<int, ToDo> _repository;
        private ICommand _updateCommand, _deleteCommand;
        private string _title, _description;
        private bool _done;

        public ToDoViewModel(ToDo entity) : base(entity)
        {
            _repository = new ToDoRepository("https://localhost:5001/api/");
            Title = Entity.Title;
            Description = Entity.Description;
            Done = Entity.Done;
        }

        public int Id
        {
            get { return Entity.Id; }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                Set(ref _title, value);
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                Set(ref _description, value);
            }
        }

        public bool Done
        {
            get
            {
                return _done;
            }

            set
            {
                Set(ref _done, value);
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete));
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return _updateCommand ?? (_updateCommand = new RelayCommand(Update, CanUpdate));
            }
        }

        private bool CanUpdate()
        {
            return Title != Entity.Title;
        }

        private void Update()
        {
            string oldTitle = Entity.Title;
            string oldDescription = Entity.Description;
            bool oldDone = Entity.Done;

            Entity.Title = Title;
            Entity.Description = Description;
            Entity.Done = Done;

            if (!_repository.Update(Id, Entity))
            {
                Entity.Title = oldTitle;
                Entity.Description = oldDescription;
                Entity.Done = oldDone;
            }
        }

        private void Delete()
        {
            _repository.Delete(Id);
            Messenger<ToDoViewModel>.Instance.Send("Delete", this);
        }
    }
}