using System;
using System.Collections.Generic;
using System.Text;

namespace GestToDo.Models.Client.Data
{
    public class ToDo
    {
        private int _id;
        private string _title, _description;
        private bool _done;
        private DateTime? _validationDate;

        public int Id
        {
            get
            {
                return _id;
            }

            private set
            {
                _id = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
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
                _description = value;
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
                _done = value;
            }
        }

        public DateTime? ValidationDate
        {
            get
            {
                return _validationDate;
            }

            private set
            {
                _validationDate = value;
            }
        }

        public ToDo(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public ToDo(int id, string title, string description, bool done)
            : this(id, title, description, done, null)
        {
        }

        internal ToDo(int id, string title, string description, bool done, DateTime? validationDate)
            : this(title, description)
        {
            Id = id;
            Done = done;
            ValidationDate = validationDate;
        }
    }
}
