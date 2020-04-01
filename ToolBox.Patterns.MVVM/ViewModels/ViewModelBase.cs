using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ToolBox.Patterns.MVVM.Commands;

namespace ToolBox.Patterns.MVVM.ViewModels
{
    public abstract class ViewModelBase : ObservableObject
    {
        public ViewModelBase()
        {
            Type viewModelType = GetType();
            IEnumerable<PropertyInfo> propertyInfos = viewModelType.GetProperties().Where(pi => pi.PropertyType.Equals(typeof(ICommand)) || pi.PropertyType.GetInterfaces().Contains(typeof(ICommand)));

            foreach(PropertyInfo propertyInfo in propertyInfos)
            {
                ICommand command = propertyInfo.GetMethod.Invoke(this, null) as ICommand;

                if(!(command is null))
                {
                    PropertyChanged += (s, e) => command.RaiseCanExecuteChanged();
                }                    
            }
        }
    }
}
