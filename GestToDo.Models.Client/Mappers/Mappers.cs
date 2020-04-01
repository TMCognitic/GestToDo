using G = GestToDo.Model.Global;
using System;
using System.Collections.Generic;
using System.Text;
using GestToDo.Models.Client.Data;

namespace GestToDo.Models.Client.Mappers
{
    internal static class Mappers
    {
        internal static G.ToDo ToGlobal(this ToDo entity)
        {
            return new G.ToDo()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Done = entity.Done,
                ValidationDate = entity.ValidationDate
            };
        }

        internal static ToDo ToClient(this G.ToDo entity)
        {
            return new ToDo(entity.Id, entity.Title, entity.Description, entity.Done, entity.ValidationDate);
        }
    }
}
