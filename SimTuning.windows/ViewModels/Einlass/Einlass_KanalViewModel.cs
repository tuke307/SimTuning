using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using SimTuning.Models;
using SimTuning.ModuleLogic;
using SimTuning.windows.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using UnitsNet.Units;

namespace SimTuning.windows.ViewModels.Einlass
{
    public class Einlass_KanalViewModel : SimTuning.ViewModels.Einlass.KanalViewModel
    {
        public Einlass_KanalViewModel()
        {
            InsertDataCommand = new ActionCommand(InsertData);
        }
    }
}