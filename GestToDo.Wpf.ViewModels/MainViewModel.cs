using GestToDo.Models.Client.Data;
using GestToDo.Models.Client.Services;
using GestToDo.Models.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using ToolBox.Patterns.Mediator;
using ToolBox.Patterns.MVVM.Commands;
using ToolBox.Patterns.MVVM.ViewModels;

namespace GestToDo.Wpf.ViewModels
{
    public class MainViewModel : CollectionViewModelBase<ToDoViewModel>
    {
        private ICommand _addCommand;
        private IRepository<int, ToDo> _repository;

        private string _title, _description;

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

        public ICommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand(Add, CanAdd));
            }
        }

        private bool CanAdd()
        {
            return !string.IsNullOrWhiteSpace(Title)
                && !string.IsNullOrWhiteSpace(Description);
        }

        private void Add()
        {
            ToDo toDo = new ToDo(Title, Description);
            toDo = _repository.Insert(toDo);
            Items.Add(new ToDoViewModel(toDo));

            Title = Description = null;
        }

        public MainViewModel()
        {
            _repository = new ToDoRepository("https://localhost:5001/api/");
            Messenger<ToDoViewModel>.Instance.Register("Delete", OnDeleteToDo);
        }

        private void OnDeleteToDo(ToDoViewModel instance)
        {
            Items.Remove(instance);
        }

        protected override ObservableCollection<ToDoViewModel> LoadItems()
        {
            return new ObservableCollection<ToDoViewModel>(_repository.Get().Select(td => new ToDoViewModel(td)));
        }
    }
}
