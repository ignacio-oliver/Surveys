﻿using Prism.Mvvm;
using Prism.Navigation;

namespace Surveys.Core
{
    public abstract class ViewModelBase : BindableBase, INavigatedAware
    {
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}
