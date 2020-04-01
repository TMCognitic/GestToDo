using GestToDo.Model.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace GestToDo.Api.Models.Client.Mappers
{
    internal static class IDataRecordExtensions
    {
        internal static ToDo ToToDo(this IDataRecord dataRecord)
        {
            return new ToDo()
            {
                Id = (int)dataRecord["Id"],
                Title = (string)dataRecord["Title"],
                Description = (string)dataRecord["Description"],
                Done = (bool)dataRecord["Done"],
                ValidationDate = (dataRecord["ValidationDate"] is DBNull)? null : (DateTime?)dataRecord["ValidationDate"]
            };
        }
    }
}
