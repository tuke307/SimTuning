﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTuning.Core.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        public ViewModelBase()
        {

        }

        public virtual Task OnNavigatingTo(object? parameter)
            => Task.CompletedTask;

        public virtual Task OnNavigatedFrom(bool isForwardNavigation)
            => Task.CompletedTask;

        public virtual Task OnNavigatedTo()
            => Task.CompletedTask;
    }
}
